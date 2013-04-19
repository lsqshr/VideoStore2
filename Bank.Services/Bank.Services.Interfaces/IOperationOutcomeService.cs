using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Entities;

namespace Bank.Services.Interfaces
{
    public interface IOperationOutcomeService
    {
        void NotifyOperationOutcome(OperationOutcome pOutcome); 
    }
}
