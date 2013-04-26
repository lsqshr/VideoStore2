using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Services.Interfaces;
using System.ServiceModel;

namespace Bank.Business.Components
{
    public class OperationOutcomeServiceFactory
    {
        public static INotifyService GetOperationOutcomeService(String pAddress)
        {
            ChannelFactory<INotifyService> lChannelFactory = new ChannelFactory<INotifyService>(new NetMsmqBinding(NetMsmqSecurityMode.None) { Durable = true }, 
                        new EndpointAddress(pAddress));
            return lChannelFactory.CreateChannel();
        }
    }
}
