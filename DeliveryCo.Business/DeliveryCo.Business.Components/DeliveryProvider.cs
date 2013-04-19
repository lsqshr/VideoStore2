using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Business.Components.Interfaces;
using System.Transactions;
using DeliveryCo.Business.Entities;
using System.Threading;
using DeliveryCo.Services.Interfaces;

namespace DeliveryCo.Business.Components
{
    public class DeliveryProvider : IDeliveryProvider
    {
        public Guid SubmitDelivery(DeliveryCo.Business.Entities.DeliveryInfo pDeliveryInfo)
        {
            using(TransactionScope lScope = new TransactionScope())
            using(DeliveryDataModelContainer lContainer = new DeliveryDataModelContainer())
            {
                pDeliveryInfo.DeliveryIdentifier = Guid.NewGuid();
                pDeliveryInfo.Status = 0;
                lContainer.DeliveryInfoes.AddObject(pDeliveryInfo);
                lContainer.SaveChanges();
                ThreadPool.QueueUserWorkItem(new WaitCallback((pObj) => ScheduleDelivery(pDeliveryInfo)));
                lScope.Complete();
            }
            return pDeliveryInfo.DeliveryIdentifier;
        }

        private void ScheduleDelivery(DeliveryInfo pDeliveryInfo)
        {
            Console.WriteLine("Delivering to" + pDeliveryInfo.DestinationAddress);
            Thread.Sleep(1000);
            //notifying of delivery completion
            using (TransactionScope lScope = new TransactionScope())
            using (DeliveryDataModelContainer lContainer = new DeliveryDataModelContainer())
            {
                pDeliveryInfo.Status = 1;
                IDeliveryNotificationService lService = DeliveryNotificationServiceFactory.GetDeliveryNotificationService(pDeliveryInfo.DeliveryNotificationAddress);
                lService.NotifyDeliveryCompletion(pDeliveryInfo.DeliveryIdentifier, DeliveryInfoStatus.Delivered);
            }

        }
    }
}
