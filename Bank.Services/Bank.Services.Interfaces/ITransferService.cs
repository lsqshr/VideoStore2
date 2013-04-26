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
        [OperationContract(IsOneWay=true)]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        void Transfer(Guid OrderNumber,double pAmount, int pFromAcctNumber, int pToAcctNumber,String pReturnAddress);
    }
}
