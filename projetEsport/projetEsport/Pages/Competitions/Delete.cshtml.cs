using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR,ORGANISATEUR")]
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<DetailsModel> _logger;

        public DeleteModel(projetEsport.Data.ApplicationDbContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Competition Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions
                .Include(c => c.Proprietaire)
                .Include(c => c.TypeCompetition).FirstOrDefaultAsync(m => m.ID == id);

            if (Competition == null)
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
