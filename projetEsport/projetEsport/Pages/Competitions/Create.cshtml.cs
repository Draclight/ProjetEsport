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
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Competitions
{
    [Authorize(Roles = "Administrateur,Organisateur")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<DetailsModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, ILogger<DetailsModel> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var date = DateTime.Now;

            Competition = new CompetitionViewModel
            {
                DateDebut = date,
                DateFin = date
            };

            ViewData["ProprietaireID"] = new SelectList(_context.Licencies, "ID", "ID");
            ViewData["TypeCompetitionID"] = new SelectList(_context.TypesDeCompetition, "ID", "Nom"); 
            ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom"); 
            return Page();
        }

        [BindProperty]
        public CompetitionViewModel Competition { get; set; }
        public Competition NouvelleCompetition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var date = DateTime.Now;
            var propriétaire = await _context.Licencies.FirstOrDefaultAsync(l => l.UtilisateurID.Equals(_userManager.GetUserId(User)));
            NouvelleCompetition = new Competition
            {
                CreeLe = date,
                ModifieeLe = date,
                DateDebut = Competition.DateDebut,
                DateFin = Competition.DateFin,
                JeuID = Competition.JeuID,
                Nom = Competition.Nom,
                ProprietaireID = propriétaire.ID,
                TypeCompetitionID = Competition.TypeCompetitionID
            };

            _context.Competitions.Add(NouvelleCompetition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
