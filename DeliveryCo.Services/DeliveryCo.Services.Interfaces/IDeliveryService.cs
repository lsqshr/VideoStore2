using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Business.Entities;
using System.ServiceModel;

namespace DeliveryCo.Services.Interfaces
{
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract(IsOneWay = true)]
        void SubmitDelivery(DeliveryInfo pDeliveryInfo);
    }
}
