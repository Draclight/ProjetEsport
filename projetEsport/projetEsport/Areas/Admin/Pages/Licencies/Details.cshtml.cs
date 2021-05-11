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

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public LicencieViewModel Licencie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Licencie = new LicencieViewModel();
            Licencie.licencie = await _context.Licencie
                .Include(l => l.Equipe).FirstOrDefaultAsync(m => m.ID == id);

            try
            {
                var roles = from r in _context.Roles
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            where ur.UserId == Licencie.licencie.IdUtilisateur
                            select r;

                Licencie.Roles = roles.Select(r => new RoleViewModel()
                {
                    RoleName = r.NormalizedName
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (Licencie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
