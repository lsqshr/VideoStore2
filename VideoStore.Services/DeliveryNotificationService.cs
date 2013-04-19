using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Services.Interfaces;
using VideoStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Business.Entities;

namespace VideoStore.Services
{
    public class DeliveryNotificationService : IDeliveryNotificationService
    {
        IDeliveryNotificationProvider Provider
        {
            get { return ServiceLocator.Current.GetInstance<IDeliveryNotificationProvider>(); }
        }

        public void NotifyDeliveryCompletion(Guid pDeliveryId, DeliveryInfoStatus status)
        {
            Provider.NotifyDeliveryCompletion(pDeliveryId, GetDeliveryStatusFromDeliveryInfoStatus(status));
        }

        private DeliveryStatus GetDeliveryStatusFromDeliveryInfoStatus(DeliveryInfoStatus status)
        {
            if (status == DeliveryInfoStatus.Delivered)
            {
                return DeliveryStatus.Delivered;
            }
            else if(status == DeliveryInfoStatus.Failed)
            {
                return DeliveryStatus.Failed;
            }
            else if(status == DeliveryInfoStatus.Submitted)
            {
                return DeliveryStatus.Submitted;
            }
            else
            { 
                throw new Exception("Unexpected delivery status received");
            }
        }
        
    }
}
