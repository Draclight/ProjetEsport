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
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
{
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public CreateModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            ViewData["CompetitionID"] = new SelectList(_context.Competitions.Where(c => c.ID.Equals(id)).ToList(), "ID", "Nom");
            ViewData["TypeMatcheID"] = new SelectList(_context.TypesDeMatche, "ID", "Nom");
            ViewData["EquipeID"] = new SelectList(_context.CompetitionEquipe.Include(ce => ce.Equipe).Where(ce => ce.CompetitionID.Equals(id)).ToList(), "EquipeID", "Equipe.Nom");

            return Page();
        }

        [BindProperty]
        public MatcheViewModel Matche { get; set; }
        public Matche NouveauMatche { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NouveauMatche = new Matche
            {
                
            };

            _context.Matches.Add(NouveauMatche);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
