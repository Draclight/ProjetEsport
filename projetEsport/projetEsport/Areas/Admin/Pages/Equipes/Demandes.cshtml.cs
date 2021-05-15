using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Equipes
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DemandesModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public DemandesModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EquipeViewModel> Equipes { get; set; }

        public void OnGetAsync()
        {
            Equipes = _context.Equipe.Include(i => i.Membres).Where(e => !e.IsApproved).Select(e => new EquipeViewModel()
            {
                Equipe = e,
                ID = e.ID,
                Invitations = _context.InvitationEquipe.Include(i => i.Equipe).Include(i => i.Licencie).Where(i => i.EquipeID == e.ID).ToList()
            }).ToList();
        }

        public async Task<IActionResult> OnPostApproveEquipeAsync(int id)
        {
            Equipe equipe = await _context.Equipe.FirstOrDefaultAsync(e => e.ID == id);
            var invitations = await _context.InvitationEquipe.Where(i => i.EquipeID == equipe.ID).ToListAsync();
            if (invitations.All(i => i.IsAccepted))
            {
                equipe.IsApproved = true;
                _context.Attach(equipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                //Assignation de l'équipe aux membres
                foreach (var i in invitations)
                {
                    var licencie = await _context.Licencie.FirstAsync(l => l.ID.Equals(i.LicencieID));
                    licencie.EquipeID = i.EquipeID;
                    _context.Attach(licencie).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage();
        }
    }
}
