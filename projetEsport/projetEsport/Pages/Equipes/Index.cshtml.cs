using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Equipes
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Equipe> Equipe { get;set; }

        public async Task OnGetAsync()
        {
            Equipe = await _context.Equipes
                .Include(e => e.Membres)
                .Include(e => e.Jeu).Where(e => e.IsApproved).ToListAsync();
        }
    }
}
