using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Competitions.Matches
{
    [Authorize(Roles = "Administrateur,Organisateur,Licencie")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MatcheViewModel> Matche { get; set; }
        public int CompetitionID { get; set; }
        public bool IsProprietaire { get; set; }

        public async Task OnGetAsync(int? id)
        {
            CompetitionID = (int)id;

            Matche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.EquipesDisputes).ThenInclude(e => e.EquipesDisputes)
                .Include(m => m.TypeMatche).Where(m => m.CompetitionID.Equals(id)).Select(m => new MatcheViewModel
                {
                    ID = m.ID,
                    EquipesDuMatche = _context.EquipeMatche.Include(e => e.EquipesDisputes).Where(e => e.MatchesDisputesID.Equals(m.ID)).Select(e => new EquipeViewModel
                    {
                        EquipeID = e.ID,
                        Nom = e.EquipesDisputes.Nom
                    }).ToList(),
                    CompetitionID = m.CompetitionID,
                    CompetitionNom = m.Competition.Nom,
                    CreeLe = m.CreeLe,
                    JeuID = m.Competition.JeuID,
                    JeuNom = m.Competition.Nom,
                    ModifieeLe = m.ModifieeLe,
                    TypeMatcheID = m.TypeMatcheID,
                    TypeMatche = m.TypeMatche.Nom,
                    NbVictoiresEquipeA = m.VictoireEquipeA,
                    NbVictoiresEquipeB = m.VictoireEquipeB,
                    Date = m.DateMatche,
                    IsProprietaire = m.Competition.Proprietaire.UtilisateurID.Equals(_userManager.GetUserId(User))
                }).ToListAsync();

            IsProprietaire = _context.Competitions.Include(c => c.Proprietaire).Any( c => c.ID.Equals(CompetitionID) && c.Proprietaire.UtilisateurID.Equals(_userManager.GetUserId(User)));
        }
    }
}
