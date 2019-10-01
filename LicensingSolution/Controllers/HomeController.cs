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
        public IActionResult Index()
        {
            var reportModel = new List<ReportViewModel>();
            var username = HttpContext.User.Identity.Name;

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using (var _context = new ApplicationDbContext(optionBuilder.Options))
            {
                var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);

                var months = 3;
                var drivers = _context.Drivers.Include(o => o.Owner).Where(x => x.Owner.AssociationId == user.AssociationId && x.DrivingLicence.LicenceExpiryDate.Date.AddMonths(-months) <= DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddMonths(-months) <= DateTime.Today).ToList();
                var driversQuant = _context.Drivers.Where(p=>p.Owner.AssociationId == user.AssociationId).Count();
                var operatingLicences = _context.OperatingLicences.Include(o => o.Owner).Where(y => y.Owner.AssociationId == user.AssociationId && y.ValidUntil.AddMonths(-months) <= DateTime.Today).ToList();
                var operatingLicencesQaunt = _context.OperatingLicences.Where(p => p.Owner.AssociationId == user.AssociationId).Count();
                var vehicleLicences = _context.VehicleLicences.Include(o => o.Owner).Where(y => y.Owner.AssociationId == user.AssociationId && y.DateOfExpiry.AddMonths(-months) <= DateTime.Today).ToList();
                var vehicleLicencesQuant = _context.VehicleLicences.Where(p => p.Owner.AssociationId == user.AssociationId).Count();
                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Driver's Licences",
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
