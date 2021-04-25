using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Identity.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        private readonly ILogger<RegisterConfirmationModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterConfirmationModel(ILogger<RegisterConfirmationModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Licencie NouveauLicencie { get; set; }

        public async Task<IActionResult> OnGetAsync(IdentityUser identityUser)
        {
            try
            {
                if (LicencieExists(identityUser.Id))
                {
                    new Exception("Il y a un autre licencie avec le meme userId");
                }

                //créer un licencié avec le guid et les données de base nécéssaires
                NouveauLicencie = new Licencie
                {
                    IdUtilisateur = identityUser.Id,
                    Nom = "",
                    Penom = "",
                    Pseudo = "",
                    PremierConnexion = true,
                    CreeLe = DateTime.UtcNow,
                    ModifieeLe = DateTime.UtcNow
                };
                _context.Attach(NouveauLicencie).State = EntityState.Added;

                //Role
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Name.Equals("CONNEXION")).Id;
                newRole.UserId = identityUser.Id;
                _context.Attach(newRole).State = EntityState.Added;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return RedirectToPage("/Index");
        }

        private bool LicencieExists(string id)
        {
            return _context.Licencie.Any(e => e.IdUtilisateur == id);
        }
    }
}
