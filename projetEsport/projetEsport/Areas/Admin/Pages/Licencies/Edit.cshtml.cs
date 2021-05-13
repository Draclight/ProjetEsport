using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Licencies
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public EditModel(projetEsport.Data.ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public LicencieViewModel Licencie { get; set; }
        [BindProperty]
        public IList<RoleViewModel> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Licencie = new LicencieViewModel();
            Licencie.licencie = await _context.Licencie.Include(l => l.Equipe).FirstOrDefaultAsync(m => m.ID == id);

            try
            {
                //Roles = await _context.Roles.Select(role => new RoleViewModel()
                //{
                //    RoleId = role.Id,
                //    LicencieUserId = Licencie.licencie.IdUtilisateur,
                //    LicencieId = Licencie.licencie.ID,
                //    RoleName = role.Name,
                //    IsAcquired = _context.UserRoles.Any(ur => ur.RoleId == role.Id && ur.UserId == Licencie.licencie.IdUtilisateur)
                //}).ToListAsync();
                Licencie.Roles = await _context.Roles.Select(role => new RoleViewModel()
                {
                    RoleId = role.Id,
                    LicencieUserId = Licencie.licencie.IdUtilisateur,
                    LicencieId = Licencie.licencie.ID,
                    RoleName = role.Name,
                    IsAcquired = _context.UserRoles.Any(ur => ur.RoleId == role.Id && ur.UserId == Licencie.licencie.IdUtilisateur)
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (Licencie == null)
            {
                return NotFound();
            }

            //Equipe
            var equipesListe = new List<Equipe>();
            equipesListe.Add(new Equipe());
            await _context.Equipe.ForEachAsync(e => equipesListe.Add(e));
            ViewData["EquipeID"] = new SelectList(equipesListe, "ID", "Nom");
            return Page();
        }

        public async Task<IActionResult> OnPostSaveLicencieAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Licencie.licencie.EquipeID.Equals(0))
            {
                Licencie.licencie.EquipeID = null;
                Licencie.licencie.Equipe = null;
            }

            Licencie.licencie.ModifieeLe = DateTime.UtcNow;
            _context.Attach(Licencie.licencie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LicencieExists(Licencie.licencie.ID))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage(new
            {
                id = (int?)Licencie.licencie.ID,
            });
        }

        public async Task<IActionResult> OnPostAddRoleAsync(RoleViewModel role)
        {
            try
            {
                //User role
                IdentityUserRole<string> newRole = new IdentityUserRole<string>();
                newRole.RoleId = _context.Roles.First(r => r.Id.Equals(role.RoleId)).Id;
                newRole.UserId = role.LicencieUserId;
                _context.UserRoles.Add(newRole);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LicencieExists(Licencie.licencie.ID))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage(new
            {
                id = (int?)role.LicencieId,
            });
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(RoleViewModel role)
        {
            try
            {
                //User role
                IdentityUserRole<string> formerRole = new IdentityUserRole<string>();
                formerRole.RoleId = _context.Roles.First(r => r.Id.Equals(role.RoleId)).Id;
                formerRole.UserId = role.LicencieUserId;
                _context.UserRoles.Remove(formerRole);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LicencieExists(Licencie.licencie.ID))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            return RedirectToPage(new
            {
                id = (int?)role.LicencieId,
            });
        }

        private bool LicencieExists(int id)
        {
            return _context.Licencie.Any(e => e.ID == id);
        }
    }
}
