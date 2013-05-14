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
            Console.WriteLine("result received: Order Id " + 
                        OrderNumber.ToString() + "\nmsg: " +Message);
            if (Message == "Success")
            {
                //request to start delivery
                Order pOrder = this.OrderProvider.FindOrderbyOrderNumber(OrderNumber);
                OrderProvider.PlaceDeliveryForOrder(pOrder);
                OrderProvider.SendDeliverySubmittedEmail(OrderNumber);               
            }
            else {
                //if the transfer fails, send email to the customer with fail message
                OrderProvider.SendTransferErrorEmail(OrderNumber, Message);
            }
        }
    }
}
