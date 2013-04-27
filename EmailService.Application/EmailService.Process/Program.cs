using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Messaging;

namespace EmailService.Process
{
    class Program
    {
        private static readonly String sNotifyQueuePath = ".\\private$\\EmailService";

        static void Main(string[] args)
        {
            using (ServiceHost lHost = new ServiceHost(typeof(EmailService.Services.EmailService)))
            {
                EnsureMessageQueuesExists();
                lHost.Open();
                Console.WriteLine("Email Service Started");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
        }

        private static void EnsureMessageQueuesExists()
        {
            // Create the transacted MSMQ queue if necessary.
            if (!MessageQueue.Exists(sNotifyQueuePath))
                MessageQueue.Create(sNotifyQueuePath, true);
        }
    }
}
