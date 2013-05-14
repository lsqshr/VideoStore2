using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Bank.Services.Interfaces
{
    [DataContract]
    public enum DeliveryInfoStatus { 
        [EnumMember]
        Successful = 1,
        [EnumMember]
        Failed = 0 
    };

    [ServiceContract]
    public interface IBankNotificationService
    {
        
        [OperationContract(IsOneWay=true)]
        void NotifyOperationOutcome(Guid OrderId, DeliveryInfoStatus status , String Message);
    }
}
