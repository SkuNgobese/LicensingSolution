using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicensingSolution.Data;
using LicensingSolution.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LicensingSolution.Controllers
{
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public DriversController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = environment;
            _userManager = userManager;
        }

        // GET: Drivers
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            var drivers = new List<Driver>();

            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Superuser"))
            {
                drivers = await _context.Drivers
                    .Include(l => l.DrivingLicence)
                    .Include(v => v.Owner).ToListAsync();
            }
            else
            {
                drivers = await _context.Drivers
                    .Include(l => l.DrivingLicence)
                    .Include(v => v.Owner)
                    .Where(x => x.Owner.AssociationId == user.AssociationId).ToListAsync();
            }

            return View(drivers);
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(l => l.DrivingLicence)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver driver, IFormFile image)
        {
            var owner = await _context.Owners.FindAsync(driver.OwnerId);
            if (DriverExists(driver.DriverId))
            {
                ModelState.AddModelError("DriverId", $"Driver's ID Number: {driver.DriverId} already exist");
            }
            if (DrivingLicenceExists(driver.DrivingLicence.LicenceNumber))
            {
                ModelState.AddModelError("DrivingLicence.LicenceNumber", $"Licence Number: {driver.DrivingLicence.LicenceNumber} already exist");
            }
            if (owner == null)
            {
                ModelState.AddModelError("OwnerId", "Owner with this ID Number does not exist");
            }
            if (image != null)
            {
                var ext = Path.GetExtension(image.FileName);

                if (ext != ".jpg" && ext != ".png" && ext != ".jpeg")
                {
                    ModelState.AddModelError("", "Allowed image formats are .jpg/.png/.jpeg");
                }
                if (image.Length > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("", "You can not upload picture more than 2MB");
                }
            }
            if (ModelState.IsValid)
            {
                if (image.Length > 512)
                {
                    FileInfo fileInfo = new FileInfo(image.FileName);
                    var newImagename = driver.FirstName + (DateTime.Now.Ticks / 10) % 100000000 + driver.LastName + fileInfo.Extension;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ImageFiles\" + newImagename);
                    var pathToSave = @"/ImageFiles/" + newImagename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    driver.ImgPath = pathToSave;
                }
                driver.Owner = owner;
                _context.Add(driver);
                driver.DrivingLicence.Driver = driver;
                _context.Add(driver.DrivingLicence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(l => l.DrivingLicence)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Driver driver)
        {
            if (id != driver.DriverId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    driver.DrivingLicence.Driver = driver;
                    _context.Update(driver.DrivingLicence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .Include(l => l.DrivingLicence)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var driver = await _context.Drivers
                .Include(l => l.DrivingLicence)
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            _context.DrivingLicences.Remove(driver.DrivingLicence);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(string id)
        {
            return _context.Drivers.Any(e => e.DriverId == id);
        }
        private bool DrivingLicenceExists(string id)
        {
            return _context.DrivingLicences.Any(e => e.LicenceNumber == id);
        }
    }
}
