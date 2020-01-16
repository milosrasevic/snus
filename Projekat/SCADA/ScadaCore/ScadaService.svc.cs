using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using ScadaModels;

namespace ScadaCore
{
    public class ScadaService : IRealTimeUnit, IDisplay, IDatabaseManager, IAlarmer, ITrendingPublisher, ITrendingSubscriber
    {
        public const int NUM_OF_ADDRESSES = 5;
        public static RealTimeDriver realTimeDriver { get; set; }
        public static SimulationDriver simulationDriver { get; set; }
        public static OutputDriver outputDriver { get; set; }
        public static TagProcessing tagProcessing { get; set; }


        static IAlarmerCallback alarmProxy = null;
        delegate void AlarmDelegate(Tag t, double value, DateTime timeStamp);
        static event AlarmDelegate alarmed = null;

        static ITrendingSubscriberCallback trendingProxy = null;
        delegate void TrendingDelegateDict(Dictionary<string, Tag> tagsDict);
        delegate void TrendingDelegateScan(Tag t, double value, DateTime timeStamp);

        static event TrendingDelegateDict dictChangedSent = null;
        static event TrendingDelegateScan scanSent = null;

        public ScadaService()
        {
            if (realTimeDriver == null)
                realTimeDriver = new RealTimeDriver(NUM_OF_ADDRESSES);
            if (simulationDriver == null)
                simulationDriver = new SimulationDriver(NUM_OF_ADDRESSES);
            if (outputDriver == null)
                outputDriver = new OutputDriver(NUM_OF_ADDRESSES);
            if (tagProcessing == null)
                tagProcessing = new TagProcessing(this);
        }
        public bool initialize(string id, string publicKey)
        {
            return realTimeDriver.initialize(id,publicKey);
        }
        public bool writeValue(string id, int address, double value, byte[] signature)
        {
            return realTimeDriver.writeValue(id, address, value, signature);
        }
        public void deinitialize(string id, byte[] signature)
        {
            realTimeDriver.deinitialize(id, signature);
        }

        public string readDisplayValues()
        {
            return realTimeDriver.ToString() + "\n" + simulationDriver.ToString();
        }

        public bool addNewSimulationDriverUnit(string id, functionType fType, int address)
        {
            return simulationDriver.addNewSimulationUnit(id, fType, address);
        }

        public bool removeSimulationDriverUnit(string id)
        {
            return simulationDriver.removeSimulationDriverUnit(id);
        }

        public bool addTag(Tag t)
        {
            return tagProcessing.addTag(t);
        }

        public bool removeTag(string tagID)
        {
            return tagProcessing.removeTag(tagID);
        }

        public bool changeTag(string tagID, Dictionary<TagAttribute, object> changes)
        {
            return tagProcessing.changeTag(tagID, changes);
        }

        public Dictionary<string, Tag> getTags()
        {
            return tagProcessing.tagsDict;
        }

        public Dictionary<string, SimulationUnit> getUnits()
        {
            return simulationDriver.unitsDict;
        }

        public void AlarmerInit()
        {
            alarmProxy = OperationContext.Current.GetCallbackChannel<IAlarmerCallback>();
            alarmed += alarmProxy.OnAlarm;
        }

        public void alarm(Tag t, double value, DateTime timeStamp)
        {
            alarmed?.Invoke(t, value, timeStamp);
        }

        public bool sendOutputSignal(int address, double value)
        {
            return outputDriver.writeValue(address, value);
        }

        public bool sendManualInputSignal(int address, double value)
        {
            return simulationDriver.writeValue(address, value);
        }

        public Dictionary<string, Tag> getTagsDict()
        {
            return tagProcessing.tagsDict;
        }

        public List<DBEntry> getEntries()
        {
            return tagProcessing.dBEntries;
        }

        public void TrendingSubscriberInit()
        {
            trendingProxy = OperationContext.Current.GetCallbackChannel<ITrendingSubscriberCallback>();
            scanSent += trendingProxy.OnScan;
            dictChangedSent += trendingProxy.OnTagsDictChanged;
        }

        public void ScanHappened(Tag t, double value, DateTime timeStamp)
        {
            scanSent?.Invoke(t, value, timeStamp);
        }

        public void DictChanged(Dictionary<string, Tag> tagsDict)
        {
            dictChangedSent?.Invoke(tagsDict);
        }
    }
}
