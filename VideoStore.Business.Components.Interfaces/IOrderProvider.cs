using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IOrderProvider
    {
        void SubmitOrder(Order pOrder);
        void SendTransferErrorEmail(Guid OrderNumber, String Message);
        Order FindOrderbyOrderNumber(Guid tOrderNumber);
        void PlaceDeliveryForOrder(Order pOrder);
        void SendDeliverySubmittedEmail(Guid OrderNumber);
    }
}
