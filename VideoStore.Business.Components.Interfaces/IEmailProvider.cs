using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoStore.Business.Components.Interfaces
{
    public class EmailMessage
    {
        public String ToAddress { get; set; }
        public String Message { get; set;}
    }

    public interface IEmailProvider
    {
        void SendMessage(EmailMessage pMessage);
    }
}
