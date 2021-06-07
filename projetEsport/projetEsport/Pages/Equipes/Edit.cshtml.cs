using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(projetEsport.Data.ApplicationDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
        }

        [BindProperty]
        public EquipeViewModel EquipeVM { get; set; }
        public Equipe Equipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.Equipes
                .Include(e => e.Membres)
                .Include(e => e.Jeu).FirstOrDefaultAsync(m => m.ID == id);

            if (Equipe == null)
            {
                return NotFound();
            }

            EquipeVM = new EquipeViewModel
            {
                ID = Equipe.ID,
                Nom = Equipe.Nom,
                CreeLe = Equipe.CreeLe,
                ModifieeLe = Equipe.ModifieeLe,
                IsApproved = Equipe.IsApproved,
                JeuID = Equipe.JeuID,
                Invitations = await _context.Licencies.Where(l => !l.UtilisateurID.Equals(1)).Select(l => new InvitationViewModel
                {
                    LicencieID = l.ID,
                    EquipeId = Equipe.ID,
                    PseudoLicencie = l.Pseudo,
                    Accepter = _context.InvitationsEquipes.Any(ie => ie.EquipeID.Equals(Equipe.ID) && ie.LicencieID.Equals(l.ID)) ?
                                                _context.InvitationsEquipes.First(ie => ie.EquipeID.Equals(Equipe.ID) && ie.LicencieID.Equals(l.ID)).IsAccepted : false,
                    Envoyer = _context.InvitationsEquipes.Any(ie => ie.EquipeID.Equals(Equipe.ID) && ie.LicencieID.Equals(l.ID))
                }).ToListAsync()
            };

            ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Equipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID.Equals(EquipeVM.ID));
            Equipe.JeuID = EquipeVM.JeuID;
            Equipe.ModifieeLe = DateTime.Now;
            Equipe.Nom = EquipeVM.Nom;

            try
            {
                _context.Attach(Equipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipeExists(Equipe.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostInviter(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationsEquipes.FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            if (invitationEquipe is not null)
            {
                return RedirectToPage(new
                {
                    id = (int?)invitation.EquipeId,
                });
            }

            invitationEquipe = new InvitationEquipe
            {
                IsAccepted = false,
                DateEnvoi = DateTime.UtcNow,
                EquipeID = invitation.EquipeId,
                LicencieID = invitation.LicencieID
            };

            try
            {
                _context.Attach(invitationEquipe).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)invitation.EquipeId,
            });
        }


        private bool EquipeExists(int id)
        {
            return _context.Equipes.Any(e => e.ID == id);
        }
    }
}
