using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models.Services
{
    public class EmailSenderOptions
    {
        public string Email { get; set; }
        public string SendGridUser { get; set; }
        public string SENDGRID_API_KEY { get; set; }
    }
}
