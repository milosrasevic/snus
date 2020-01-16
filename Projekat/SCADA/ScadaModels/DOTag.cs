using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public class DOTag : Tag
    {
        [DataMember]
        public int initialValue { get; set; }

        public DOTag(string tagId, string description, int ioAddress, Driver driver, int initialValue) : base(tagId, description, ioAddress, driver)
        {
            this.initialValue = initialValue;
        }
    }
}
