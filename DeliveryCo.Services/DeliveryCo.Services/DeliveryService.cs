using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Services.Interfaces;
using DeliveryCo.Business.Components.Interfaces;
using System.ServiceModel;
using Microsoft.Practices.ServiceLocation;

namespace DeliveryCo.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IDeliveryProvider DeliveryProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDeliveryProvider>();
            }
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Guid SubmitDelivery(DeliveryCo.Business.Entities.DeliveryInfo pDeliveryInfo)
        {
            return DeliveryProvider.SubmitDelivery(pDeliveryInfo);
        }
    }
}
