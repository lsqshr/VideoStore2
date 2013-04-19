using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using System.Transactions;
using VideoStore.Business.Components.TransferService;
using VideoStore.Business.Components.DeliveryService;
using Microsoft.Practices.ServiceLocation;

namespace VideoStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        public IEmailProvider EmailProvider
        {
            get { return ServiceLocator.Current.GetInstance<IEmailProvider>(); }
        }

        public void SubmitOrder(Entities.Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                try
                {
                    pOrder.OrderNumber = Guid.NewGuid();
                    TransferFundsFromCustomer(pOrder.Customer.BankAccountNumber, pOrder.Total ?? 0.0);
                    PlaceDeliveryForOrder(pOrder);
                    lContainer.Orders.ApplyChanges(pOrder);
                    pOrder.UpdateStockLevels();
                    lContainer.SaveChanges();
                    lScope.Complete();
                    SendOrderPlacedConfirmation(pOrder);
                }
                catch (Exception lException)
                {
                    SendOrderErrorMessage(pOrder, lException);
                    throw;
                }
            }
        }

        private void SendOrderErrorMessage(Order pOrder, Exception pException)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "There was an error in processsing your order " + pOrder.OrderNumber + ": "+ pException.Message +". Please contact Video Store"
            });
        }

        private void SendOrderPlacedConfirmation(Order pOrder)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "Your order " + pOrder.OrderNumber + " has been placed"
            });
        }

        private void PlaceDeliveryForOrder(Order pOrder)
        {
            Delivery lDelivery = new Delivery() { DeliveryStatus = DeliveryStatus.Submitted, SourceAddress = "Video Store Address", DestinationAddress = pOrder.Customer.Address, Order = pOrder };
            DeliveryServiceClient lClient = new DeliveryServiceClient();
            Guid lDeliveryIdentifier = lClient.SubmitDelivery(new DeliveryInfo()
            { 
                OrderNumber = lDelivery.Order.OrderNumber.ToString(),  
                SourceAddress = lDelivery.SourceAddress,
                DestinationAddress = lDelivery.DestinationAddress,
                DeliveryNotificationAddress = "net.tcp://localhost:9010/DeliveryNotificationService"
            });

            lDelivery.ExternalDeliveryIdentifier = lDeliveryIdentifier;
            pOrder.Delivery = lDelivery;
            
        }

        private void TransferFundsFromCustomer(int pCustomerAccountNumber, double pTotal)
        {
            TransferServiceClient lClient = new TransferServiceClient();
            lClient.Transfer(pTotal, pCustomerAccountNumber, RetrieveVideoStoreAccountNumber());
        }



        private int RetrieveVideoStoreAccountNumber()
        {
            return 123;
        }


    }
}
