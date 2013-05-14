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
            //using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                try
                {
                    pOrder.OrderNumber = Guid.NewGuid();
                    TransferFundsFromCustomer(pOrder.OrderNumber,pOrder.Customer.BankAccountNumber, pOrder.Total ?? 0.0);
                    Console.WriteLine("Bank Done");
                    lContainer.Orders.ApplyChanges(pOrder);
                    lContainer.SaveChanges();
                    PlaceDeliveryForOrder(pOrder);
                    /*Console.WriteLine("Delivery Done");
                    lContainer.Orders.ApplyChanges(pOrder);
                    pOrder.UpdateStockLevels();
                    lContainer.SaveChanges();
                    lScope.Complete();
                    Console.WriteLine("order transaction finishes");
                    SendOrderPlacedConfirmation(pOrder);
                    Console.WriteLine("Order confirmation sent");*/
                }
                catch (Exception lException)
                {
                    //SendOrderErrorMessage(pOrder, lException);
                    Console.WriteLine("Transer Message Failed");
                    Console.WriteLine(lException.Message);
                    throw;
                }
            }
        }

        public void SendOrderErrorMessage(Guid OrderNumber, String Message)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                Order pOrder = lContainer.Orders.Include("Customer").FirstOrDefault((tOrder) => tOrder.OrderNumber == OrderNumber); 
                
                EmailProvider.SendMessage(new EmailMessage()
                {
                    ToAddress = pOrder.Customer.Email,
                    Message = "There was an error in processsing your order " + pOrder.OrderNumber + ": " + Message + ". Please contact Video Store"
                });
                lScope.Complete();
 
            }

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
            Delivery lDelivery = new Delivery() { ExternalDeliveryIdentifier = Guid.NewGuid(), DeliveryStatus = DeliveryStatus.Submitted, SourceAddress = "Video Store Address", DestinationAddress = pOrder.Customer.Address, Order = pOrder };
            DeliveryServiceClient lClient = new DeliveryServiceClient();

            lClient.SubmitDelivery(new DeliveryInfo()
            {
                DeliveryIdentifier = lDelivery.ExternalDeliveryIdentifier,
                OrderNumber = lDelivery.Order.OrderNumber.ToString(),
                SourceAddress = lDelivery.SourceAddress,
                DestinationAddress = lDelivery.DestinationAddress,
                DeliveryNotificationAddress = "net.msmq://localhost/private/DeliveryNotificationService"
            });

            pOrder.Delivery = lDelivery;
            
            
        }

        private void TransferFundsFromCustomer(Guid OrderNumber,int pCustomerAccountNumber, double pTotal)
        {
            using (TransferServiceClient lClient = new TransferServiceClient()){
                lClient.Transfer(OrderNumber, pTotal, 
                    pCustomerAccountNumber, RetrieveVideoStoreAccountNumber(),
                    "net.msmq://localhost/private/BankNotificationService");
                Console.WriteLine("new transfer message sent" + OrderNumber.ToString());
            }
        }



        private int RetrieveVideoStoreAccountNumber()
        {
            return 123;
        }


    }
}
