using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Components.Interfaces;
using Bank.Business.Entities;
using System.Transactions;
using Bank.Services.Interfaces;

namespace Bank.Business.Components
{
    public class TransferProvider : ITransferProvider
    {
        public void Transfer(Guid OrderNumber,double pAmount, int pFromAcctNumber, int pToAcctNumber, String pResultReturnAddress)
            {
            using (TransactionScope lScope = new TransactionScope())
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {
                IBankNotificationService lOutcomeService = OperationOutcomeServiceFactory.GetOperationOutcomeService(pResultReturnAddress);
                try
                {
                    Console.WriteLine("Trying to make a new transaction.");
                    Account lFromAcct = GetAccountFromNumber(pFromAcctNumber);
                    Account lToAcct = GetAccountFromNumber(pToAcctNumber);
                    lFromAcct.Withdraw(pAmount);
                    lToAcct.Deposit(pAmount);
                    lContainer.Attach(lFromAcct);
                    lContainer.Attach(lToAcct);
                    lContainer.ObjectStateManager.ChangeObjectState(lFromAcct, System.Data.EntityState.Modified);
                    lContainer.ObjectStateManager.ChangeObjectState(lToAcct, System.Data.EntityState.Modified);
                    lContainer.SaveChanges();
                    Console.WriteLine("Transfer Transaction Done");
                    lOutcomeService.NotifyOperationOutcome(OrderNumber, 
                        DeliveryInfoStatus.Successful, "Success");
                    Console.WriteLine("successful message sent!"+OrderNumber.ToString());
                    lScope.Complete();
                }
                catch (Exception lException)
                {
                    
                    Console.WriteLine("Error occured while transferring money:  " + lException.Message);
                    lOutcomeService.NotifyOperationOutcome(OrderNumber, DeliveryInfoStatus.Failed,
                        "Error occured while transferring money:  " + lException.Message);
                    Console.WriteLine("fail messsage sent!"+OrderNumber.ToString());
                    lScope.Complete();
                    lScope.Dispose();      
                    //throw;
                }
            }
        }

        private Account GetAccountFromNumber(int pToAcctNumber)
        {
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {
                return lContainer.Accounts.Where((pAcct) => (pAcct.AccountNumber == pToAcctNumber)).FirstOrDefault();
            }
        }


    }
}
