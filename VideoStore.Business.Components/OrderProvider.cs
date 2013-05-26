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
                return lContainer.Orders.Include("Customer").Include("OrderItems.Media.Stocks").FirstOrDefault((pOrder) => pOrder.OrderNumber == tOrderNumber );
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

                    // update the stock levels for this order before the fund is transferred, 
                    //because the items should be reserved for this user
                    // if there is anything wrong with the fund transfer, the stock levels will be added back
                    pOrder.UpdateStockLevels();
                    TransferFundsFromCustomer(pOrder.OrderNumber, pOrder.Customer.BankAccountNumber, pOrder.Total ?? 0.0);
                    Console.WriteLine("Fund transfer of order: " + pOrder.OrderNumber + " has been requested to the bank");
                    lContainer.Orders.ApplyChanges(pOrder);
                    lContainer.SaveChanges();
                }
                catch (Exception lException)
                {
                    SendTransferErrorEmail(pOrder.OrderNumber, lException.Message);
                    Console.WriteLine("Something wrong happened. The fund transferr request were not able to be placed.");
                    Console.WriteLine( "Exception Message:" + lException.Message);
                    throw;
                }
                finally { 
                    lScope.Complete();
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

        public void HandleBankNotification(Guid OrderNumber, String pMessage)
        { 
            Order pOrder = this.FindOrderbyOrderNumber(OrderNumber);
            if (pMessage == "Success")
            {
                //request to start delivery
                this.PlaceDeliveryForOrder(pOrder);
                this.SendDeliverySubmittedEmail(OrderNumber);
            }
            else
            {
                //if the transfer fails, send email to the customer with fail message
                this.SendTransferErrorEmail(OrderNumber, pMessage);

                using (TransactionScope lScope = new TransactionScope())
                using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
                {
                    Console.Write("Calling Compensator to restore Stock levels");
                    // update the Stock by adding the stock quantity back
                    pOrder.CompensateStockLevels();
                    //lContainer.Orders.ApplyChanges(pOrder);

                    // see if the compensator works before saving changes
                    foreach(OrderItem item in pOrder.OrderItems){
                        lContainer.Stocks.Attach(item.Media.Stocks);
                        lContainer.ObjectStateManager.ChangeObjectState(item.Media.Stocks,System.Data.EntityState.Modified);
                        //lContainer.Stocks.ApplyChanges(item.Media.Stocks);
                    }
                    //lContainer.Orders.ApplyChanges(pOrder);
                    /*lContainer.ObjectStateManager.ChangeObjectState(pOrder,
                        System.Data.EntityState.Modified);
                    lContainer.Orders.ApplyChanges(pOrder);*/
                    lContainer.SaveChanges();
                    lScope.Complete();
                    Console.WriteLine("Easy! Stock levels have been restored.");
                }
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

                // update the number of stock corresponding to this order and save the delivery with the order
                //pOrder.UpdateStockLevels();  We are not updating the stock levels with putting deliver any more 
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
            }
        }


        private int RetrieveVideoStoreAccountNumber()
        {
            return 123;
        }

    }
}
