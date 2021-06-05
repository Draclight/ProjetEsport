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

namespace projetEsport.Areas.Admin.Pages.Jeux
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Jeu> Jeu { get; set; }

        public async Task OnGetAsync()
        {
            Jeu = await _context.Jeux.ToListAsync();
        }
    }
}
