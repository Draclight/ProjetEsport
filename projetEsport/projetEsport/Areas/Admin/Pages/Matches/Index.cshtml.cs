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

namespace projetEsport.Areas.Admin.Pages.Matches
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MatcheViewModel> Matche { get; set; }

        public async Task OnGetAsync()
        {
            Matche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.EquipesDisputes).ThenInclude(e => e.EquipesDisputes)
                .Include(m => m.TypeMatche).Select(m => new MatcheViewModel
                {
                    ID = m.ID,
                    EquipesDuMatche = _context.EquipeMatche.Include(e => e.EquipesDisputes).Where(e => e.MatchesDisputesID.Equals(m.ID)).Select(e => new EquipeViewModel
                    {
                        EquipeID = e.ID,
                        Nom = e.EquipesDisputes.Nom,
                        Vainqueur = e.Vainqueur
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
                    Terminer = m.MatcheTeminer
                }).ToListAsync();

            foreach (MatcheViewModel m in Matche)
            {
                var vainqueur = _context.EquipeMatche.Include(em => em.EquipesDisputes).FirstOrDefault(e => e.MatchesDisputesID.Equals(m.ID) && e.Vainqueur);
                if (vainqueur != null)
                {
                    m.VainqueurId = vainqueur.ID;
                    m.VainqueurNom = vainqueur.EquipesDisputes.Nom;
                }
            }
        }
    }
}
