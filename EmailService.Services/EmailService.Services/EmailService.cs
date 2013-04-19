using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailService.Services.Interfaces;

namespace EmailService.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Business.Entities.EmailMessage pMessage)
        {
            Console.WriteLine("Sending email to " + pMessage.ToAddresses + ": " + pMessage.Message);
        }
    }
}
