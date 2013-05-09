using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace VideoStore.Services.Interfaces{

    [DataContract]
    public enum DeliveryInfoStatus { 
        [EnumMember]
        Successful=1, 
        [EnumMember]
        Failed=0 
    };

    [ServiceContract]
    public interface INotifyService
    {
        
        [OperationContract(IsOneWay=true)]
        void NotifyOperationOutcome(Guid OrderId, DeliveryInfoStatus Status, String Message);
    }
}
