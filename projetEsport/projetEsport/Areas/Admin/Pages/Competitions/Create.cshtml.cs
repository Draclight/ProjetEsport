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

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "Administrateur")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var date = DateTime.Now;
            CompetitionVM = new CompetitionViewModel
            {
                CreeLe = date,
                ModifieeLe = date,
                DateDebut = date,
                DateFin = date,
                EquipesDeLaCompetition = await _context.Equipes.Where(e => e.IsApproved).Select(e => new EquipeViewModel
                {
                    ID = e.ID,
                    Nom = e.Nom,
                    JeuID = e.JeuID
                }).ToListAsync()
            };

            ViewData["ProprietaireID"] = new SelectList(_context.Licencies, "ID", "Pseudo");
            ViewData["TypeCompetitionID"] = new SelectList(_context.TypesDeCompetition, "ID", "Nom");
            ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");

            return Page();
        }

        [BindProperty]
        public CompetitionViewModel CompetitionVM { get; set; }
        public Competition Competition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            try
            {
                //Vérifiraction propriétaire à les droits organisateur
                var administrateursRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(Constants.AdministrateursRole));
                var organisteursRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(Constants.OrganisteursRole));
                var licencie = await _context.Licencies.FirstOrDefaultAsync(l => l.ID.Equals(CompetitionVM.ProprietaireID));
                var licencieIsOrganisateur = await _context.UserRoles.AnyAsync(ur => ur.UserId.Equals(licencie.UtilisateurID) && ur.RoleId == organisteursRole.Id.ToString());
                var licencieIsAdmin = await _context.UserRoles.AnyAsync(ur => ur.UserId.Equals(licencie.UtilisateurID) && ur.RoleId == administrateursRole.Id.ToString());

                if (licencieIsOrganisateur == false && licencieIsAdmin == false)
                {
                    return RedirectToPage();
                }

                //Compétitions
                Competition = new Competition();
                Competition.CreeLe = DateTime.Now;
                Competition.ModifieeLe = DateTime.Now;
                Competition.DateDebut = CompetitionVM.DateDebut;
                Competition.DateFin = CompetitionVM.DateFin;
                Competition.Nom = CompetitionVM.Nom;
                Competition.TypeCompetitionID = CompetitionVM.TypeCompetitionID;
                Competition.ProprietaireID = CompetitionVM.ProprietaireID;
                //Jeux
                Competition.JeuID = CompetitionVM.JeuID;

                _context.Competitions.Add(Competition);
                await _context.SaveChangesAsync();

                //Equipes
                //if (CompetitionVM.EquipesDeLaCompetition != null)
                //{
                //    foreach (EquipeViewModel equipe in CompetitionVM.EquipesDeLaCompetition)
                //    {
                //        if (equipe.IsInCompetition)
                //        {
                //            CompetitionEquipe competitionEquipe = new CompetitionEquipe
                //            {
                //                CompetitionID = Competition.ID,
                //                EquipeID = equipe.ID
                //            };
                //            _context.CompetitionEquipe.Add(competitionEquipe);
                //            await _context.SaveChangesAsync();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage();
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
