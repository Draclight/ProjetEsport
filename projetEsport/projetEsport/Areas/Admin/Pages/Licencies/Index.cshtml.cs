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
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LicencieViewModel> Licencie { get;set; }

        public async Task OnGetAsync()
        {
            Licencie = new List<LicencieViewModel>();
            var listeLicencies = await _context.Licencie.Include(l => l.Equipe).ToListAsync();
            foreach (var licencie in listeLicencies)
            {
                var roles = from r in _context.Roles
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            where ur.UserId == licencie.IdUtilisateur
                            select r;

                var userRoles = roles.Select(r => new RoleViewModel()
                {
                    RoleName = r.NormalizedName
                }).ToList();

                Licencie.Add(new LicencieViewModel() { licencie = licencie, Roles = userRoles });
            }
        }
    }
}
