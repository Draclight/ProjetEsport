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
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DeleteModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CompetitionViewModel CompetitionVM { get; set; }
        public Competition Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompetitionVM = await _context.Competitions
                .Include(c => c.Proprietaire)
                .Include(c => c.TypeCompetition)
                .Include(c => c.Jeu)
                .Include(c => c.EquipesDeLaCompetition).ThenInclude(e => e.Equipe).ThenInclude(e => e.Membres)
                .Include(c => c.MatchesDisputes).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    EquipesDeLaCompetition = c.EquipesDeLaCompetition.Select(e => new EquipeViewModel
                    {
                        ID = e.EquipeID,
                        Nom = e.Equipe.Nom,
                        Membres = e.Equipe.Membres.Select(m => new LicencieViewModel
                        {
                            ID = m.ID,
                            Pseudo = m.Pseudo
                        }).ToList()
                    }).ToList(),
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
                }).FirstOrDefaultAsync(c => c.ID.Equals(id));

            if (CompetitionVM == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FindAsync(id);

            if (Competition != null)
            {
                _context.Competitions.Remove(Competition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
