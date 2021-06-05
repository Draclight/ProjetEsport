using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Equipes
{
    [Authorize(Roles = "Administrateur")]
    public class ApprouveEquipesModel : PageModel
    {
        private readonly ILogger<ApprouveEquipesModel> _logger;
        private readonly ApplicationDbContext _context;

        public ApprouveEquipesModel(ApplicationDbContext context, ILogger<ApprouveEquipesModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<EquipeViewModel> Equipes { get; set; }
        public async Task OnGetAsync()
        {
            Equipes = await _context.Equipes.Where(e => !e.IsApproved).Select(e => new EquipeViewModel
            {
                ID = e.ID,
                Nom = e.Nom,
                Invitations = _context.InvitationsEquipes.Include(ie => ie.Licencie).Where(ie => ie.EquipeID.Equals(e.ID)).Select(ie => new InvitationViewModel
                {
                    ID = ie.ID,
                    PseudoLicencie = ie.Licencie.Pseudo,
                    Accepter = ie.IsAccepted
                }).ToList()
            }).ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveEquipeAsync(int id)
        {
            var isAllInvitationsAccepte = await _context.InvitationsEquipes.Where(e => e.ID.Equals(id)).AllAsync(i => i.IsAccepted);

            if (isAllInvitationsAccepte == false)
            {
                return RedirectToPage();
            }

            var Equipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID.Equals(id));

            if (Equipe != null)
            {
                try
                {
                    //Equipe
                    Equipe.IsApproved = true;
                    Equipe.ModifieeLe = DateTime.Now;
                    _context.Attach(Equipe).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    //Licencies
                    var invitations = await _context.InvitationsEquipes.Include(ie => ie.Equipe).Include(ie => ie.Licencie).Where(ie => ie.EquipeID.Equals(id)).ToListAsync();
                    foreach (var invitation in invitations)
                    {
                        var licenie = invitation.Licencie;
                        licenie.EquipeID = Equipe.ID;
                        _context.Attach(licenie).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipeExists(Equipe.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectEquipeAsync(string id)
        {
            var Equipe = await _context.Equipes.FirstOrDefaultAsync(e => e.ID.Equals(id));

            if (Equipe != null)
            {
                try
                {
                    Equipe.IsApproved = false;
                    Equipe.ModifieeLe = DateTime.Now;
                    _context.Attach(Equipe).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipeExists(Equipe.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }

        private bool EquipeExists(int id)
        {
            return _context.Equipes.Any(e => e.ID == id);
        }
    }
}
