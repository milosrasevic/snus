using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        public int ceiling { get; set; }

        [DataMember]
        public int floor { get; set; }

        public Alarm(int ceiling, int floor)
        {
            this.ceiling = ceiling;
            this.floor = floor;
        }
    }
}
