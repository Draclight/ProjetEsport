using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.TypesCompetitions
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TypeCompetition> TypeCompetition { get; set; }

        public async Task OnGetAsync()
        {
            TypeCompetition = await _context.TypesDeCompetition.ToListAsync();
        }
    }
}
