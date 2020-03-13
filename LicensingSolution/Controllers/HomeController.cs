using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicensingSolution.Models;
using Microsoft.AspNetCore.Authorization;
using LicensingSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using LicensingSolution.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace LicensingSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var reportModel = new List<ReportViewModel>();
            var username = HttpContext.User.Identity.Name;

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using (var _context = new ApplicationDbContext(optionBuilder.Options))
            {
                var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
                
                var months = 3;
                var drivers = await _context.Drivers.Include(o => o.Owner).Where(x => x.Owner.AssociationId == user.AssociationId && (x.DrivingLicence.LicenceExpiryDate.Date.AddMonths(-months) <= DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddMonths(-months) <= DateTime.Today)).ToListAsync();
                var driversQuant = await _context.Drivers.Where(p=>p.Owner.AssociationId == user.AssociationId).CountAsync();
                var operatingLicences = await _context.OperatingLicences.Include(o => o.Owner).Where(y => y.Owner.AssociationId == user.AssociationId && y.ValidUntil.AddMonths(-months) <= DateTime.Today).ToListAsync();
                var operatingLicencesQaunt = await _context.OperatingLicences.Where(p => p.Owner.AssociationId == user.AssociationId).CountAsync();
                var vehicleLicences = await _context.VehicleLicences.Include(o => o.Owner).Where(y => y.Owner.AssociationId == user.AssociationId && y.DateOfExpiry.AddMonths(-months) <= DateTime.Today).ToListAsync();
                var vehicleLicencesQuant = await _context.VehicleLicences.Where(p => p.Owner.AssociationId == user.AssociationId).CountAsync();
                
                if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Superuser"))
                {
                    drivers = await _context.Drivers.Include(o => o.Owner).Where(x => x.DrivingLicence.LicenceExpiryDate.Date.AddMonths(-months) <= DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddMonths(-months) <= DateTime.Today).ToListAsync();
                    driversQuant = await _context.Drivers.CountAsync();
                    operatingLicences = await _context.OperatingLicences.Include(o => o.Owner).Where(y => y.ValidUntil.AddMonths(-months) <= DateTime.Today).ToListAsync();
                    operatingLicencesQaunt = await _context.OperatingLicences.CountAsync();
                    vehicleLicences = await _context.VehicleLicences.Include(o => o.Owner).Where(y => y.DateOfExpiry.AddMonths(-months) <= DateTime.Today).ToListAsync();
                    vehicleLicencesQuant = await _context.VehicleLicences.CountAsync();
                }

                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Driver Operators",
                    Quantity = drivers.Count(),
                    TotalQuantity = driversQuant

                });
                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Operating Licences",
                    Quantity = operatingLicences.Count(),
                    TotalQuantity = operatingLicencesQaunt
                });
                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Vehicle Licences",
                    Quantity = vehicleLicences.Count(),
                    TotalQuantity = vehicleLicencesQuant
                });
            }    
            return View(reportModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
