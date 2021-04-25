﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Licencie Licencie { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User);
            bool isLicencieExist = LicencieExists(userId);
            bool isLogin = User.Identity.IsAuthenticated;

            if (isLogin)
            {
                if (isLicencieExist)
                {
                    Licencie = await _context.Licencie.Include(l => l.Equipe).Where(l => l.IdUtilisateur.Equals(userId)).FirstOrDefaultAsync();
                }
                else
                {
                    Licencie = new Licencie();
                    Licencie.PremierConnexion = true;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostPremiereConnexion()
        {
            try
            {
                //Info user
                Licencie.PremierConnexion = false;
                Licencie.ModifieeLe = DateTime.UtcNow;
                _context.Attach(Licencie).State = EntityState.Added;
                await _context.SaveChangesAsync();

                //Suppréssion role
                var rolesUser = _context.UserRoles.Where(l => l.UserId == Licencie.IdUtilisateur);
                _context.UserRoles.RemoveRange(rolesUser);
                await _context.SaveChangesAsync();

                //Nouveau role user
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Name.Equals("LICENCIE")).Id;
                newRole.UserId = Licencie.IdUtilisateur;
                _context.Attach(newRole).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);

                //check si user à un role
                var rolesUser = _context.UserRoles.Where(l => l.UserId == Licencie.IdUtilisateur);
                if (rolesUser.Count() == 0)
                {
                    IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                    newRole.RoleId = _context.Roles.First(r => r.Name.Equals("LICENCIE")).Id;
                    newRole.UserId = Licencie.IdUtilisateur;
                    _context.Attach(newRole).State = EntityState.Added;
                    await _context.SaveChangesAsync();
                }
            }

            return Page();
        }

        private bool LicencieExists(int id)
        {
            return _context.Licencie.Any(e => e.ID == id);
        }

        private bool LicencieExists(string userId)
        {
            return _context.Licencie.Any(e => e.IdUtilisateur == userId);
        }
    }
}
