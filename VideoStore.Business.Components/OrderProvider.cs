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

        public Order FindOrderbyOrderNumber(Guid tOrderNumber)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                return lContainer.Orders.Include("Customer").FirstOrDefault((pOrder) => pOrder.OrderNumber == tOrderNumber );
            }
        }

        public void SubmitOrder(Entities.Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                try
                {
                    // Only Request the transfer, delivery will be placed after the trasfer succeed
                    pOrder.OrderNumber = Guid.NewGuid();
                    //debug call delivery first
                    TransferFundsFromCustomer(pOrder.OrderNumber,pOrder.Customer.BankAccountNumber, pOrder.Total ?? 0.0);
                    //do delivery fist
                    //PlaceDeliveryForOrder(pOrder);
                    Console.WriteLine("Bank Done");
                    lContainer.Orders.ApplyChanges(pOrder);
                    lContainer.SaveChanges();
                    lScope.Complete();
                    //PlaceDeliveryForOrder(pOrder);
                    /*Console.WriteLine("Delivery Done");
                    lContainer.Orders.ApplyChanges(pOrder);
                    
                    lContainer.SaveChanges();
                    
                    Console.WriteLine("order transaction finishes");
                    SendOrderPlacedConfirmation(pOrder);
                    Console.WriteLine("Order confirmation sent");*/
                }
                catch (Exception lException)
                {
                    //SendTransferErrorEmail(pOrder, lException);
                    Console.WriteLine("Transer Message Failed");
                    Console.WriteLine(lException.Message);
                    throw;
                }
            }
        }

        public void SendTransferErrorEmail(Guid OrderNumber, String Message)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                Order pOrder = lContainer.Orders.Include("Customer").FirstOrDefault((tOrder) => tOrder.OrderNumber == OrderNumber); 
                
                EmailProvider.SendMessage(new EmailMessage()
                {
                    ToAddress = pOrder.Customer.Email,
                    Message = "There was an error in processsing your order in fund transferring: " + pOrder.OrderNumber + ": " + Message + ". Please contact Video Store"
                });
                lScope.Complete();
 
            }

        }

        public void SendDeliverySubmittedEmail(Guid OrderNumber)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                Order pOrder = lContainer.Orders.Include("Customer").FirstOrDefault((tOrder) => tOrder.OrderNumber == OrderNumber);

                EmailProvider.SendMessage(new EmailMessage()
                {
                    ToAddress = pOrder.Customer.Email,
                    Message = "Dear " + pOrder.Customer.Name + ",\n Your order with order number " + OrderNumber + " has been requested for delivery." 
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

        public void PlaceDeliveryForOrder(Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                Delivery lDelivery = new Delivery()
                {
                    ExternalDeliveryIdentifier = Guid.NewGuid(),
                    DeliveryStatus = DeliveryStatus.Submitted,
                    SourceAddress = "Video Store Address",
                    DestinationAddress = pOrder.Customer.Address,
                    Order = pOrder
                };
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
                lContainer.Orders.ApplyChanges(pOrder);
                lContainer.Deliveries.ApplyChanges(lDelivery);
                lContainer.SaveChanges();
                lScope.Complete();
            }
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
