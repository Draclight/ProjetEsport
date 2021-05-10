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
using projetEsport.ViewModels;
using projetEsport.Data;
using projetEsport.Models;

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["TypeCompetitionID"] = new SelectList(_context.TypeCompetition, "ID", "Nom");

            JeuxDisponible = await _context.Jeu.Select(jeu => new CompetitionViewModel()
            {
                JeuID = jeu.ID,
                Nom = jeu.Nom,
                IsInCompetition = false
            }).ToArrayAsync();

            return Page();
        }

        [BindProperty]
        public Competition Competition { get; set; }
        [BindProperty]
        public CompetitionViewModel[] JeuxDisponible { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (JeuxDisponible.All(j => !j.IsInCompetition))
            {
                return RedirectToPage();
            }

            #region Compétition
            //Propriétaire
            var licencie = await _context.Licencie.FirstOrDefaultAsync(l => l.IdUtilisateur == _userManager.GetUserId(User));
            Competition.ProprietaireID = licencie.ID;
            var dateTimeCreate = DateTime.UtcNow;
            Competition.CreeLe = dateTimeCreate;
            Competition.ModifieeLe = dateTimeCreate;

            //Sauvegarde
            _context.Competition.Add(Competition);
            await _context.SaveChangesAsync();
            #endregion

            #region Jeux
            //Jeux
            //Récupération des ids des jeux sélectionnés
            var IdJeux = from jd in JeuxDisponible
                         where jd.IsInCompetition.Equals(true)
                         select jd.JeuID;

            var competitionJeux = IdJeux.Select(jd => new CompetitionJeu()
            {
                CompetitionID = Competition.ID,
                JeuID = jd
            }).ToList();

            _context.CompetitionJeu.AddRange(competitionJeux);
            await _context.SaveChangesAsync();
            #endregion

            return RedirectToPage("./Index");
        }
    }
}
