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
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Guid SubmitDelivery(DeliveryInfo pDeliveryInfo);
    }
}
