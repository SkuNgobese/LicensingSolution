using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicensingSolution.Data;
using LicensingSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace LicensingSolution.Controllers
{
    [Authorize]
    public class DrivingLicencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DrivingLicencesController(ApplicationDbContext context, IFileProvider fileProvider, IHostingEnvironment environment)
        {
            _context = context;
            _fileProvider = fileProvider;
            _hostingEnvironment = environment;
        }

        // GET: DrivingLicences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DrivingLicences.Include(d => d.Driver);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DrivingLicences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drivingLicence = await _context.DrivingLicences
                .Include(d => d.Driver)
                .FirstOrDefaultAsync(m => m.LicenceNumber == id);
            if (drivingLicence == null)
            {
                return NotFound();
            }

            return View(drivingLicence);
        }

        // GET: DrivingLicences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrivingLicences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrivingLicence drivingLicence, IFormFile image)
        {
            var owner = await _context.Owners.FindAsync(drivingLicence.Driver.OwnerId);
            if (owner == null)
            {
                ModelState.AddModelError("", "Owner with this ID Number does not exist in the system");
            }
            if (DriverExists(drivingLicence.Driver.DriverId))
            {
                ModelState.AddModelError("Driver.IDNumber", $"Driver's ID Number: {drivingLicence.Driver.DriverId} already exist");
            }
            if (DrivingLicenceExists(drivingLicence.LicenceNumber))
            {
                ModelState.AddModelError("LicenceNumber", $"Licence Number: {drivingLicence.LicenceNumber} already exist");
            }
            if(image != null)
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
                    var newImagename = drivingLicence.Driver.FirstName + (DateTime.Now.Ticks/10) %100000000 + drivingLicence.Driver.LastName + fileInfo.Extension;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ImageFiles\" + newImagename);
                    var pathToSave = @"/ImageFiles/" + newImagename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    drivingLicence.Driver.ImgPath = pathToSave;
                }

                _context.Add(drivingLicence);
                drivingLicence.Driver.Owner = owner;
                _context.Add(drivingLicence.Driver);
                await _context.SaveChangesAsync();                            

                return RedirectToAction(nameof(Index));
            }
            return View(drivingLicence);
        }

        // GET: DrivingLicences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drivingLicence = await _context.DrivingLicences.FindAsync(id);
            if (drivingLicence == null)
            {
                return NotFound();
            }
            return View(drivingLicence);
        }

        // POST: DrivingLicences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LicenceNumber,LicenceExpiryDate,PDPExpiryDate,IDNumber")] DrivingLicence drivingLicence)
        {
            if (id != drivingLicence.LicenceNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drivingLicence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrivingLicenceExists(drivingLicence.LicenceNumber))
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
            return View(drivingLicence);
        }

        // GET: DrivingLicences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drivingLicence = await _context.DrivingLicences
                .Include(d => d.Driver)
                .FirstOrDefaultAsync(m => m.LicenceNumber == id);
            if (drivingLicence == null)
            {
                return NotFound();
            }

            return View(drivingLicence);
        }

        // POST: DrivingLicences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drivingLicence = await _context.DrivingLicences.FindAsync(id);
            _context.DrivingLicences.Remove(drivingLicence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrivingLicenceExists(string id)
        {
            return _context.DrivingLicences.Any(e => e.LicenceNumber == id);
        }

        private bool DriverExists(string id)
        {
            return _context.Drivers.Any(e => e.DriverId == id);
        }
    }
}
