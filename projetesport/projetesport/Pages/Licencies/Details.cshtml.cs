using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetesport.Data;
using projetesport.Models;

namespace projetesport.Pages.Licencies
{
    public class DetailsModel : PageModel
    {
        private readonly projetesport.Data.ApplicationDbContext _context;

        public DetailsModel(projetesport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Licencie Licencie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = await _context.Licencie
                .Include(l => l.Equipe).FirstOrDefaultAsync(m => m.ID == id);

            if (Licencie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
