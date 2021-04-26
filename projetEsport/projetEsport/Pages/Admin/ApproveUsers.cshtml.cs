using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Admin
{
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
            Page();
        }

        public async Task OnPostApproveUserAsync(string id)
        {
            //User
            if (await _context.Users.AnyAsync(u => u.Id == id))
            {
                IdentityUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                user.EmailConfirmed = true;
                _context.Attach(user).State = EntityState.Modified;


                //User role
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Name.Equals(Constants.ConnexionRole)).Id;
                newRole.UserId = id;
                _context.UserRoles.Add(newRole);
                await _context.SaveChangesAsync();
            }

            //Licencie
            if (!await _context.Licencie.AnyAsync(l => l.IdUtilisateur == id))
            {
                //créer un licencié avec le guid et les données de base nécéssaires
                Licencie NouveauLicencie = new Licencie
                {
                    IdUtilisateur = id,
                    Nom = string.Empty,
                    Penom = string.Empty,
                    Pseudo = string.Empty,
                    PremierConnexion = true,
                    CreeLe = DateTime.UtcNow,
                    ModifieeLe = DateTime.UtcNow
                };
                _context.Licencie.Add(NouveauLicencie);
                await _context.SaveChangesAsync();
            }

            Page();
        }

        public async Task OnPostRejectUserAsync(string id)
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

            Page();
        }
    }
}
