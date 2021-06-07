using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "Administrateur")]
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<DeleteModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public Licencie Licencie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = await _context.Licencies
                .Include(l => l.Utilisateur).FirstOrDefaultAsync(m => m.ID == id);

            if (Licencie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = await _context.Licencies.Include(l => l.Utilisateur).FirstOrDefaultAsync(l => l.ID.Equals(id));

            if (Licencie != null)
            {
                try
                {
                    var isAdmin = await _userManager.IsInRoleAsync(Licencie.Utilisateur, Constants.AdministrateursRole);
                    if (isAdmin)
                    {
                        return RedirectToPage();
                    }
                    else
                    {
                        _context.Licencies.Remove(Licencie);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
