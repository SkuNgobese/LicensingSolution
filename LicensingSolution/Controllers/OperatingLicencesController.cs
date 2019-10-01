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
using Microsoft.AspNetCore.Identity;

namespace LicensingSolution.Controllers
{
    public class OperatingLicencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OperatingLicencesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: OperatingLicences
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            var operatingLicences = new List<OperatingLicence>();

            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Superuser"))
            {
                operatingLicences = await _context.OperatingLicences
                    .Include(v => v.Owner).ToListAsync();
            }
            else
            {
                operatingLicences = await _context.OperatingLicences
                    .Include(v => v.Owner)
                    .Where(x => x.Owner.AssociationId == user.AssociationId).ToListAsync();
            }

            return View(operatingLicences);
        }

        // GET: OperatingLicences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingLicence = await _context.OperatingLicences
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.VehRegistrationNumber == id);
            if (operatingLicence == null)
            {
                return NotFound();
            }

            return View(operatingLicence);
        }

        // GET: OperatingLicences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OperatingLicences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OperatingLicence operatingLicence)
        {
            if (OperatingLicenceExists(operatingLicence.OperatingLicenceNumber))
            {
                ModelState.AddModelError("OperatingLicenceNumber", $"Operating Licence Number: {operatingLicence.OperatingLicenceNumber} already exist");
            }
            var owner = await _context.Owners.FindAsync(operatingLicence.OwnerId);
            if (owner == null)
            {
                ModelState.AddModelError("OwnerId", "Owner with this ID Number does not exist");
            }
            if (ModelState.IsValid)
            {
                operatingLicence.Owner = owner;
                _context.Add(operatingLicence);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            
            return View(operatingLicence);
        }

        // GET: OperatingLicences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingLicence = await _context.OperatingLicences
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.VehRegistrationNumber == id);
            if (operatingLicence == null)
            {
                return NotFound();
            }
            return View(operatingLicence);
        }

        // POST: OperatingLicences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OperatingLicenceNumber,ApplicationNumber,AssociationName,VehRegistrationNumber,EngineNumber,VehMass,Manufacturer,VehDescription,Passengers,YearOfReg,ValidFrom,ValidUntil,OwnerId")] OperatingLicence operatingLicence)
        {
            if (id != operatingLicence.VehRegistrationNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operatingLicence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatingLicenceExists(operatingLicence.OperatingLicenceNumber))
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
            return View(operatingLicence);
        }

        // GET: OperatingLicences/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingLicence = await _context.OperatingLicences
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.VehRegistrationNumber == id);
            if (operatingLicence == null)
            {
                return NotFound();
            }

            return View(operatingLicence);
        }

        // POST: OperatingLicences/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var operatingLicence = await _context.OperatingLicences.FindAsync(id);
            _context.OperatingLicences.Remove(operatingLicence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatingLicenceExists(string id)
        {
            return _context.OperatingLicences.Any(e => e.OperatingLicenceNumber == id);
        }
    }
}
