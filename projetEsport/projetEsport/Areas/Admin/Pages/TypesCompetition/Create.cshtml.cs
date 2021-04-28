using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.TypesCompetition
{
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public CreateModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TypeCompetition TypeCompetition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var dateTime = DateTime.UtcNow;
            TypeCompetition.CreeLe = dateTime;
            TypeCompetition.ModifieeLe = dateTime;

            _context.TypeCompetition.Add(TypeCompetition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
