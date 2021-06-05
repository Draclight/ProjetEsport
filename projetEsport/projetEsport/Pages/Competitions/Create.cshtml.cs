using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR,ORGANISATEUR")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<DetailsModel> _logger;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
        ViewData["ProprietaireID"] = new SelectList(_context.Licencies, "ID", "ID");
        ViewData["TypeCompetitionID"] = new SelectList(_context.TypesDeCompetition, "ID", "Nom");
            return Page();
        }

        [BindProperty]
        public Competition Competition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Competitions.Add(Competition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
