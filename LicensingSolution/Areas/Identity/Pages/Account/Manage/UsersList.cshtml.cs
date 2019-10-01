using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicensingSolution.Data;
using LicensingSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LicensingSolution.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin,Superuser")]
    public class UsersListModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersListModel> _logger;
        public UsersListModel(UserManager<ApplicationUser> userManager, ILogger<UsersListModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public IList<ApplicationUser> Users = new List<ApplicationUser>();
        public IActionResult OnGet()
        {
            Users = _userManager.Users.Include(p=>p.Association).Where(p => p.UserName != "i.skngobese@gmail.com").ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred while deleting user with ID '{user.Id}'.");
            }
            Users = _userManager.Users.Include(p => p.Association).Where(p=>p.UserName != "i.skngobese@gmail.com").ToList();
            _logger.LogInformation("User with ID '{0}' deleted.", user.Id);
            return Page();
        }
    }
}