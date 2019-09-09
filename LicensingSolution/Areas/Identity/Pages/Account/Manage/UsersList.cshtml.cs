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
using Microsoft.Extensions.Logging;

namespace LicensingSolution.Areas.Identity.Pages.Account.Manage
{
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
        public void OnGet()
        {
            Users = _userManager.Users.ToList();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' deleted.", userId);

            return Page();
        }
    }
}