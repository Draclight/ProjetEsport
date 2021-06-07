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

namespace projetEsport.Areas.Admin.Pages.TypesCompetitions
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
        public TypeCompetition TypeCompetition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeCompetition = await _context.TypesDeCompetition.FirstOrDefaultAsync(m => m.ID == id);

            if (TypeCompetition == null)
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

            TypeCompetition = await _context.TypesDeCompetition.FindAsync(id);

            if (TypeCompetition != null)
            {
                _context.TypesDeCompetition.Remove(TypeCompetition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
