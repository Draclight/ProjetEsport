using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.Pages.Equipes;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<InvitationModel> _logger;


        public IndexModel(projetEsport.Data.ApplicationDbContext context, ILogger<InvitationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Licencie> Licencie { get;set; }
        public IList<LicencieViewModel> Licencies { get;set; }

        public async Task OnGetAsync()
        {
            Licencies = await _context.Licencies
                .Include(l => l.CompetitionsCrees)
                .Include(l => l.Equipe)
                .Include(l => l.InvitationEquipe)
                .Include(l => l.Utilisateur).Where(l => l.Utilisateur.EmailConfirmed).Select(l => new LicencieViewModel
                {
                    ID = l.ID,
                    CreeLe = l.CreeLe,
                    ModifieeLe = l.ModifieeLe,
                    Nom = l.Nom,
                    Prenom = l.Prenom,
                    Pseudo = l.Pseudo,
                    Equipe = l.Equipe.Nom,
                    Utilisateur = l.UtilisateurID,
                    Roles = _context.UserRoles.Where(ur => ur.UserId.Equals(l.UtilisateurID)).Select(ur => new RoleViewModel
                    {
                        RoleName = _context.Roles.FirstOrDefault(r => r.Id.Equals(ur.RoleId)).Name
                    }).ToList()
                }).ToListAsync();
        }
    }
}
