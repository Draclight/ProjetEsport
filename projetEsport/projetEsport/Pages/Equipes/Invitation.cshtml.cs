using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    [Authorize(Roles = "Administrateur,Organisateur,Licencie")]
    public class InvitationModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<InvitationModel> _logger;

        public InvitationModel(projetEsport.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<InvitationModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IList<InvitationViewModel> InvitationEquipe { get; set; }

        public async Task OnGetAsync()
        {
            InvitationEquipe = new List<InvitationViewModel>();
            InvitationEquipe = await _context.InvitationsEquipes.Include(i => i.Equipe).ThenInclude(i => i.Membres).Include(i => i.Licencie)
                .Where(i => i.Licencie.UtilisateurID.Equals(_userManager.GetUserId(User)) && i.IsAccepted.Equals(false)).Select(ivm => new InvitationViewModel
                {
                    ID = ivm.ID,
                    LicencieID = ivm.LicencieID,
                    NomEquipe = ivm.Equipe.Nom,
                    Membres = _context.InvitationsEquipes.Include(i => i.Licencie).Select(i => new MembreViewModel
                    {
                        Pseudo = i.Licencie.Pseudo,
                        IsAccepter = i.IsAccepted
                    }).ToList(),
                    DateEnvoi = ivm.Equipe.CreeLe,
                    DateAccepter = ivm.DateAccepter,
                    Accepter = ivm.IsAccepted
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPostAccepterInvitation(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationsEquipes.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            invitationEquipe.IsAccepted = true;
            invitationEquipe.DateAccepter = DateTime.Now;
            try
            {
                _context.Attach(invitationEquipe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejeterInvitation(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationsEquipes.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            try
            {
                _context.InvitationsEquipes.Remove(invitationEquipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage();
                throw;
            }
            return RedirectToPage();
        }
    }
}
