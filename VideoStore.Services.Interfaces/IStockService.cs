using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using VideoStore.Business.Entities;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        void SellStock(Stock pStock, int pQuantity);
    }
}
