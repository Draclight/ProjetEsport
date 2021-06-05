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
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CompetitionViewModel Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions
                .Include(c => c.Proprietaire)
                .Include(c => c.TypeCompetition)
                .Include(c => c.JeuxDeLaCompetition).ThenInclude(j => j.JeuxDeLaCompetition)
                .Include(c => c.EquipesDeLaCompetition).ThenInclude(e => e.Equipe).ThenInclude(e => e.Membres)
                .Include(c => c.MatchesDisputes).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    EquipesDeLaCompetition = c.EquipesDeLaCompetition.Select(e => new EquipeViewModel
                    {
                        ID = e.ID,
                        Nom = e.Equipe.Nom,
                        CompetitionID = c.ID,
                        EquipeID = e.EquipeID,
                        Membres = e.Equipe.Membres.Select(m => new LicencieViewModel
                        {
                            ID= m.ID,
                            Pseudo = m.Pseudo
                        }).ToList()
                    }).ToList(),
                    JeuxDeLaCompetition = c.JeuxDeLaCompetition.Select(j => new CompetitionJeuViewModel
                    {
                        ID = j.ID,
                        CompetitionID = c.ID,
                        JeuID = j.JeuxDeLaCompetitionID,
                        Nom = j.JeuxDeLaCompetition.Nom
                    }).ToList(),
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
                    NbJeux = c.JeuxDeLaCompetition.Count,
                    Nom = c.Nom,
                    ProprietaireID = c.ProprietaireID,
                    Proprietaire = c.Proprietaire.Pseudo,
                    TypeCompetitionID = c.TypeCompetitionID,
                    TypeCompetition = c.TypeCompetition.Nom
                }).FirstOrDefaultAsync(c => c.ID.Equals(id));

            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
