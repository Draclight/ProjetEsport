using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.TypesMatches
{
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DeleteModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TypeMatche TypeMatche { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeMatche = await _context.TypesDeMatche.FirstOrDefaultAsync(m => m.ID == id);

            if (TypeMatche == null)
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

            TypeMatche = await _context.TypesDeMatche.FindAsync(id);

            if (TypeMatche != null)
            {
                _context.TypesDeMatche.Remove(TypeMatche);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
