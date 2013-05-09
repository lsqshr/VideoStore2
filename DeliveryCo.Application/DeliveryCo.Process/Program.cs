using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DeliveryCo.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;
using System.Messaging;

namespace DeliveryCo.Process
{
    class Program
    {
        private static readonly String sDeliveryCoQueue = ".\\private$\\DeliveryQ";

        static void Main(string[] args)
        {
            ResolveDependencies();
            EnsureQueueExists();
            
            using (ServiceHost lHost = new ServiceHost(typeof(DeliveryService)))
            {
                lHost.Open();
                Console.WriteLine("Delivery Service started. Press Q to quit");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }

        }

        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
        // used from lab code (TradeMessageBus)
        private static void EnsureQueueExists()
        {
            // Create the transacted MSMQ queue if necessary.
            if (!MessageQueue.Exists(sDeliveryCoQueue))
                MessageQueue.Create(sDeliveryCoQueue, true);
        }
    }
}
