using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScadaCore
{
    public class OutputDriver
    {
        public Dictionary<int, double> communicationChannel { get; set; }

        public OutputDriver(int numOfAddresses)
        {
            this.communicationChannel = new Dictionary<int, double>();
            for (int i = 0; i < numOfAddresses; i++)
                this.communicationChannel[i] = 0;
        }

        public bool writeValue(int address, double value)
        {
            if(communicationChannel.ContainsKey(address))
            {
                communicationChannel[address] = value;
                return true;
            }
            return false;
        }

        public double readValue(int ioAddress)
        {
            return this.communicationChannel.ContainsKey(ioAddress) ? this.communicationChannel[ioAddress] : 0;
        }
    }
}