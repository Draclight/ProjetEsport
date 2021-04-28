using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetEsport.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {

        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IdentityUser> Users { get; set; }

        public void OnGetAsync()
        {
            Users = _context.Users.ToList();
        }
    }
}