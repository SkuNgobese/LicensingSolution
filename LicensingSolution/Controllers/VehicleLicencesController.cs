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
    public class VehicleLicencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public VehicleLicencesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VehicleLicences
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            var vehicleLicences = new List<VehicleLicence>();

            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Superuser"))
            {
                vehicleLicences = await _context.VehicleLicences
                    .Include(v => v.Owner).ToListAsync();
            }
            else
            {
                vehicleLicences = await _context.VehicleLicences
                    .Include(v => v.Owner)
                    .Where(x => x.Owner.AssociationId == user.AssociationId).ToListAsync();
            }

            return View(vehicleLicences);
        }

        // GET: VehicleLicences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleLicence = await _context.VehicleLicences
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VehRegisterNumber == id);
            if (vehicleLicence == null)
            {          
                return NotFound();
            }

            return View(vehicleLicence);
        }

        // GET: VehicleLicences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleLicences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleLicence vehicleLicence)
        {
            if (VehicleLicenceExists(vehicleLicence.VehRegisterNumber))
            {
                ModelState.AddModelError("LicenceNumber", $"Register Number: {vehicleLicence.VehRegisterNumber} already exist");
            }
            var owner = await _context.Owners.FindAsync(vehicleLicence.OwnerId);
            if (owner == null)
            {
                ModelState.AddModelError("OwnerId", "Owner with this ID Number does not exist");
            }
            if (ModelState.IsValid)
            {
                vehicleLicence.Owner = owner;
                _context.Add(vehicleLicence);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vehicleLicence);
        }

        // GET: VehicleLicences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleLicence = await _context.VehicleLicences.FindAsync(id);
            if (vehicleLicence == null)
            {
                return NotFound();
            }
            return View(vehicleLicence);
        }

        // POST: VehicleLicences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VehRegisterNumber,VehLicenceNumber,VehIdentificationNumber,VehMake,RegisteringAuthority,DateOfExpiry,OwnerId")] VehicleLicence vehicleLicence)
        {
            if (id != vehicleLicence.VehRegisterNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleLicence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleLicenceExists(vehicleLicence.VehRegisterNumber))
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
            return View(vehicleLicence);
        }

        // GET: VehicleLicences/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleLicence = await _context.VehicleLicences
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VehRegisterNumber == id);
            if (vehicleLicence == null)
            {
                return NotFound();
            }

            return View(vehicleLicence);
        }

        // POST: VehicleLicences/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehicleLicence = await _context.VehicleLicences.FindAsync(id);
            _context.VehicleLicences.Remove(vehicleLicence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleLicenceExists(string id)
        {
            return _context.VehicleLicences.Any(e => e.VehRegisterNumber == id);
        }
    }
}
