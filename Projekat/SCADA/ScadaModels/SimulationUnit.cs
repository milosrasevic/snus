using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public struct SimulationUnit
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public functionType fType { get; set; }
        [DataMember]
        public int address { get; set; }
    }
}
