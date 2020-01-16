using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ScadaModels;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Text;
using Npgsql;
using System.Data;

namespace ScadaCore
{
    public class TagProcessing
    {
        const string ALARM_LOG_FILE = @"C:\Users\Nenad\Desktop\alarmLog.txt";
        const string CONFIG_FILE = @"C:\Users\Nenad\Desktop\scadaConfig.xml";
        public ScadaService scada { get; set; }
        public Dictionary<string, Tag> tagsDict { get; set; }
        public Dictionary<string, Thread> threadsDict { get; set; }

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public List<DBEntry> dBEntries = new List<DBEntry>();

        public TagProcessing(ScadaService scada)
        {
            tagsDict = new Dictionary<string, Tag>();
            threadsDict = new Dictionary<string, Thread>();
            this.scada = scada;
            loadCfg();
            loadDB();
        }

        public void loadCfg()
        {
            // check if file exists
            if (!File.Exists(CONFIG_FILE))
                return;
            // loads from xml file on DATABASE_PATH
            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Tag>));
            FileStream fs = new FileStream(CONFIG_FILE, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

            List<Tag> tags = (List<Tag>)dcs.ReadObject(reader);
            reader.Close();
            fs.Close();
            foreach(Tag t in tags)
            {
                addTag(t);
            }
        }


        public bool addTag(Tag t)
        {
            if (tagsDict.ContainsKey(t.tagId))
            {
                return false;
            }
            foreach(var key in tagsDict.Keys)
            {
                if(tagsDict[key].ioAddress == t.ioAddress && tagsDict[key].driver == t.driver)
                {
                    return false;
                }
            }


            tagsDict[t.tagId] = t;

            if (t is AITag || t is DITag)
            {
                Thread th = new Thread(new ParameterizedThreadStart(scanTag));
                threadsDict[t.tagId] = th;
                th.Start(t.tagId);
            }
            else
            {
                if(t is AOTag)
                {
                    ScadaService.outputDriver.writeValue(t.ioAddress, ((AOTag)t).initialValue);
                }else if(t is DOTag)
                {
                    ScadaService.outputDriver.writeValue(t.ioAddress, ((DOTag)t).initialValue);
                }
            }

            scada.DictChanged(tagsDict);
            saveCfg();
            return true;
        }

        public bool removeTag(string tagID)
        {
            if (tagsDict.ContainsKey(tagID))
            {
                tagsDict.Remove(tagID);
                if (threadsDict.ContainsKey(tagID))
                {
                    threadsDict[tagID].Abort();
                    threadsDict.Remove(tagID);
                }

                scada.DictChanged(tagsDict);
                saveCfg();
                return true;
            }
            return false;
        }

        public bool changeTag(string tagID, Dictionary<TagAttribute, object> changes)
        {
            if (!(tagsDict.ContainsKey(tagID)))
                return false;

            Tag t = tagsDict[tagID];
            dynamic d = t;
            foreach(TagAttribute key in changes.Keys)
            {
                switch (key)
                {
                    case TagAttribute.tagId:
                        //cant change id
                        return false;
                    case TagAttribute.description:
                        t.description = (string)changes[key];
                        break;
                    case TagAttribute.driver:
                        if ((string)changes[key] == "OutputDriver")
                            return false;
                        t.driver = (string)changes[key]=="RealTimeDriver"?Driver.RealTimeDriver : Driver.SimulationDriver;
                        break;
                    case TagAttribute.ioAddress:
                        t.ioAddress = (int) changes[key];
                        break;
                    case TagAttribute.scanTime:
                        if(d is DITag || d is AITag)
                            d.scanTime = (int)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.alarm:
                        if (d is DITag || d is AITag)
                        {
                            string[] alarmValues = ((string)changes[key]).Split('|');
                            d.alarm = new Alarm(int.Parse(alarmValues[0]), int.Parse(alarmValues[1]));
                        }
                        else
                            return false;
                        break;
                    case TagAttribute.on:
                        if (d is DITag || d is AITag)
                            d.on = (bool)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.auto:
                        if ((d is DITag || d is AITag) && t.driver==Driver.SimulationDriver)
                            d.auto = (bool)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.initialValue:
                        if (d is AOTag || d is DOTag)
                            d.initialValue = (int)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.low:
                        if (d is AITag || d is AOTag)
                            d.low = (int)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.high:
                        if (d is AITag || d is AOTag)
                            d.high = (int)changes[key];
                        else
                            return false;
                        break;
                    case TagAttribute.units:
                        if (d is AITag || d is AOTag)
                            d.units = (string)changes[key];
                        else
                            return false;
                        break;
                    default:
                        break;
                }
            }
            scada.DictChanged(tagsDict);
            saveCfg();
            return true;
        }


