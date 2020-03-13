using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models.Services
{
    public class SMSSender:ISmsSender
    {
        public SMSSender(IOptions<SMSSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public SMSSenderOptions Options { get; }  // set only via Secret Manager

        public Task SendSmsAsync(string number, string message)
        {
            ASPSMS.SMS SMSSender = new ASPSMS.SMS
            {
                Userkey = Options.SMSAccountIdentification,
                Password = Options.SMSAccountPassword,
                Originator = Options.SMSAccountFrom
            };

            SMSSender.AddRecipient(number);
            SMSSender.MessageData = message;

            SMSSender.SendTextSMS();

            return Task.FromResult(0);
        }
    }
}
