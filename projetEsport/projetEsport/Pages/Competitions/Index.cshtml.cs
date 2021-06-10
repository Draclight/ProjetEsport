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
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Competitions
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        protected IAuthorizationService AuthorizationService { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, IAuthorizationService authorizationService, ILogger<IndexModel> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            AuthorizationService = authorizationService;
            _logger = logger;
            _userManager = userManager;
        }

        public IList<CompetitionViewModel> Competition { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Competition = await _context.Competitions
                .Include(c => c.TypeCompetition)
                .Include(c => c.Proprietaire)
                .Include(c => c.Jeu)
                .Include(c => c.EquipesDeLaCompetition).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
                    JeuID = c.JeuID,
                    Jeu = new CompetitionJeuViewModel
                    {
                        ID = c.ID,
                        Nom = c.Jeu.Nom
                    },
                    Proprietaire = c.Proprietaire.Pseudo,
                    Nom = c.Nom,
                    TypeCompetition = c.TypeCompetition.Nom,
                    IsPropriétaire = c.Proprietaire.UtilisateurID.Equals(_userManager.GetUserId(User)),
                    Vainqueur = _context.Matches.Include(m => m.EquipesDisputes).Any(m => m.CompetitionID.Equals(c.ID) && m.TypeMatcheID.Equals((int)TypeMatchesViewModel.Finale) && m.MatcheTeminer) ?
                                                    _context.Matches.Include(m => m.EquipesDisputes).FirstOrDefault(m => m.CompetitionID.Equals(c.ID)
                                                                                    && m.TypeMatcheID.Equals((int)TypeMatchesViewModel.Finale)
                                                                                    && m.MatcheTeminer).EquipesDisputes.FirstOrDefault(e => e.Vainqueur).EquipesDisputes.Nom : ""
                }).ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                if ((await AuthorizationService.AuthorizeAsync(User, new Competition(), ESportOperations.Read)).Failure.FailCalled)
                {
                    return Forbid();
                }
            }

            return Page();
        }
    }
}
