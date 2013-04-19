using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace EmailService.Process
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost lHost = new ServiceHost(typeof(EmailService.Services.EmailService)))
            {
                lHost.Open();
                Console.WriteLine("Email Service Started");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
        }
    }
}
