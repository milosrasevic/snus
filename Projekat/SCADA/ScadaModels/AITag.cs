using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class AITag : Tag
    {
        [DataMember]
        public int scanTime { get; set; }

        [DataMember]
        public Alarm alarm { get; set; }

        [DataMember]
        public bool on { get; set; }

        [DataMember]
        public bool auto { get; set; }

        [DataMember]
        public int low { get; set; }

        [DataMember]
        public int high { get; set; }

        [DataMember]
        public string units { get; set; }

        public AITag(string tagId, string description, int ioAddress, Driver driver, int scanTime, Alarm alarm, bool on, bool auto, int low, int high, string units) : base(tagId, description, ioAddress, driver)
        {
            this.scanTime = scanTime;
            this.alarm = alarm;
            this.on = on;
            this.auto = auto;
            this.low = low;
            this.high = high;
            this.units = units;
        }
    }
}
