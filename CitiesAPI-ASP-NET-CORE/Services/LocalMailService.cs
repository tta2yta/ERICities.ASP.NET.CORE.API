using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "tta2yta@gmail.com";
        private string _mailFrom = "m1909652@edu.misis.ru";
        public void send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}  with Local Service");
            Debug.WriteLine($"Subject :{subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }


}
