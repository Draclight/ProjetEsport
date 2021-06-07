using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Equipes
{
    [Authorize(Roles="Administrateur")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var date = DateTime.Now;
            Equipe = new EquipeViewModel
            {
                CreeLe = date,
                ModifieeLe = date,
                LicenciesAInviter = _context.Licencies.Include(l => l.Utilisateur).Where(l => l.Utilisateur.EmailConfirmed && !l.EquipeID.Equals(null)).Select(l => new LicencieViewModel
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                //Equipe
                NouvelleEquipe = new Equipe
                {
                    CreeLe = Equipe.CreeLe,
                    IsApproved = Equipe.IsApproved,
                    JeuID = Equipe.JeuID,
                    ModifieeLe = Equipe.ModifieeLe,
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
