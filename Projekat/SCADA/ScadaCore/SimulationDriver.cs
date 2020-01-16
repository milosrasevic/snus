using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Runtime.Serialization;
using ScadaModels;

namespace ScadaCore
{
    
    public class SimulationDriver
    {
        private double Amplitude = 100;
        public Dictionary<int, double> communicationChannel { get; set; }
        public int numOfReservedAddresses { get; set; }

        public Dictionary<string, Thread> threadsDict { get; set; }
        public Dictionary<string, SimulationUnit> unitsDict { get; set; }

        public SimulationDriver(int _numOfReservedAddresses)
        {
            this.threadsDict = new Dictionary<string, Thread>();
            this.unitsDict = new Dictionary<string, SimulationUnit>();
            this.numOfReservedAddresses = _numOfReservedAddresses;
            this.communicationChannel = new Dictionary<int, double>();
            for (int i = 0; i < numOfReservedAddresses; i++)
            {
                this.communicationChannel[i] = 0;
            }
        }
        public bool addNewSimulationUnit(string id, functionType fType, int address)
        {

            Thread t = null;
            SimulationUnit u = new SimulationUnit(); ;
            foreach (var idt in threadsDict.Keys)
            {
                if (idt == id)
                    return false;
            }

            u.id = id;

            switch (fType)
            {
                case functionType.Sin:
                    t = new Thread(new ParameterizedThreadStart(Sine));
                    u.fType = functionType.Sin;
                    break;
                case functionType.Cos:
                    t = new Thread(new ParameterizedThreadStart(Cosine));
                    u.fType = functionType.Cos;
                    break;
                case functionType.Ramp:
                    t = new Thread(new ParameterizedThreadStart(Ramp));
                    u.fType = functionType.Ramp;
                    break;
                case functionType.Triangle:
                    t = new Thread(new ParameterizedThreadStart(Triangle));
                    u.fType = functionType.Triangle;
                    break;
                case functionType.Rectangle:
                    t = new Thread(new ParameterizedThreadStart(Rectangle));
                    u.fType = functionType.Rectangle;
                    break;
            }
            u.address = address;
            unitsDict[id] = u;
            threadsDict[id] = t;
            t.Start(address);
            return true;
        }

        public bool removeSimulationDriverUnit(string id)
        {
            foreach (var idt in threadsDict.Keys)
            {
                if (idt == id)
                {
                    threadsDict[idt].Abort();
                    threadsDict.Remove(idt);
                    unitsDict.Remove(idt);
                    return true;
                }
            }
            return false;
        }



        public void Sine(Object o)
        {
            int address = (int)o;
            while (true)
            {
                writeValue(address, Amplitude * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI));
                Thread.Sleep(500);
            }
        }

        public void Cosine(Object o)
        {
            int address = (int)o;
            while (true)
            {
                writeValue(address, Amplitude * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI));
                Thread.Sleep(500);
            }
        }

        public void Ramp(Object o)
        {
            int address = (int)o;
            while (true)
            {
                writeValue(address, Amplitude * DateTime.Now.Second / 60);
                Thread.Sleep(500);
            }
        }

        public void Triangle(Object o)
        {
            int address = (int)o;
            while (true)
            {
                writeValue(address, ((2 * Amplitude) / Math.PI) * Math.Asin(Math.Sin(2 * Math.PI * DateTime.Now.Second / 60.0)));
                Thread.Sleep(500);
            }
        }

        public void Rectangle(Object o)
        {
            int address = (int)o;
            while (true)
            {
                writeValue(address,Amplitude * Math.Sign(Math.Sin((DateTime.Now.Second % 10) / 5.0)));
                Thread.Sleep(500);
            }
        }
        public bool writeValue(int address, double value)
        {
            lock (this.communicationChannel)
                this.communicationChannel[address] = value;
            return true;
        }

        public double readValue(int ioAddress)
        {
            return this.communicationChannel.ContainsKey(ioAddress) ? this.communicationChannel[ioAddress] : 0;
        }


        public override string ToString()
        {
            string result = "===================================================\n";
            lock (this.communicationChannel)
            {
                foreach (var address in communicationChannel.Keys)
                {
                    result += address + ": " + communicationChannel[address] + "\n";
                }
            }
            result += "===================================================";
            return result;
        }
    }
}