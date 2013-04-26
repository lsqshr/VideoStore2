using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;

namespace VideoStore.Services
{

    class NotifyService : INotifyService
    {
        public void NotifyOperationOutcome(Guid OrderId , DeliveryInfoStatus Status , String Message) {
            Console.WriteLine("result received: Order Id " + 
                        OrderId.ToString() + Status.ToString() + "\nmsg: " +Message);
        }
    }
}
