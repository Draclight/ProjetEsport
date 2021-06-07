using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "Administrateur")]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Licencie Licencie { get; set; }
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
                EquipeID = Licencie.EquipeID,
                CreeLe = Licencie.CreeLe,
                ModifieeLe = Licencie.ModifieeLe,
                Nom = Licencie.Nom,
                Prenom = Licencie.Prenom,
                Pseudo = Licencie.Pseudo,
                Equipe = Licencie.Equipe?.Nom,
                Utilisateur = Licencie.UtilisateurID,
                Createur = Licencie.CreateurEquipe,
                Roles = await _context.UserRoles.Where(ur => ur.UserId.Equals(Licencie.UtilisateurID)).Select(ur => new RoleViewModel
                {

                    RoleId = ur.UserId,
                    RoleName = _context.Roles.FirstOrDefault(r => r.Id.Equals(ur.RoleId)).Name
                }).ToListAsync(),
                Competitions = await _context.Competitions.Include(c => c.EquipesDeLaCompetition).Include(c => c.Jeu).Where(c => c.ProprietaireID.Equals(Licencie.ID))
                .Select(c => new CompetitionViewModel { 
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count(),
                    Jeu = new CompetitionJeuViewModel
                    {
                        ID = c.JeuID,
                        Nom = c.Jeu.Nom
                    },
                    Nom = c.Nom
                }).ToListAsync(),
                Invitations = await _context.InvitationsEquipes.Include(ie => ie.Equipe).Where(ie => ie.LicencieID.Equals(Licencie.ID)).Select(ie => new InvitationViewModel
                {
                    ID = ie.ID,
                    LicencieID = ie.LicencieID,
                    Accepter = ie.IsAccepted,
                    NomEquipe = ie.Equipe.Nom,
                    DateAccepter = ie.DateAccepter,
                    DateEnvoi = ie.DateEnvoi,
                    EquipeId = ie.EquipeID
                }).ToListAsync()
            };

            return Page();
        }
    }
}
