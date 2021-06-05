﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Equipes
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
        ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");
            return Page();
        }

        [BindProperty]
        public Equipe Equipe { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Equipes.Add(Equipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
