using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetesport.Data;
using projetesport.Models;

namespace projetesport.Pages.Licencies
{
    public class IndexModel : PageModel
    {
        private readonly projetesport.Data.ApplicationDbContext _context;

        public IndexModel(projetesport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Licencie> Licencie { get;set; }

        public async Task OnGetAsync()
        {
            Licencie = await _context.Licencie
                .Include(l => l.Equipe).ToListAsync();
        }
    }
}
