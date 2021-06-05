using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public DashboardViewModel Dashboard { get; set; }

        public void OnGetAsync()
        {
            Dashboard = new DashboardViewModel
            {
                NbLicenciesAnnee = _context.Licencies.Count(),
                NbLicenciesMois = _context.Licencies.Where(l => l.CreeLe.Date.Year.Equals(DateTime.Today.Date.Year)).Count(),
                NbLicenciesJour = _context.Licencies.Where(l => l.CreeLe.Date.Equals(DateTime.Today.Date)).Count()
            };
        }
    }
}
