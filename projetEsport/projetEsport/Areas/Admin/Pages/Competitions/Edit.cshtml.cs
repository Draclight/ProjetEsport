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
        public IList<CreateCompetitionViewModel> JeuxDisponible { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //récupération de la compétition
            Competition = await _context.Competition
                .Include(c => c.TypeCompetition).Include(c => c.Proprietaire).Include(c => c.Equipes).FirstOrDefaultAsync(m => m.ID == id);

            //récupération des jeux
            var jeux = await _context.Jeu.ToListAsync();

            //Affichage des jeux si déjà présent dans la compétition
            JeuxDisponible = jeux.Select(jeu => new CreateCompetitionViewModel()
            {
                JeuID = jeu.ID,
                //IsInCompetition = Competition.Jeux.Contains(jeu),
                Nom = jeu.Nom
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

            _context.Attach(Competition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
