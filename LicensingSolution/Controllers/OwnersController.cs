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
using Newtonsoft.Json;

namespace LicensingSolution.Controllers
{
    public class OwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public void PopulateAssociationsDropdownList()
        {
            var AssList = new List<SelectListItem>();
            var associationsQuery = (from a in _context.Associations
                                     orderby a.Name
                                     select a).AsNoTracking();
            foreach (var m in associationsQuery)
            {
                AssList.Add(new SelectListItem { Value = (m.AssociationId.ToString()), Text = m.Name });
            }
            TempData["AssList"] = AssList;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Owners
                .Include(a => a.Association)
                .Include(d => d.Drivers)
                .Include(o => o.OperatingLicences)
                .Include(v => v.VehicleLicences);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }
        // GET: Owners/Create
        public IActionResult Create()
        {
            PopulateAssociationsDropdownList();
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner owner)
        {
            if (OwnerExists(owner.OwnerId))
            {
                ModelState.AddModelError("OwnerId", $"Owner's ID Number: {owner.OwnerId} already exist");
            }
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssociationsDropdownList();
            return View(owner);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            if (owner == null)
            {
                return NotFound();
            }
            PopulateAssociationsDropdownList();
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Owner owner)
        {
            if (id != owner.OwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.OwnerId))
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
            PopulateAssociationsDropdownList();
            return View(owner);
        }

        // GET: Owners/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [Authorize(Roles = "Admin,Superuser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var owner = await _context.Owners
                .Include(a => a.Association)
                .Include(d => d.Drivers)
                .Include(o => o.OperatingLicences)
                .Include(v => v.VehicleLicences)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            foreach(var driver in owner.Drivers)
            {
                _context.DrivingLicences.Remove(driver.DrivingLicence);
                _context.Drivers.Remove(driver);
            }
            foreach (var operatingLicence in owner.OperatingLicences)
            {
                _context.OperatingLicences.Remove(operatingLicence);
            }
            foreach (var vehicleLicence in owner.VehicleLicences)
            {
                _context.VehicleLicences.Remove(vehicleLicence);
            }
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public JsonResult IDExists(string idNo)
        {
            var isExist = _context.Owners.ToList().Find(p => p.OwnerId == idNo);
            if (isExist != null)
            {
                return Json(data: $"ID Number {idNo} is already exist.");
            }
            return Json(data: true);
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public IActionResult EmailExists(string email)
        {
            var isExist = _context.Owners.ToList().Find(p => p.EmailAddress == email);
            if(isExist != null)
            {
                return Json(data: $"Email {email} is already in use.");
            }            
            return Json(data: true);
        }

        private bool OwnerExists(string id)
        {
            return _context.Owners.Any(e => e.OwnerId == id);
        }
    }
}
