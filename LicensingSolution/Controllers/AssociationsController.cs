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

namespace LicensingSolution.Controllers
{
    [Authorize(Roles = "Admin,Superuser")]
    public class AssociationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssociationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Associations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Associations.Include(o => o.Owners).Include(p=>p.ApplicationUsers).ToListAsync());
        }

        // GET: Associations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssociationId,Name")] Association association)
        {
            if (ModelState.IsValid)
            {
                _context.Add(association);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(association);
        }

        // GET: Associations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FindAsync(id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssociationId,Name")] Association association)
        {
            if (id != association.AssociationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(association);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.AssociationId))
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
            return View(association);
        }

        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .Include(o => o.Owners)
                .FirstOrDefaultAsync(m => m.AssociationId == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var association = await _context.Associations.Include(o=>o.Owners).FirstOrDefaultAsync(p=>p.AssociationId == id);

            foreach (var owner in association.Owners)
            {
                var drivers = await _context.Drivers.Include(d=>d.DrivingLicence).Where(p => p.OwnerId == owner.OwnerId).ToListAsync();
                foreach (var driver in drivers)
                {
                    _context.DrivingLicences.Remove(driver.DrivingLicence);
                    _context.Drivers.Remove(driver);
                }
                var operatingLicences = await _context.OperatingLicences.Where(p => p.OwnerId == owner.OwnerId).ToListAsync();
                foreach (var operatingLicence in operatingLicences)
                {
                    _context.OperatingLicences.Remove(operatingLicence);
                }
                var vehicleLicences = await _context.VehicleLicences.Where(p => p.OwnerId == owner.OwnerId).ToListAsync();
                foreach (var vehicleLicence in vehicleLicences)
                {
                    _context.VehicleLicences.Remove(vehicleLicence);
                }
                _context.Owners.Remove(owner);
            }

            _context.Associations.Remove(association);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssociationExists(int id)
        {
            return _context.Associations.Any(e => e.AssociationId == id);
        }
    }
}
