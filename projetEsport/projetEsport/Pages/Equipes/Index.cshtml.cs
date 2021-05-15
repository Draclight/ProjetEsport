using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<EquipeViewModel> Equipes { get; set; }
        public bool PeutCreer { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var licencie = await _context.Licencie.Include(l => l.Equipe).FirstOrDefaultAsync(l => l.IdUtilisateur.Equals(userId));
            PeutCreer = (licencie is not null && licencie.Equipe is null);
            Equipes = await _context.Equipe.Include(e => e.Membres).Where(e => e.IsApproved).Select(e => new EquipeViewModel
            {
                ID = e.ID,
                Equipe = e,
                IsMembre = e.Membres.Any(l => l.IdUtilisateur.Equals(userId))
        }).ToListAsync();
        }
    }
}
