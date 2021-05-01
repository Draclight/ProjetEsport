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

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CompetitionViewModel Competition {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.TypeCompetition).Include(c => c.Proprietaire).FirstOrDefaultAsync(m => m.ID == id);

            Competition = new CompetitionViewModel()
            {
                Competition = competition,
                //NbJeux = competition.Jeux.Count,
                //NbEquipes = competition.Equipes.Count
            };  

            if (Competition.Competition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
