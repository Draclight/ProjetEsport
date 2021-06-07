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
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var date = DateTime.Now;
            Equipe = new EquipeViewModel
            {
                CreeLe = date,
                ModifieeLe = date,
                LicenciesAInviter = _context.Licencies.Include(l => l.Utilisateur)
                .Where(l => l.Utilisateur.EmailConfirmed && !l.EquipeID.Equals(null) && !l.UtilisateurID.Equals(_userManager.GetUserId(User))).Select(l => new LicencieViewModel
                {
                    ID = l.ID,
                    Pseudo = l.Pseudo,
                    InviteDansEquipe = false
                }).ToList()
            };

            ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");

            return Page();
        }

        [BindProperty]
        public EquipeViewModel Equipe { get; set; }
        public Equipe NouvelleEquipe { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var date = DateTime.Now;

                //Equipe
                NouvelleEquipe = new Equipe
                {
                    CreeLe = date,
                    IsApproved = false,
                    JeuID = Equipe.JeuID,
                    ModifieeLe = date,
                    Nom = Equipe.Nom
                };

                _context.Equipes.Add(NouvelleEquipe);
                await _context.SaveChangesAsync();

                //Invitations
                foreach (LicencieViewModel licencie in Equipe.LicenciesAInviter)
                {
                    if (licencie.InviteDansEquipe)
                    {
                        InvitationEquipe newInvitation = new InvitationEquipe
                        {
                            EquipeID = NouvelleEquipe.ID,
                            LicencieID = licencie.ID,
                            DateEnvoi = DateTime.Now
                        };

                        _context.InvitationsEquipes.Add(newInvitation);
                        await _context.SaveChangesAsync();
                    }
                }

                var createur = await _context.Licencies.Include(l => l.Equipe).FirstOrDefaultAsync(l => l.UtilisateurID.Equals(_userManager.GetUserId(User)));
                InvitationEquipe invitationLicencie = new InvitationEquipe
                {
                    EquipeID = NouvelleEquipe.ID,
                    LicencieID = createur.ID,
                    IsAccepted = true,
                    DateEnvoi = DateTime.Now
                };
                _context.InvitationsEquipes.Add(invitationLicencie);
                await _context.SaveChangesAsync();

                createur.EquipeID = NouvelleEquipe.ID; 
                createur.CreateurEquipe = true;
                _context.Attach(createur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return RedirectToPage();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
