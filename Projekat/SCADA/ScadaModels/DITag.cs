using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class DITag : Tag
    {
        [DataMember]
        public int scanTime { get; set; }

        [DataMember]
        public Alarm alarm { get; set; }

        [DataMember]
        public bool on { get; set; }

        [DataMember]
        public bool auto { get; set; }

        public DITag(string tagId, string description, int ioAddress, Driver driver, int scanTime, Alarm alarm, bool on, bool auto) : base(tagId, description, ioAddress, driver)
        {
            this.scanTime = scanTime;
            this.alarm = alarm;
            this.on = on;
            this.auto = auto;
        }
    }
}
