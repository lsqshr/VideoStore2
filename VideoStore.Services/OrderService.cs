using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using Microsoft.Practices.ServiceLocation;

namespace VideoStore.Services
{
    public class OrderService : IOrderService
    {

        private IOrderProvider OrderProvider
        {
            get
            {
                return ServiceFactory.GetService<IOrderProvider>();
            }
        }

        public void SubmitOrder(Business.Entities.Order pOrder)
        {
            OrderProvider.SubmitOrder(pOrder);
        }

        public void NotifyTransferOutcome(TransferOutcome outcome)
        {
            Console.WriteLine("Transfer done");
        }
    }
}
