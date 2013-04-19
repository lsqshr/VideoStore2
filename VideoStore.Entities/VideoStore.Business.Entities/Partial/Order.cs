using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.Business.Entities
{
    public partial class Order
    {
        public void UpdateStockLevels()
        {
            foreach (OrderItem lItem in this.OrderItems)
            {
                if (lItem.Media.Stocks.Quantity - lItem.Quantity >= 0)
                {
                    lItem.Media.Stocks.Quantity -= lItem.Quantity;
                }
                else
                {
                    throw new Exception("Cannot place an order - no more stock for media item");
                }
            }
        }
    }
}
