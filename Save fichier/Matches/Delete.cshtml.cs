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

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EquipesMatche = await _context.EquipeMatche.Where(em => em.MatchesDisputesID.Equals(id)).ToListAsync();

            if (EquipesMatche != null)
            {
                _context.EquipeMatche.RemoveRange(EquipesMatche);
                await _context.SaveChangesAsync();
            }

            Matche = await _context.Matches.FindAsync(id);

            if (Matche != null)
            {
                _context.Matches.Remove(Matche);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new
            {
                id = (int?)Matche.CompetitionID
            });
        }
    }
}
