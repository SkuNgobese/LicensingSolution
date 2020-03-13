﻿using LicensingSolution.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LicensingSolution.Models.Services
{
    public class Reminder : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        [TempData]
        public string ReminderTime { get; set; }
        public Reminder(IConfiguration configuration, IEmailSender emailSender, ISmsSender smsSender, ILogger<Reminder> logger)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time to send reminders");
            Task.Run(TaskRoutineAsync, cancellationToken);
            _logger.LogInformation("Sent");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task TaskRoutineAsync()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            
            while (true)
            {
                using (var _context = new ApplicationDbContext(optionBuilder.Options))
                {
                    var config = _context.Config.FirstOrDefault(p => p.Name == "ReminderTime");
                    ReminderTime = config.Value;
                }
                var now = DateTime.Now.TimeOfDay.ToString(@"hh\:mm");
                if (now == ReminderTime)
                {
                    await SendReminder("driving licence", 30);
                    await SendReminder("operating licence", 30);
                    await SendReminder("licence disc", 30);
                    await SendReminder("driving licence", 7);
                    await SendReminder("operating licence", 7);
                    await SendReminder("licence disc", 7);
                }
                DateTime nextStop = DateTime.Now.AddMinutes(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }

        public async Task SendReminder(string licenceType, int days)
        {
            var owners = new List<Owner>();
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using (var _context = new ApplicationDbContext(optionBuilder.Options))
            {
                if (licenceType.ToLower() == "driving licence")
                {
                    var drivers = await _context.Drivers.Include(o => o.Owner).Where(x => x.DrivingLicence.LicenceExpiryDate.Date.AddDays(-days) == DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddDays(-days) == DateTime.Today).ToListAsync();
                    foreach (var driver in drivers)
                    {
                        var owner = driver.Owner;
                        if (owner.EmailAddress != "")
                        {
                            await _emailSender.SendEmailAsync(owner.EmailAddress, $"{licenceType} expires soon", Message(owner.FirstName, days, licenceType, driver.FullName));
                        }
                        //if (owner.PrimaryContactNumber != "")
                        //{
                        //    await _smsSender.SendSmsAsync(owner.PrimaryContactNumber, Message(owner.FirstName, days, licenceType, driver.FullName));
                        //}
                    }
                }
                if (licenceType.ToLower() == "operating licence")
                {
                    var operatingLicences = await _context.OperatingLicences.Include(o => o.Owner).Where(y => y.ValidUntil.AddDays(-days) == DateTime.Today).ToListAsync();
                    if (operatingLicences != null)
                    {
                        foreach (var operatingLicence in operatingLicences)
                        {
                            var owner = operatingLicence.Owner;
                            if (owner.EmailAddress != "")
                            {
                                await _emailSender.SendEmailAsync(owner.EmailAddress, $"{licenceType} expires soon", Message(owner.FirstName, days, licenceType,"", operatingLicence.VehRegistrationNumber));
                            }
                            //if (owner.PrimaryContactNumber != "")
                            //{
                            //    await _smsSender.SendSmsAsync(owner.PrimaryContactNumber, Message(owner.FirstName, days, licenceType, "", operatingLicence.VehRegistrationNumber));
                            //}
                        }
                    }
                }
                if (licenceType.ToLower() == "licence disc")
                {
                    var vehicleLicences = await _context.VehicleLicences.Include(o => o.Owner).Where(y => y.DateOfExpiry.AddDays(-days) == DateTime.Today).ToListAsync();
                    if (vehicleLicences != null)
                    {

                        foreach (var vehicleLicence in vehicleLicences)
                        {
                            var owner = vehicleLicence.Owner;
                            var regno = vehicleLicence.VehLicenceNumber;
                            if (owner.EmailAddress != "")
                            {
                                await _emailSender.SendEmailAsync(owner.EmailAddress, $"{licenceType} expires soon", Message(owner.FirstName, days, licenceType, "", vehicleLicence.VehLicenceNumber));
                            }
                            //if (owner.PrimaryContactNumber != "")
                            //{
                            //    await _smsSender.SendSmsAsync(owner.PrimaryContactNumber, Message(owner.FirstName, days, licenceType, "", vehicleLicence.VehLicenceNumber));
                            //}
                        }
                    }
                }
            }
        }

        public string Message(string fname, int days, string licenceType, string drivername = "", string regnumber = "")
        {
            if (licenceType.ToLower() == "driving licence")
            {
                licenceType = $"{licenceType}/PDP";
                return $"Dear {fname},<br><br>This is to inform you that the {licenceType} for {drivername} is due for renewal in {days} days.<br><br><br>Best Regards,<br><br>Licence Solution Team";
            }
            if (licenceType.ToLower() == "operating licence")
            {
                return $"Dear {fname},<br><br>This is to inform you that the {licenceType} for {regnumber} is due for renewal in {days} days.<br><br><br>Best Regards,<br><br>Licence Solution Team";
            }
            if (licenceType.ToLower() == "licence disc")
            {
                return $"Dear {fname},<br><br>This is to inform you that the {licenceType} for {regnumber} is due for renewal in {days} days.<br><br><br>Best Regards,<br><br>Licence Solution Team";
            }
            return null;
        }
    }
}
