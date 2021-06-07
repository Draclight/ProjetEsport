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

namespace projetEsport.Pages.Equipes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public EquipeViewModel EquipeVM { get; set; }
        public Equipe Equipe { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.Equipes
                .Include(e => e.Membres)
                .Include(e => e.Jeu).FirstOrDefaultAsync(m => m.ID == id);

            if (Equipe == null)
            {
                return NotFound();
            }

            EquipeVM = new EquipeViewModel
            {
                ID = Equipe.ID,
                Nom = Equipe.Nom,
                Membres = Equipe.Membres.Select(m => new LicencieViewModel
                {
                    ID = m.ID,
                    Pseudo = m.Pseudo
                }).ToList(),
                JeuNom = Equipe.Jeu.Nom,
                IsProprietaire = _context.Licencies.Include(l => l.Equipe).FirstOrDefault(l => l.EquipeID.Equals(Equipe.ID) && l.CreateurEquipe).UtilisateurID.Equals(_userManager.GetUserId(User))
            };

            return Page();
        }
    }
}
