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
        public static IOperationOutcomeService GetOperationOutcomeService(String pAddress)
        {
            ChannelFactory<IOperationOutcomeService> lChannelFactory = new ChannelFactory<IOperationOutcomeService>(new NetTcpBinding(), new EndpointAddress(pAddress));
            return lChannelFactory.CreateChannel();
        }
    }
}
