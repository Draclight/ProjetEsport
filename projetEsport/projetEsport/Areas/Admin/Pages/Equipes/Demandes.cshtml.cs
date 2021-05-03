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

        public IList<Equipe> Equipe { get; set; }
        public IList<EquipeViewModel> Equipes { get; set; }

        public async Task OnGetAsync()
        {
            Equipe = await _context.Equipe.Include(e => e.Membres).Where(e => e.IsApproved == false).ToListAsync();
            Equipes = Equipe.Select(e => new EquipeViewModel()
            {
                Equipe = e,
                Invitations = _context.InvitationEquipe.Include(i => i.Equipe).Include(i => i.Licencie).Where(i => i.EquipeID == e.ID).ToList(),
                Membres = e.Membres
            }).ToList();
        }

        public async Task OnPostApproveEquipeAsync(int id)
        {
            Equipe equipe = await _context.Equipe.FirstOrDefaultAsync(e => e.ID == id);
            equipe.IsApproved = true;
            _context.Attach(equipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //Assignation de l'équipe aux membres
            var invitations = await _context.InvitationEquipe.Include(i => i.Licencie).Include(i => i.Equipe).Where(i => i.EquipeID == equipe.ID).ToListAsync();
            foreach (var l in invitations)
            {
                l.EquipeID = equipe.ID;
                _context.Attach(l).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            Page();
        }
    }
}
