using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApiCore.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subjec, string message)
        {
            Debug.WriteLine($"Mial from {_mailFrom} to {_mailTo}");
            Debug.WriteLine($"subject {subjec}");
            Debug.WriteLine($"message {message}");
        }
    }
}
