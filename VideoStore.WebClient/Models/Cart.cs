using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Business.Entities;
using VideoStore.WebClient.ViewModels;

namespace VideoStore.WebClient.Models
{
    public class Cart
    {
        private List<OrderItem> mOrderItems = new List<OrderItem>();
        public IList<OrderItem> OrderItems { get { return mOrderItems.AsReadOnly(); }}

        public void AddItem(Media pMedia, int pQuantity)
        {
            var lItem = mOrderItems.FirstOrDefault(oi => oi.Media.Id == pMedia.Id);
            if (lItem == null)
            {
                mOrderItems.Add(new OrderItem() { Media = pMedia, Quantity = pQuantity });
            }
            else
            {
                lItem.Quantity += pQuantity;
            }
        }

        public decimal ComputeTotalValue()
        {
            return mOrderItems.Sum(oi => oi.Media.Price * oi.Quantity);
        }

        public void Clear()
        {
            mOrderItems.Clear();
        }

        public void SubmitOrderAndClearCart(UserCache pUserCache)
        {
            Order lOrder = new Order();
            lOrder.OrderDate = DateTime.Now;
            lOrder.Customer = pUserCache.Model;
            foreach(OrderItem lItem in mOrderItems)
            {
                lOrder.OrderItems.Add(lItem);
            }
            lOrder.Total = Convert.ToDouble(ComputeTotalValue());

            OrderService.OrderServiceClient lOrderService = new OrderService.OrderServiceClient();

            lOrderService.SubmitOrder(lOrder);
            pUserCache.UpdateUserCache();
            Clear();
        }

        public void RemoveLine(Media pMedia)
        {
            mOrderItems.RemoveAll(oi => oi.Media.Id == pMedia.Id);
        }
    }
}