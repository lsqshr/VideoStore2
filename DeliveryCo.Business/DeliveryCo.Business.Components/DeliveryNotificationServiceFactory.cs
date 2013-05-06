using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Services.Interfaces;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DeliveryCo.Business.Components
{
    public class DeliveryNotificationServiceFactory
    {
        public static IDeliveryNotificationService GetDeliveryNotificationService(String pAddress)
        {
            Binding lBinding;
            if (pAddress.Contains("net.tcp"))
            {
                lBinding = new NetTcpBinding();
            }
            else if (pAddress.Contains("net.msmq"))
            {
                lBinding = new NetMsmqBinding(NetMsmqSecurityMode.None) { Durable = true };
            }
            else
            {
                throw new Exception("Unrecognized address type");
            }
            EndpointAddress myEndpoint = new EndpointAddress(pAddress);
            ChannelFactory<IDeliveryNotificationService> myChannelFactory = new ChannelFactory<IDeliveryNotificationService>(lBinding, myEndpoint);
            return myChannelFactory.CreateChannel();

        }
    }
}
