using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetesport.Data;
using projetesport.Models;

namespace projetesport.Pages.Licencies
{
    [Authorize(Roles = "Administrateur, Licencie")]
    public class CreateModel : PageModel
    {
        private readonly projetesport.Data.ApplicationDbContext _context;

        public CreateModel(projetesport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EquipeID"] = new SelectList(_context.Equipe, "ID", "Nom");
            return Page();
        }

        [BindProperty]
        public Licencie Licencie { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Licencie.Add(Licencie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
