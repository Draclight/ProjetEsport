using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    [Authorize(Roles = "ADMINISTRATEUR,ORGANISATEUR,LICENCIE")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            LicenciesViewModel = _context.Licencie.Where(l => l.IdUtilisateur != _userManager.GetUserId(User)).Select(l => new LicencieViewModel
            {
                licencie = l,
                InviteDansEquipe = false
            }).ToArray();

            EquipeViewModel = new EquipeViewModel
            {
                LicencieID = _context.Licencie.FirstOrDefault(l => l.IdUtilisateur.Equals(userId)).ID,
                Licencies = LicenciesViewModel
            };

            return Page();
        }

        [BindProperty]
        public EquipeViewModel EquipeViewModel { get; set; }
        [BindProperty]
        public IList<LicencieViewModel> LicenciesViewModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!string.IsNullOrEmpty(EquipeViewModel.Equipe.Nom) || LicenciesViewModel.All(l => !l.InviteDansEquipe))
            {
                var date = DateTime.UtcNow;
                EquipeViewModel.Equipe.IsApproved = false;
                EquipeViewModel.Equipe.CreeLe = EquipeViewModel.Equipe.ModifieeLe = date;

                _context.Equipe.Add(EquipeViewModel.Equipe);
                await _context.SaveChangesAsync();

                //Invitation dans l'équipe
                InvitationEquipe invitation = new InvitationEquipe
                {
                    EquipeID = EquipeViewModel.Equipe.ID,
                    LicencieID = EquipeViewModel.LicencieID,
                    IsAccepted = false
                };
                IList<InvitationEquipe> invitations = new List<InvitationEquipe>();
                invitations.Add(invitation);
                foreach (LicencieViewModel licencie in LicenciesViewModel)
                {
                    if (licencie.InviteDansEquipe)
                    {
                        invitations.Add(new InvitationEquipe
                        {
                            EquipeID = EquipeViewModel.Equipe.ID,
                            LicencieID = licencie.licencie.ID,
                            IsAccepted = false
                        });

                    }
                }

                _context.InvitationEquipe.AddRange(invitations);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
