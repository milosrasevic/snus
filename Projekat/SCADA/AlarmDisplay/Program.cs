using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlarmDisplay.ServiceReference1;

namespace AlarmDisplay
{
    public class ClientAlarmCallback : ServiceReference1.IAlarmerCallback
    {
        public void OnAlarm(Tag t, double value, DateTime timeStamp)
        {
            dynamic d = null;
            if((t is AITag) || (t is DITag)){
                d = t;
            }
            if (d != null)
                Console.WriteLine("Tag {0} with alarms (<{1}) and (>{2}) recieved value {3} at {4}!", t.tagId, d.alarm.floor, d.alarm.ceiling, value, String.Format("{0:d/M/yyyy HH:mm:ss}", timeStamp));
            
        }

       
    }
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new ClientAlarmCallback());
            ServiceReference1.AlarmerClient proxy = new ServiceReference1.AlarmerClient(ic);
            proxy.AlarmerInit();
            while (true) {
                Console.ReadKey();
            }
        }
        
    }
}