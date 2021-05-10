using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public EditModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Competition Competition { get; set; }
        [BindProperty]
        public IList<CompetitionViewModel> JeuxDisponible { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //récupération de la compétition
            Competition = await _context.Competition
                .Include(c => c.TypeCompetition).Include(c => c.Proprietaire).Include(c => c.Jeux).FirstOrDefaultAsync(m => m.ID == id);

            //récupération des jeux
            var jeux = await _context.Jeu.Include(j => j.Competitions).ToListAsync();

            //Affichage des jeux si déjà présent dans la compétition
            JeuxDisponible = jeux.Select(jeu => new CompetitionViewModel()
            {
                JeuID = jeu.ID,
                IsInCompetition = Competition.Jeux.FirstOrDefault(j => j.JeuID == jeu.ID) == null ? false : true,
                Nom = jeu.Nom,
                CompetitiionId = Competition.ID
            }).ToList();

            if (Competition == null)
            {
                return NotFound();
            }

            ViewData["TypeCompetition"] = new SelectList(_context.TypeCompetition, "ID", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                //Modification de la compétition
                _context.Attach(Competition).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                //Jeux de la compétition
                var jeux = await _context.CompetitionJeu.Include(cj => cj.Competition).Include(cj => cj.Jeu).ToListAsync();
                foreach (CompetitionViewModel jeu in JeuxDisponible.Where(jd => jd.IsInCompetition))
                {
                    CompetitionJeu competitionJeu = await _context.CompetitionJeu.FirstOrDefaultAsync(cj => cj.JeuID.Equals(jeu.JeuID));
                    if (competitionJeu is null)
                    {
                        competitionJeu = new CompetitionJeu()
                        {
                            JeuID = jeu.JeuID,
                            CompetitionID = Competition.ID
                        };
                        _context.Attach(Competition).State = EntityState.Added;
                    }
                    else
                    {
                        _context.Attach(Competition).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(Competition.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competition.Any(e => e.ID == id);
        }
    }
}
