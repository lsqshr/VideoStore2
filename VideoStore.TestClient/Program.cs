using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerService.CustomerServiceClient lClient = new CustomerService.CustomerServiceClient();
            lClient.CreateCustomer(new CustomerService.Customer() {  Name = "Matt", Address="1 Reynolds Street", Orders = new CustomerService.Order[0]});
        }
    }
}
