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
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "Administrateur")]
    public class DetailsModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(projetEsport.Data.ApplicationDbContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        [BindProperty]
        public CompetitionViewModel Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions
                .Include(c => c.Proprietaire)
                .Include(c => c.TypeCompetition)
                .Include(c => c.Jeu)
                .Include(c => c.EquipesDeLaCompetition).ThenInclude(e => e.Equipe).ThenInclude(e => e.Membres)
                .Include(c => c.MatchesDisputes).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    EquipesDeLaCompetition = _context.Equipes.Include(e => e.Membres).Where(e => e.IsApproved && e.JeuID.Equals(c.JeuID)).Select(e => new EquipeViewModel
                    {
                        ID = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID)) ?
                        _context.CompetitionEquipe.FirstOrDefault(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID)).ID : 0,
                        EquipeID = e.ID,
                        CompetitionID = c.ID,
                        Nom = e.Nom,
                        JeuID = e.JeuID,
                        Membres = e.Membres.Select(m => new LicencieViewModel
                        {
                            ID = m.ID,
                            Pseudo = m.Pseudo
                        }).ToList(),
                        IsInCompetition = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID)),
                        EncoreEnCompetition = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID) && ce.EncoreEnCompetition)
                    }).ToList(),
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
                    JeuID = c.JeuID,
                    Jeu = new CompetitionJeuViewModel
                    {
                        ID = c.JeuID,
                        Nom = c.Jeu.Nom
                    },
                    Nom = c.Nom,
                    ProprietaireID = c.ProprietaireID,
                    Proprietaire = c.Proprietaire.Pseudo,
                    TypeCompetitionID = c.TypeCompetitionID,
                    TypeCompetition = c.TypeCompetition.Nom
                }).FirstOrDefaultAsync(c => c.ID.Equals(id));

            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAddEquipeAsync(EquipeViewModel equipe)
        {
            try
            {
                if (!equipe.JeuID.Equals(Competition.JeuID))
                {
                    return RedirectToPage(new
                    {
                        id = (int?)equipe.CompetitionID,
                    });
                }

                CompetitionEquipe competitionEquipe = new CompetitionEquipe
                {
                    CompetitionID = equipe.CompetitionID,
                    EquipeID = equipe.EquipeID,
                    EncoreEnCompetition = true
                };

                _context.CompetitionEquipe.Add(competitionEquipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage(new
                {
                    id = (int?)equipe.CompetitionID,
                });
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)equipe.CompetitionID,
            });
        }
        
        public async Task<IActionResult> OnPostRemoveEquipeAsync(EquipeViewModel equipe)
        {
            CompetitionEquipe competitionEquipe = null;

            try
            {
                //User role
                if (equipe.ID.Equals(0))
                {
                    competitionEquipe = await _context.CompetitionEquipe.FirstOrDefaultAsync(e => e.EquipeID.Equals(equipe.EquipeID) && e.CompetitionID.Equals(equipe.CompetitionID));
                }
                else
                {
                    competitionEquipe = await _context.CompetitionEquipe.FirstOrDefaultAsync(e => e.ID.Equals(equipe.ID));
                }

                _context.CompetitionEquipe.Remove(competitionEquipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage(new
                {
                    id = (int?)equipe.CompetitionID,
                });
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)equipe.CompetitionID,
            });
        }
    }
}
