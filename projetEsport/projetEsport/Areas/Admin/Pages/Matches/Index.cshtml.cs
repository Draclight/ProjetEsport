using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Matche> Matche { get;set; }

        public async Task OnGetAsync()
        {
            Matche = await _context.Matches
                .Include(m => m.Competition)
                .Include(m => m.Jeu)
                .Include(m => m.TypeMatche).ToListAsync();
        }
    }
}
