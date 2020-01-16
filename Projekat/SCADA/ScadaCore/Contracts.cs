using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using ScadaModels;

namespace ScadaCore
{
    [ServiceContract]
    interface IRealTimeUnit
    {
        [OperationContract]
        bool initialize(string id, string publicKey);
        [OperationContract]
        bool writeValue(string id, int address, double value, byte[] signature);
        [OperationContract]
        void deinitialize(string id, byte[] signature);
    }

    [ServiceContract]
    interface IDisplay
    {
        [OperationContract]
        string readDisplayValues();
    }

    [ServiceContract]
    interface IDatabaseManager
    {
        [OperationContract]
        bool addNewSimulationDriverUnit(string id, functionType fType, int address);

        [OperationContract]
        bool removeSimulationDriverUnit(string id);

        [OperationContract]
        bool addTag(Tag t);
        [OperationContract]
        bool removeTag(string tagID);
        [OperationContract]
        bool changeTag(string tagID, Dictionary<TagAttribute, object> changes);

        [OperationContract]
        Dictionary<string, Tag> getTags();
        
        [OperationContract]
        Dictionary<string, SimulationUnit> getUnits();
    }
    
    public interface IAlarmerCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnAlarm(Tag t, double value, DateTime timeStamp);
    }

    [ServiceContract(CallbackContract = typeof(IAlarmerCallback))]
    public interface IAlarmer
    {
        [OperationContract]
        void AlarmerInit();    }

    [ServiceContract]
    public interface ITrendingPublisher
    {
        [OperationContract]
        bool sendOutputSignal(int address, double value);
        [OperationContract]
        bool sendManualInputSignal(int address, double value);
        [OperationContract]
        Dictionary<string, Tag> getTagsDict();
        [OperationContract]
        List<DBEntry> getEntries();

        [OperationContract]
        void ScanHappened(Tag t, double value, DateTime timeStamp);
        [OperationContract]
        void DictChanged(Dictionary<string, Tag> tagsDict);
    }
    public interface ITrendingSubscriberCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnScan(Tag t, double value, DateTime timeStamp);
        [OperationContract(IsOneWay = true)]
        void OnTagsDictChanged(Dictionary<string, Tag> tagsDict);
        
    }

    [ServiceContract(CallbackContract = typeof(ITrendingSubscriberCallback))]
    public interface ITrendingSubscriber
    {
        [OperationContract]
        void TrendingSubscriberInit();    }

}
