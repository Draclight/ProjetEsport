using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Authorization;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public bool DisplayAdmin { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Pseudo")]
            public string Pseudo { get; set; }
            [Display(Name = "Nom")]
            public string Nom { get; set; }
            [Display(Name = "Prénom")]
            public string Prenom { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var licencie = await _context.Licencies.Include(l => l.Equipe).FirstOrDefaultAsync(l => l.UtilisateurID == user.Id);

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Pseudo = userName,
                Nom = (string.IsNullOrEmpty(licencie.Nom) ? string.Empty : licencie.Nom),
                Prenom = (string.IsNullOrEmpty(licencie.Prenom) ? string.Empty : licencie.Prenom)
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            DisplayAdmin = await _userManager.IsInRoleAsync(user, Constants.AdministrateursRole);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            if (Input.Pseudo != userName)
            {
                try
                {
                    await _userManager.SetUserNameAsync(user, Input.Pseudo);

                    Licencie licencie = await _context.Licencies.Include(l => l.Equipe).FirstOrDefaultAsync(l => l.UtilisateurID == user.Id);
                    licencie.Pseudo = Input.Pseudo;
                    licencie.Nom = Input.Nom;
                    licencie.Prenom = Input.Prenom;
                    _context.Attach(licencie).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    StatusMessage = "Unexpected error when trying to set user name.";
                    return RedirectToPage();
                }

            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