        public void scanTag(object o)
        {
            string tagId = (string)o;
            Tag t = tagsDict[tagId];
            if (!(t is AITag || t is DITag))
                return;

            double value = 0;
            while (true)
            {
                t = tagsDict[tagId];
                dynamic d = t;

                if (!d.on)
                {
                    Thread.Sleep(d.scanTime);
                    continue;
                }

                if (t.driver == Driver.RealTimeDriver)
                {
                    value = ScadaService.realTimeDriver.readValue(t.ioAddress);
                } else if (t.driver == Driver.SimulationDriver)
                {
                    value = ScadaService.simulationDriver.readValue(t.ioAddress);
                }

                if (t is AITag)
                {
                    AITag ai = (AITag)t;
                    if (value < ai.low)
                        value = ai.low;
                    else if (value > ai.high)
                        value = ai.high;
                }

                Alarm a = d.alarm;

                DateTime timeStamp = DateTime.Now;

                if (value < a.floor || value > a.ceiling)
                {
                    alarm(t, value, timeStamp);
                }
                scada.ScanHappened(t, value, timeStamp);
                dBEntries.Add(new DBEntry(t.tagId, value, timeStamp));
                writeToDB(t, value, timeStamp);
                Thread.Sleep(d.scanTime);
            }
            
        }

        public void alarm(Tag t, double value, DateTime timeStamp)
        {
            

            dynamic d = null;
            if ((t is AITag) || (t is DITag))
            {
                d = t;
            }
            if (d == null)
                return;

            ReaderWriterLock locker = new ReaderWriterLock();
            locker.AcquireWriterLock(int.MaxValue);
            File.AppendAllText(ALARM_LOG_FILE, string.Format("Tag {0} with alarms (<{1}) and (>{2}) recieved value {3} at {4}!", t.tagId, d.alarm.floor, d.alarm.ceiling, value, String.Format("{0:d/M/yyyy HH:mm:ss}", timeStamp)));
            locker.ReleaseLock();

            
        
            scada.alarm(t, value, timeStamp);
        }
        
        public bool onOutputData(Tag t)
        {
            if (!((t is AOTag) || (t is DOTag)))
                return false;

            double value = ScadaService.outputDriver.readValue(t.ioAddress);
            DateTime timeStamp = DateTime.Now;
            if(t is AOTag)
            {
                if (((AOTag)t).low > value)
                    value = ((AOTag)t).low;
                else if (((AOTag)t).high < value)
                    value = ((AOTag)t).high;
            }

            writeToDB(t, value, timeStamp);
            return true;
        }

        public void writeToDB(Tag t, double value, DateTime timeStamp)
        {
            try
            {
                string connstring = String.Format("Server={0};Port={1};" +
                   "User Id={2};Password={3};Database={4};",
                   "localhost", "5432", "postgres",
                   "qwerqwer1", "Scada");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = string.Format("INSERT INTO Events VALUES (DEFAULT, '{0}', {1}, (TIMESTAMP '{2}'))", t.tagId, value, timeStamp.ToString("yyyy-MM-dd HH:mm:ss"));
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                msg.ToString();
                throw;
            }
        }

        public void loadDB()
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "localhost", "5432", "postgres",
                    "qwerqwer1", "Scada");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT * FROM Events";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                foreach (DataRow row in dt.Rows)
                {
                    DBEntry dbe = new DBEntry(row["tagId"].ToString(), (double)row["value"], (DateTime)row["timeStamp"]);
                    dBEntries.Add(dbe);
                }
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                msg.ToString();
                throw;
            }
        }

        public void saveCfg()
        {

            List<Tag> tags = new List<Tag>();
            foreach (var key in tagsDict.Keys)
            {
                tags.Add(tagsDict[key]);
            }

            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Tag>));


            using (Stream stream = new FileStream(CONFIG_FILE, FileMode.Create, FileAccess.ReadWrite))
            {
                using (XmlDictionaryWriter writer =
                    XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    dcs.WriteObject(writer, tags);
                }
            }
        }
    }
}