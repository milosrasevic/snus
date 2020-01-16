using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class AOTag : Tag
    {
        [DataMember]
        public int initialValue { get; set; }
        [DataMember]
        public int low { get; set; }
        [DataMember]
        public int high { get; set; }
        [DataMember]
        public string units { get; set; }

        public AOTag(string tagId, string description, int ioAddress, Driver driver, int initialValue, int low, int high, string units) : base(tagId, description, ioAddress, driver)
        {
            this.initialValue = initialValue;
            this.low = low;
            this.high = high;
            this.units = units;
        }
    }
}
