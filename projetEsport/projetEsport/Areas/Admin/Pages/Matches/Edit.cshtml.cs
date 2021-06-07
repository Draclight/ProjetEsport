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

namespace projetEsport.Areas.Admin.Pages.Matches
{
    [Authorize(Roles = "Administrateur")]
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public EditModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Matche Matche { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Matche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.TypeMatche).FirstOrDefaultAsync(m => m.ID == id);

            if (Matche == null)
            {
                return NotFound();
            }
           ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "Nom");
           ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");
           ViewData["TypeMatcheID"] = new SelectList(_context.TypesDeMatche, "ID", "Nom");
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

            _context.Attach(Matche).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatcheExists(Matche.ID))
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

        private bool MatcheExists(int id)
        {
            return _context.Matches.Any(e => e.ID == id);
        }
    }
}
