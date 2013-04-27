using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace VideoStore.Services.Interfaces{

    public enum DeliveryInfoStatus { Successful=1, Failed=0 };

    [ServiceContract]
    public interface INotifyService
    {
        
        [OperationContract(IsOneWay=true)]
        void NotifyOperationOutcome(Guid OrderId, DeliveryInfoStatus Status, String Message);
    }
}
