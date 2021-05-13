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
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Pages.Equipes
{
    [Authorize(Roles = "ADMINISTRATEUR,ORGANISATEUR,LICENCIE")]
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
            InvitationEquipe = await _context.InvitationEquipe.Include(i => i.Equipe).ThenInclude(i => i.Membres).Include(i => i.Licencie)
                .Where(i => i.Licencie.IdUtilisateur.Equals(_userManager.GetUserId(User)) && i.IsAccepted.Equals(false)).Select(ivm => new InvitationViewModel
                {
                    ID = ivm.ID,
                    LicencieID = ivm.LicencieID,
                    NomEquipe = ivm.Equipe.Nom,
                    Membres = _context.InvitationEquipe.Include(i => i.Licencie).Select(i => i.Licencie.Pseudo).ToList(),
                    Quand = ivm.Equipe.CreeLe,
                    Accepter = ivm.IsAccepted
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPostAccepterInvitation(InvitationViewModel invitation)
        {
            var invitationEquipe = await _context.InvitationEquipe.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            invitationEquipe.IsAccepted = true;
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
            var invitationEquipe = await _context.InvitationEquipe.Include(ie => ie.Licencie).Include(ie => ie.Equipe)
                .FirstOrDefaultAsync(ie => ie.ID.Equals(invitation.ID));

            try
            {
                _context.InvitationEquipe.Remove(invitationEquipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return RedirectToPage();
        }
    }
}
