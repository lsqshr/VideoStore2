using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;
using VideoStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace VideoStore.Services
{
    public class StockService : IStockService
    {
        private IStockProvider StockProvider
        {
            get
            {
                return ServiceFactory.GetService<IStockProvider>();
            }
        }

        public void SellStock(Business.Entities.Stock pStock, int pQuantity)
        {
            StockProvider.SellStock(pStock, pQuantity);
        }
    }
}
