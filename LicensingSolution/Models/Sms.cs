using LicensingSolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace LicensingSolution.Models
{
    public class Sms : IHostedService
    {
        private readonly ApplicationDbContext _context;
        public Sms(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(TaskRoutineAsync, cancellationToken);
            return Task.CompletedTask;
        }
        private async Task TaskRoutineAsync()
        {
            while(true)
            {
                await SendSms("driving licence", 30);

                DateTime nextStop = DateTime.Now.AddMinutes(60);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);              
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task Stopped");

            return Task.FromCanceled(cancellationToken);
        }
        public async Task<Task> SendSms(string licenceType, int days)
        {
            var owners = new List<Owner>();
            if (licenceType.ToLower() == "driving licence")
            {
                var drivers = await _context.Drivers.Where(x => x.DrivingLicence.LicenceExpiryDate.AddDays(-days) == DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddDays(-days) == DateTime.Today).ToListAsync();
                foreach (var driver in drivers)
                {
                    owners.Add(driver.Owner);
                }
            }
            if (licenceType.ToLower() == "operating licence")
            {
                var operatingLicences = await _context.OperatingLicences.Where(y => y.ValidUntil.AddDays(-days) == DateTime.Today).ToListAsync();
                if (operatingLicences != null)
                {
                    foreach (var ol in operatingLicences)
                    {
                        owners.Add(ol.Owner);
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
                        owners.Add(vl.Owner);
                    }
                }
            }
            
            if (owners != null)
            {
                foreach (var owner in owners)
                {
                    if (owner.PrimaryContactNumber != "")
                    {
                        string accountSid = "";
                        string authToken = "";
                        TwilioClient.Init(accountSid, authToken);
                        var message = MessageResource.Create(
                            body: Message(owner.FirstName, days, licenceType),
                            from: new Twilio.Types.PhoneNumber("+27640617805"),
                            to: new Twilio.Types.PhoneNumber(owner.PrimaryContactNumber));
                    };
                }                
            }
            return null;
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
