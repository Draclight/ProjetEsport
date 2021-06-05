﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.TypesMatches
{
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public EditModel(projetEsport.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TypeMatche).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeMatcheExists(TypeMatche.ID))
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

        private bool TypeMatcheExists(int id)
        {
            return _context.TypesDeMatche.Any(e => e.ID == id);
        }
    }
}
