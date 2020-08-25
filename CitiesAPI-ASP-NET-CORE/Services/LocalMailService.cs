using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.ASP.NET.CORE.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        //private string _mailTo = "tta2yta@gmail.com";
       // private string _mailFrom = "m1909652@edu.misis.ru";

        public LocalMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void send(string subject, string message)
        {
            /*Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}  with Local Service");
            Debug.WriteLine($"Subject :{subject}");
            Debug.WriteLine($"Message: {message}");*/

            Debug.WriteLine($"Mail from {_configuration["mailSettings:mailToAdsress"]} to {_configuration["mailSettings:mailFromAdress"]}  with Local Service");
            Debug.WriteLine($"Subject :{subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }


}
