using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Services.Interfaces;
using System.ServiceModel;

namespace DeliveryCo.Business.Components
{
    public class DeliveryNotificationServiceFactory
    {
        public static IDeliveryNotificationService GetDeliveryNotificationService(String pAddress)
        {
            ChannelFactory<IDeliveryNotificationService> lChannelFactory = new ChannelFactory<IDeliveryNotificationService>(new NetTcpBinding(), new EndpointAddress(pAddress));
            return lChannelFactory.CreateChannel();
        }
    }
}
