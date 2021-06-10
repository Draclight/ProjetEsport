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
using projetEsport.ViewModels;

namespace projetEsport.Pages.Competitions.Matches
{
    [Authorize(Roles = "Administrateur,Organisateur")]
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
        public MatcheViewModel Matche { get; set; }
        public Matche dbMatche { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dbMatche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.Competition).ThenInclude(m => m.Proprietaire)
                .Include(m => m.EquipesDisputes).ThenInclude(ed => ed.EquipesDisputes)
                .Include(m => m.TypeMatche).FirstOrDefaultAsync(m => m.ID == id);

            if (dbMatche == null)
            {
                return NotFound();
            }

            Matche = new MatcheViewModel
            {
                ID = dbMatche.ID,
                CompetitionID = dbMatche.CompetitionID,
                CompetitionNom = dbMatche.Competition.Nom,
                CreeLe = dbMatche.CreeLe,
                Date = dbMatche.DateMatche,
                JeuID = dbMatche.Competition.JeuID,
                JeuNom = dbMatche.Competition.Jeu.Nom,
                TypeMatche = dbMatche.TypeMatche.Nom,
                NbVictoiresEquipeA = dbMatche.VictoireEquipeA,
                NbVictoiresEquipeB = dbMatche.VictoireEquipeB,
                ModifieeLe = dbMatche.ModifieeLe,
                EquipeANom = dbMatche.EquipesDisputes.ToArray()[0].EquipesDisputes.Nom,
                EquipeBNom = dbMatche.EquipesDisputes.ToArray()[1].EquipesDisputes.Nom,
                IsProprietaire = dbMatche.Competition.Proprietaire.UtilisateurID.Equals(_userManager.GetUserId(User))
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EquipesMatche = await _context.EquipeMatche.Where(em => em.MatchesDisputesID.Equals(id)).ToListAsync();

            if (EquipesMatche != null)
            {
                _context.EquipeMatche.RemoveRange(EquipesMatche);
                await _context.SaveChangesAsync();
            }

            var Matche = await _context.Matches.FindAsync(id);

            if (Matche != null)
            {
                _context.Matches.Remove(Matche);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new
            {
                id = (int?)Matche.CompetitionID
            });
        }
    }
}
