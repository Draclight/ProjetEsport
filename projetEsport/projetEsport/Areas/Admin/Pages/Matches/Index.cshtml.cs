using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MatcheViewModel> Matche { get; set; }

        public async Task OnGetAsync(int? id)
        {
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
                    NbVictoiresEquipeA = m.VictoireAEquipe1,
                    NbVictoiresEquipeB = m.VictoireAEquipe2
                }).ToListAsync();
        }
    }
}
