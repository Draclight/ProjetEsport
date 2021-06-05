using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "Administrateur")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public CreateModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var equipes = await _context.Equipes.Where(e => e.IsApproved).ToListAsync();
            equipes.Insert(0, new Equipe() { Nom = string.Empty });
            ViewData["EquipeID"] = new SelectList(equipes, "ID", "Nom");
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
            try
            {
                if (Licencie.EquipeID.Equals(0))
                {
                    Licencie.EquipeID = null;
                }
                _context.Licencies.Add(Licencie);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}
