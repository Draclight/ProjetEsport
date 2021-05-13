using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Admin.Users
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class ApproveUsersModel : PageModel
    {
        private readonly ILogger<RegisterConfirmationModel> _logger;
        private readonly ApplicationDbContext _context;

        public ApproveUsersModel(ILogger<RegisterConfirmationModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [BindProperty]
        public IList<IdentityUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.Where(u => !u.EmailConfirmed).ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveUserAsync(string id)
        {
            //User
            if (await _context.Users.AnyAsync(u => u.Id == id))
            {
                IdentityUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                user.EmailConfirmed = true;
                _context.Attach(user).State = EntityState.Modified;

                //User role
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Name.Equals(Constants.LicenciesRole)).Id;
                newRole.UserId = id;
                _context.UserRoles.Add(newRole);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectUserAsync(string id)
        {
            //User
            if (await _context.Users.AnyAsync(u => u.Id == id))
            {
                IdentityUser user = await _context.Users.FindAsync(id);

                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage();
        }
    }
}
