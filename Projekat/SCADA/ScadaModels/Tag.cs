using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ScadaModels
{
    [DataContract]
    [KnownType(typeof(DITag))]
    [KnownType(typeof(DOTag))]
    [KnownType(typeof(AITag))]
    [KnownType(typeof(AOTag))]
    public abstract class Tag
    {
        [DataMember]
        public string tagId { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public Driver driver { get; set; }

        [DataMember]
        public int ioAddress { get; set; }

        public Tag(string tagId, string description, int ioAddress, Driver driver)
        {
            this.tagId = tagId;
            this.description = description;
            this.ioAddress = ioAddress;
            this.driver = driver;
        }

    }
}
