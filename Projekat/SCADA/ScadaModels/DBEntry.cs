using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class DBEntry
    {
        [DataMember]
        public string tagId { get; set; }
        [DataMember]
        public double value { get; set; }
        [DataMember]
        public DateTime timeStamp { get; set; }

        public DBEntry(string t, double v, DateTime ts)
        {
            this.tagId = t;
            this.value = v;
            this.timeStamp = ts;
        }
    }
}
