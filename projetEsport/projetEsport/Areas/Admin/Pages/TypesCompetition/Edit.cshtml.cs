using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace projetEsport.Areas.Admin.Pages.TypesCompetition
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
        public EditTypeCompetitionViewModel viewModel { get; set; }
        [BindProperty]
        public TypeCompetition TypeCompetition { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeCompetition = await _context.TypeCompetition.FirstOrDefaultAsync(m => m.ID == id);

            if (TypeCompetition == null)
            {
                return NotFound();
            }

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

            TypeCompetition.ModifieeLe = DateTime.UtcNow;
            _context.Attach(TypeCompetition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeCompetitionExists(TypeCompetition.ID))
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

        private bool TypeCompetitionExists(int id)
        {
            return _context.TypeCompetition.Any(e => e.ID == id);
        }
    }
}
