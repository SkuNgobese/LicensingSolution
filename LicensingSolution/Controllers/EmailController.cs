using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LicensingSolution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using LicensingSolution.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LicensingSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller, IHostedService
    {
        //private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public EmailController( IConfiguration configuration, ILogger<EmailController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time to send email reminders");
            Task.Run(TaskRoutineAsync, cancellationToken);

            return Task.CompletedTask;
        }

        private async Task TaskRoutineAsync()
        {                                   
            while (true)
            {
                var now = DateTime.Now.TimeOfDay.ToString(@"hh\:mm");
                if (now == "12:00")
                {
                    await PostEmail("vehicle licence", 30);
                    await PostEmail("operating licence", 30);
                    await PostEmail("vehicle licence", 30);
                    await PostEmail("vehicle licence", 7);
                    await PostEmail("operating licence", 7);
                    await PostEmail("vehicle licence", 7);
                }
                DateTime nextStop = DateTime.Now.AddMinutes(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task PostEmail(string licenceType, int days)
        {
            var owners = new List<Owner>();
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using(var _context = new ApplicationDbContext(optionBuilder.Options))
            {
                if (licenceType.ToLower() == "driving licence")
                {
                    var drivers = await _context.Drivers.Where(x => x.DrivingLicence.LicenceExpiryDate.AddDays(-days) == DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddDays(-days) == DateTime.Today).ToListAsync();
                    foreach (var driver in drivers)
                    {
                        var owner = await _context.Owners.FindAsync(driver.OwnerId);
                        owners.Add(owner);
                    }
                }
                if (licenceType.ToLower() == "operating licence")
                {
                    var operatingLicences = await _context.OperatingLicences.Where(y => y.ValidUntil.AddDays(-days) == DateTime.Today).ToListAsync();
                    if (operatingLicences != null)
                    {
                        foreach (var ol in operatingLicences)
                        {
                            var owner = await _context.Owners.FindAsync(ol.OwnerId);
                            owners.Add(owner);
                        }
                    }
                }
                if (licenceType.ToLower() == "vehicle licence")
                {
                    var vehicleLicences = await _context.VehicleLicences.Where(y => y.DateOfExpiry.AddDays(-days) == DateTime.Today).ToListAsync();
                    if (vehicleLicences != null)
                    {
                        
                        foreach (var vl in vehicleLicences)
                        {
                            var owner = await _context.Owners.FindAsync(vl.OwnerId);
                            owners.Add(owner);
                        }
                    }
                }
            }
            
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);

            if (owners != null)
            {
                foreach (var owner in owners)
                {
                    if (owner.EmailAddress != "")
                    {
                        var msg = new SendGridMessage()
                        {
                            From = new EmailAddress(_configuration.GetSection("Username").Value, "Skhumbuzo Ngobese"),
                            Subject = $"{licenceType} expires soon",
                            PlainTextContent = Message(fname: owner.FirstName, days: days, licenceType: licenceType)
                        };
                        msg.AddTo(new EmailAddress(owner.EmailAddress, owner.FullName));
                        var response = await client.SendEmailAsync(msg);
                    }
                }
            }
        }

        public string Message(string fname, int days, string licenceType)
        {
            if (licenceType.ToLower() == "driving licence")
            {
                licenceType = $"{licenceType}/PDP";
            }
            return $"Dear {fname},\n\nThis is to remind you that {licenceType} is due for renewal in {days} days.\n\n\nRegards\n\nLicencing Solution";
        }
    }
}