using ScadaModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Trending
{

    
    public partial class TrendingForm : Form
    {
        public ServiceReference1.TrendingPublisherClient pProxy = new ServiceReference1.TrendingPublisherClient();
        ServiceReference1.TrendingSubscriberClient cProxy = null;
        public string tagShown = "";
        public TrendingForm()
        {
            InstanceContext ic = new InstanceContext(new TrendingCallbackService(this));
            cProxy = new ServiceReference1.TrendingSubscriberClient(ic);
            cProxy.TrendingSubscriberInit();
            InitializeComponent();

            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Scada Trending";
            myPane.XAxis.Title.Text = "Timestamp";
            myPane.YAxis.Title.Text = "Value";

            initTagsList(pProxy.getTagsDict());


        }
        private void CreateGraph(ZedGraphControl zgc, string tagId)
        {

            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;
            tagShown = tagId;
            myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.Clear();
            zedGraphControl1.Invalidate(true);

            // Set the Titles
            myPane.Title.Text = "Scada Trending for tag " + tagId;
            myPane.XAxis.Title.Text = "Timestamp";
            myPane.YAxis.Title.Text = "Value";
            

            PointPairList list1 = new PointPairList();

            DBEntry[] dBEntries = pProxy.getEntries();
            foreach (DBEntry dBEntry in dBEntries)
            {
                if(dBEntry.tagId == tagId)
                {
                    
                    list1.Add((double)new XDate(dBEntry.timeStamp), dBEntry.value);
                }
            }
            
            myPane.XAxis.Type = AxisType.Date;
            

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve = myPane.AddCurve(tagId,
                  list1, Color.LightSkyBlue, SymbolType.Square);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
        }
        public void OnScan(Tag t, double value, DateTime timeStamp)
        {
            if (t.tagId != tagShown)
                return;
            GraphPane myPane = zedGraphControl1.GraphPane;

            myPane = zedGraphControl1.GraphPane;

            myPane.CurveList[0].AddPoint((double)new XDate(timeStamp), value);

            zedGraphControl1.Refresh();

        }
        public void OnTagsDictChanged(Dictionary<string, Tag> tagsDict)
        {
            listViewTag.Items.Clear();
            initTagsList(tagsDict);

            if (!listViewTag.Items.ContainsKey(tagShown))
            {
                GraphPane myPane = zedGraphControl1.GraphPane;
                myPane = zedGraphControl1.GraphPane;
                myPane.CurveList.Clear();
                zedGraphControl1.Invalidate(true);

                myPane.Title.Text = "Scada Trending";
                myPane.XAxis.Title.Text = "Timestamp";
                myPane.YAxis.Title.Text = "Value";
                tagShown = "";
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            

        }

        private void listViewTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTag.SelectedItems.Count == 0)
                return;

            var firstSelectedItem = listViewTag.SelectedItems[0];
            CreateGraph(zedGraphControl1, firstSelectedItem.Text);
            

            if (firstSelectedItem.SubItems[2].Text == "OutputDriver")
            {
                outName.Text = firstSelectedItem.Text;
                if(zedGraphControl1.GraphPane.CurveList.Count == 1)
                {
                    PointPairList list1 = new PointPairList();
                    list1.Add((double)new XDate(DateTime.Now), double.Parse(firstSelectedItem.SubItems[9].Text));
                    LineItem myCurve = zedGraphControl1.GraphPane.AddCurve(firstSelectedItem.SubItems[0].Text,
                  list1, Color.LightSkyBlue, SymbolType.Square);

                    // Tell ZedGraph to refigure the
                    // axes since the data have changed
                    zedGraphControl1.AxisChange();
                }
                
            }
            else if (firstSelectedItem.SubItems[2].Text == "SimulationDriver")
            {
                simName.Text = firstSelectedItem.Text;
            }

        }

        private void initTagsList(Dictionary<string, Tag> tags)
        {
            Tag t = null;
            foreach (var tag in tags.Keys)
            {
                t = tags[tag];
                var item = new ListViewItem(t.tagId);
                item.SubItems.Add(t.description);
                item.SubItems.Add(t.driver.ToString());
                item.SubItems.Add(t.ioAddress.ToString());
                if (t is AITag)
                {
                    var ti = (AITag)t;
                    item.SubItems.Add(ti.scanTime.ToString());
                    item.SubItems.Add(ti.alarm.floor.ToString());
                    item.SubItems.Add(ti.alarm.ceiling.ToString());
                    item.SubItems.Add(ti.on ? "Yes" : "No");
                    item.SubItems.Add(ti.auto ? "Yes" : "No");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.low.ToString());
                    item.SubItems.Add(ti.high.ToString());
                    item.SubItems.Add(ti.units);
                }
                else if (t is AOTag)
                {
                    var ti = (AOTag)t;
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.initialValue.ToString());
                    item.SubItems.Add(ti.low.ToString());
                    item.SubItems.Add(ti.high.ToString());
                    item.SubItems.Add(ti.units);
                }
                else if (t is DITag)
                {
                    var ti = (DITag)t;
                    item.SubItems.Add(ti.scanTime.ToString());
                    item.SubItems.Add(ti.alarm.floor.ToString());
                    item.SubItems.Add(ti.alarm.ceiling.ToString());
                    item.SubItems.Add(ti.on ? "Yes" : "No");
                    item.SubItems.Add(ti.auto ? "Yes" : "No");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                else if (t is DOTag)
                {
                    var ti = (DOTag)t;
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.initialValue.ToString());
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                listViewTag.Items.Add(item);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void simName_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewTag.Items)
            {
                if (item.Text == simName.Text)
                {
                    if (item.SubItems[2].Text == "OutputDriver")
                    {
                        simVal.Enabled = false;
                        sendSimBtn.Enabled = false;
                    }
                    else if (item.SubItems[2].Text == "SimulationDriver" && item.SubItems[8].Text == "No")
                    {
                        simVal.Enabled = true;
                        sendSimBtn.Enabled = true;
                    }
                    else
                    {
                        simVal.Enabled = false;
                        sendSimBtn.Enabled = false;
                    }
                }
            }
        }

        private void outName_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewTag.Items)
            {
                if (item.Text == outName.Text)
                {
                    if (item.SubItems[2].Text == "OutputDriver")
                    {
                        outVal.Enabled = true;
                        sendOutBtn.Enabled = true;
                    }
                    else
                    {
                        outVal.Enabled = false;
                        sendOutBtn.Enabled = false;
                    }
                }
            }
        }

        private void sendSimBtn_Click(object sender, EventArgs e)
        {
            string tagid = simName.Text;
            foreach(ListViewItem item in listViewTag.Items)
            {
                if(item.Text == tagid)
                {
                    if(item.SubItems[2].Text == "SimulationDriver" && item.SubItems[8].Text == "No")
                    {
                        pProxy.sendManualInputSignal(int.Parse(item.SubItems[3].Text), (double)simVal.Value);
                    }
                }
            }
        }

        private void sendOutBtn_Click(object sender, EventArgs e)
        {
            string tagid = outName.Text;
            foreach (ListViewItem item in listViewTag.Items)
            {
                if (item.Text == tagid)
                {
                    if (item.SubItems[2].Text == "OutputDriver")
                    {
                        if(pProxy.sendOutputSignal(int.Parse(item.SubItems[3].Text), (double)simVal.Value))
                        {
                            if (tagid != tagShown)
                                return;
                            GraphPane myPane = zedGraphControl1.GraphPane;

                            myPane = zedGraphControl1.GraphPane;

                            myPane.CurveList[1].AddPoint((double)new XDate(DateTime.Now), (double)outVal.Value);

                            zedGraphControl1.Refresh();
                        }
                    }
                }
            }
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }

    public class TrendingCallbackService : ServiceReference1.ITrendingSubscriberCallback
    {
        public TrendingForm form;
        public TrendingCallbackService(TrendingForm f)
        {
            form = f;
        }
        public void OnScan(Tag t, double value, DateTime timeStamp)
        {
            form.OnScan(t, value, timeStamp);
        }
        public void OnTagsDictChanged(Dictionary<string, Tag> tagsDict)
        {
            form.OnTagsDictChanged(tagsDict);

        }
    }
}
