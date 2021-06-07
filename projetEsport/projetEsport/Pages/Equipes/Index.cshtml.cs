using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, ILogger<IndexModel> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IList<EquipeViewModel> Equipes { get; set; }

        public async Task OnGetAsync()
        {
            Equipes = await _context.Equipes
                .Include(e => e.Membres)
                .Include(e => e.Jeu).Where(e => e.IsApproved).Select(e => new EquipeViewModel
                {
                    ID = e.ID,
                    CreeLe = e.CreeLe,
                    ModifieeLe = e.ModifieeLe,
                    JeuNom = e.Jeu.Nom,
                    Nom = e.Nom,
                    IsProprietaire = _context.Licencies.FirstOrDefault(l => l.EquipeID.Equals(e.ID) && l.CreateurEquipe).UtilisateurID.Equals(_userManager.GetUserId(User))
                }).ToListAsync();
        }
    }
}
