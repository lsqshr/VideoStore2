using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;
using VideoStore.Business.Components;
using VideoStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Business.Entities;

namespace VideoStore.Services
{

    class BankNotificationService : IBankNotificationService
    {
        public IOrderProvider OrderProvider
        {
            get { return ServiceLocator.Current.GetInstance<IOrderProvider>(); }
        }

        public void NotifyOperationOutcome(Guid OrderNumber , DeliveryInfoStatus Status , String Message) {
            Console.WriteLine("Bank transfer result received: Order Id " + 
                        OrderNumber.ToString() + "\nmsg: " +Message);
            this.OrderProvider.HandleBankNotification(OrderNumber, Message);
        }
    }
}
