using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ScadaModels
{
    [DataContract]
    public enum TagAttribute
    {
        [EnumMember] tagId, [EnumMember] description, [EnumMember] driver, [EnumMember] ioAddress, [EnumMember] scanTime, [EnumMember] alarm, [EnumMember] on, [EnumMember] auto, [EnumMember] initialValue, [EnumMember] low, [EnumMember] high, [EnumMember] units
    }

    [DataContract]
    public enum Driver
    {
        [EnumMember] SimulationDriver,
        [EnumMember] RealTimeDriver,
        [EnumMember] OutputDriver
    }

    [DataContract]
    public enum functionType
    {
        [EnumMember] Sin, [EnumMember] Cos, [EnumMember] Ramp, [EnumMember] Triangle, [EnumMember] Rectangle
    }
}
