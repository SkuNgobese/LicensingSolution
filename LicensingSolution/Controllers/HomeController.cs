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

namespace LicensingSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var reportModel = new List<ReportViewModel>();
            
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using (var _context = new ApplicationDbContext(optionBuilder.Options))
            {
                var months = 3;
                var drivers = _context.Drivers.Include(o => o.Owner).Where(x => x.DrivingLicence.LicenceExpiryDate.Date.AddMonths(-months) <= DateTime.Today || x.DrivingLicence.PDPExpiryDate.AddMonths(-months) <= DateTime.Today).ToList();

                var operatingLicences = _context.OperatingLicences.Include(o => o.Owner).Where(y => y.ValidUntil.AddMonths(-months) <= DateTime.Today).ToList();

                var vehicleLicences = _context.VehicleLicences.Include(o => o.Owner).Where(y => y.DateOfExpiry.AddMonths(-months) <= DateTime.Today).ToList();

                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Driver's Licences",
                    Quantity = drivers.Count()
                });
                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Operating Licences",
                    Quantity = operatingLicences.Count()
                });
                reportModel.Add(new ReportViewModel
                {
                    DimensionOne = "Vehicle Licences",
                    Quantity = vehicleLicences.Count()
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
