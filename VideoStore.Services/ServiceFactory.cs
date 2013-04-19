using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace VideoStore.Services
{
    public class ServiceFactory
    {
        public static T GetService<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }
    }
}
