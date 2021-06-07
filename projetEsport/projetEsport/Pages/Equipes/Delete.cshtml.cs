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
using projetEsport.Models;

namespace projetEsport.Pages.Equipes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public DeleteModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Equipe Equipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.Equipes
                .Include(e => e.Jeu).FirstOrDefaultAsync(m => m.ID == id);

            if (Equipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createur = await _context.Licencies.Where(l => l.EquipeID.Equals(id) && l.CreateurEquipe).FirstOrDefaultAsync();
            var isCreateur = createur.UtilisateurID.Equals(_userManager.GetUserId(User));

            if (isCreateur)
            {
                Equipe = await _context.Equipes.FindAsync(id);

                if (Equipe != null)
                {
                    try
                    {
                        foreach (var membre in Equipe.Membres)
                        {
                            membre.EquipeID = null;

                            _context.Licencies.Remove(membre);
                            await _context.SaveChangesAsync();
                        }

                        _context.Equipes.Remove(Equipe);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToPage("./Index");
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
