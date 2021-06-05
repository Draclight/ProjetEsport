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
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "Administrateur")]
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(projetEsport.Data.ApplicationDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Licencie Licencie { get; set; }
        [BindProperty]
        public LicencieViewModel LicencieVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = await _context.Licencies
                .Include(l => l.CompetitionsCrees)
                .Include(l => l.Equipe)
                .Include(l => l.InvitationEquipe).ThenInclude(ie => ie.Equipe)
                .Include(l => l.Utilisateur).FirstOrDefaultAsync(m => m.ID == id);

            if (Licencie == null)
            {
                return NotFound();
            }

            LicencieVM = new LicencieViewModel()
            {
                ID = Licencie.ID,
                CreeLe = Licencie.CreeLe,
                ModifieeLe = Licencie.ModifieeLe,
                Nom = Licencie.Nom,
                Prenom = Licencie.Prenom,
                Pseudo = Licencie.Pseudo,
                EquipeID = Licencie.EquipeID == null ? 0 : Licencie.EquipeID,
                Equipe = Licencie.Equipe?.Nom,
                Createur = Licencie.CreateurEquipe,
                UtilisateurID = Licencie.UtilisateurID,
                Roles = await _context.Roles.Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    LicencieId = Licencie.ID,
                    LicencieUserId = Licencie.UtilisateurID,
                    IsAcquired = _context.UserRoles.Any(ur => ur.RoleId.Equals(r.Id) && ur.UserId.Equals(Licencie.UtilisateurID))
                }).ToListAsync(),
                Competitions = await _context.Competitions.Include(c => c.EquipesDeLaCompetition).Include(c => c.JeuxDeLaCompetition).Where(c => c.ProprietaireID.Equals(Licencie.ID))
                .Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count(),
                    NbJeux = c.JeuxDeLaCompetition.Count(),
                    Nom = c.Nom
                }).ToListAsync(),
                Invitations = await _context.InvitationsEquipes.Include(ie => ie.Equipe).Where(ie => ie.LicencieID.Equals(Licencie.ID)).Select(ie => new InvitationViewModel
                {
                    ID = ie.ID,
                    LicencieID = ie.LicencieID,
                    Accepter = ie.IsAccepted,
                    NomEquipe = ie.Equipe.Nom,
                    DateAccepter = ie.DateAccepter,
                    DateEnvoi = ie.DateEnvoi
                }).ToListAsync()
            };

            var equipes = await _context.Equipes.Where(e => e.IsApproved).ToListAsync();
            equipes.Insert(0, new Equipe() { Nom = string.Empty });
            ViewData["EquipeID"] = new SelectList(equipes, "ID", "Nom");
            ViewData["UtilisateurID"] = new SelectList(_context.Users, "Id", "Id");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostEditLicencieAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage(new
                {
                    id = (int?)LicencieVM.ID,
                });
            }

            try
            {
                //Licencie
                Licencie = await _context.Licencies.FirstOrDefaultAsync(l => l.ID.Equals(LicencieVM.ID));
                Licencie.CreeLe = LicencieVM.CreeLe;
                if (LicencieVM.EquipeID.Equals(0))
                {
                    Licencie.EquipeID = null;
                }
                else
                {
                    Licencie.EquipeID = LicencieVM.EquipeID;
                }
                if (LicencieVM.EquipeID.Equals(0))
                {
                    Licencie.CreateurEquipe = false;
                }
                else
                {
                    Licencie.CreateurEquipe = LicencieVM.Createur;
                }
                Licencie.ModifieeLe = DateTime.UtcNow;
                Licencie.Nom = LicencieVM.Nom;
                Licencie.Prenom = LicencieVM.Prenom;
                Licencie.Pseudo = LicencieVM.Pseudo;
                Licencie.UtilisateurID = LicencieVM.UtilisateurID;
                _context.Attach(Licencie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicencieExists(Licencie.ID))
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

        public async Task<IActionResult> OnPostAddRoleAsync(RoleViewModel role)
        {
            try
            {
                //User role
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Id.Equals(role.RoleId)).Id;
                newRole.UserId = role.LicencieUserId;
                _context.UserRoles.Add(newRole);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LicencieExists(LicencieVM.ID))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage(new
            {
                id = (int?)role.LicencieId,
            });
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(RoleViewModel role)
        {
            try
            {
                //User role
                IdentityUserRole<string> formerRole = new IdentityUserRole<string>();
                formerRole.RoleId = _context.Roles.First(r => r.Id.Equals(role.RoleId)).Id;
                formerRole.UserId = role.LicencieUserId;
                _context.UserRoles.Remove(formerRole);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LicencieExists(LicencieVM.ID))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage(new
            {
                id = (int?)role.LicencieId,
            });
        }

        public async Task<IActionResult> OnPostAccepterInvitation(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationsEquipes.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            invitationEquipe.IsAccepted = true;
            try
            {
                _context.Attach(invitationEquipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)invitation.LicencieID,
            });
        }

        public async Task<IActionResult> OnPostRejeterInvitation(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationsEquipes.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            try
            {
                _context.InvitationsEquipes.Remove(invitationEquipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return RedirectToPage(new
            {
                id = (int?)invitation.LicencieID,
            });
        }
        private bool LicencieExists(int id)
        {
            return _context.Licencies.Any(e => e.ID == id);
        }
    }
}
