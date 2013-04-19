using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Bank.Services.Interfaces
{
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Transfer(double pAmount, int pFromAcctNumber, int pToAcctNumber);
    }
}
