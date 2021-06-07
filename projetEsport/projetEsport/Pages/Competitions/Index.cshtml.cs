using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Pages.Competitions
{
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        protected IAuthorizationService AuthorizationService { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(projetEsport.Data.ApplicationDbContext context, IAuthorizationService authorizationService, ILogger<IndexModel> logger)
        {
            _context = context;
            AuthorizationService = authorizationService;
            _logger = logger;
        }

        public IList<Competition> Competition { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Competition = await _context.Competitions.Include(c => c.TypeCompetition).Include(c => c.Proprietaire).Include(c => c.Jeu).Include(c => c.EquipesDeLaCompetition).ToListAsync();

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
