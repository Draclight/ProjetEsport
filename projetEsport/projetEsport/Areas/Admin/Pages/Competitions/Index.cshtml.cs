using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.ViewModels;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Competition> Competition { get;set; }
        public IList<CompetitionViewModel> ListeCompetition { get; set; }

        public async Task OnGetAsync()
        {
            Competition = await _context.Competition
                .Include(c => c.TypeCompetition).Include(c => c.Proprietaire).Include(c => c.Equipes).ToListAsync();

            ListeCompetition = Competition.Select(c => new CompetitionViewModel()
            {
                Competition = c,
                //NbJeux = c.Jeux.Count,
                NbEquipes = c.Equipes.Count
            }).ToList();
        }
    }
}
