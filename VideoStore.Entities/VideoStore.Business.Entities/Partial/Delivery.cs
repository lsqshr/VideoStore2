using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.Business.Entities
{
    public enum DeliveryStatus { Submitted, Delivered, Failed }

    public partial class Delivery
    {
        public DeliveryStatus DeliveryStatus
        {
            get
            {
                return (DeliveryStatus)this.Status;
            }
            set
            {
                this.Status = (int)value;
            }
        }
    }
}
