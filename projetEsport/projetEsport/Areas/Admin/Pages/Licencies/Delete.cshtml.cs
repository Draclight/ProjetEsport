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
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(projetEsport.Data.ApplicationDbContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public LicencieViewModel Licencie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = new LicencieViewModel();
            Licencie.licencie = await _context.Licencie
                .Include(l => l.Equipe).FirstOrDefaultAsync(m => m.ID == id);

            try
            {
                var roles = from r in _context.Roles
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            where ur.UserId == Licencie.licencie.IdUtilisateur
                            select r;

                Licencie.Roles = roles.Select(r => new RoleViewModel()
                {
                    RoleName = r.NormalizedName
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

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

            Licencie.licencie = await _context.Licencie.FindAsync(id);

            if (Licencie != null)
            {
                //Suppréssion des roles
                var roles = await _context.UserRoles.Where(ur => ur.UserId.Equals(Licencie.licencie.IdUtilisateur)).ToListAsync();
                _context.UserRoles.RemoveRange(roles);
                await _context.SaveChangesAsync();

                //Suppréssion de l'utilisateur
                var user = await _context.Users.FirstAsync(u => u.Id.Equals(Licencie.licencie.IdUtilisateur));
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                //Suppré
                _context.Licencie.Remove(Licencie.licencie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
