using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IStockProvider
    {
        void SellStock(Business.Entities.Stock pStock, int pQuantity);
    }
}
