﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.TypesCompetition
{
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}