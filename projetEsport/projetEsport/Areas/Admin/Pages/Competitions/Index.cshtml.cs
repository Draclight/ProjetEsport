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
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CompetitionViewModel> Competitions { get; set; }

        public async Task OnGetAsync()
        {
            Competitions = await _context.Competitions
                .Include(c => c.Proprietaire)
                .Include(c => c.TypeCompetition)
                .Include(c => c.Jeu)
                .Include(c => c.MatchesDisputes).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
                    Jeu = new CompetitionJeuViewModel
                    {
                        ID = c.JeuID,
                        Nom = c.Jeu.Nom
                    },
                    Nom = c.Nom,
                    ProprietaireID = c.ProprietaireID,
                    Proprietaire = c.Proprietaire.Pseudo,
                    TypeCompetitionID = c.TypeCompetitionID,
                    TypeCompetition = c.TypeCompetition.Nom
                }).ToListAsync();
        }
    }
}
