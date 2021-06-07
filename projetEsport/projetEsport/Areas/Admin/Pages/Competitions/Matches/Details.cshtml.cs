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

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
{
    [Authorize(Roles = "Administrateur")]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public MatcheViewModel Matche { get; set; }
        public Matche dbMatche { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dbMatche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.EquipesDisputes).ThenInclude(ed => ed.EquipesDisputes)
                .Include(m => m.TypeMatche).FirstOrDefaultAsync(m => m.ID == id);

            Matche = new MatcheViewModel
            {
                ID = dbMatche.ID,
                CompetitionID = dbMatche.CompetitionID,
                CompetitionNom = dbMatche.Competition.Nom,
                CreeLe = dbMatche.CreeLe,
                Date = dbMatche.DateMatche,
                JeuID = dbMatche.Competition.JeuID,
                JeuNom = dbMatche.Competition.Jeu.Nom,
                TypeMatche = dbMatche.TypeMatche.Nom,
                NbVictoiresEquipeA = dbMatche.VictoireEquipeA,
                NbVictoiresEquipeB = dbMatche.VictoireEquipeB,
                ModifieeLe = dbMatche.ModifieeLe,
                EquipeANom = dbMatche.EquipesDisputes.ToArray()[0].EquipesDisputes.Nom,
                EquipeBNom = dbMatche.EquipesDisputes.ToArray()[1].EquipesDisputes.Nom,
                
            };

            if (Matche == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
