using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions
{
    [Authorize(Roles = "Administrateur")]
    public class EditModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(projetEsport.Data.ApplicationDbContext context, ILogger<EditModel> logger)
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
                .Include(c => c.JeuxDeLaCompetition).ThenInclude(j => j.JeuxDeLaCompetition)
                .Include(c => c.EquipesDeLaCompetition).ThenInclude(e => e.Equipe).ThenInclude(e => e.Membres)
                .Include(c => c.MatchesDisputes).Select(c => new CompetitionViewModel
                {
                    ID = c.ID,
                    CreeLe = c.CreeLe,
                    DateDebut = c.DateDebut,
                    DateFin = c.DateFin,
                    EquipesDeLaCompetition = _context.Equipes.Include(e => e.Membres).Select(e => new EquipeViewModel
                    {
                        ID = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID)) ?
                        _context.CompetitionEquipe.FirstOrDefault(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID)).ID : 0,
                        EquipeID = e.ID,
                        CompetitionID = c.ID,
                        Nom = e.Nom,
                        Membres = e.Membres.Select(m => new LicencieViewModel
                        {
                            ID = m.ID,
                            Pseudo = m.Pseudo
                        }).ToList(),
                        IsInCompetition = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID))
                    }).ToList(),
                    JeuxDeLaCompetition = _context.Jeux.Select(j => new CompetitionJeuViewModel
                    {
                        ID = _context.CompetitionJeu.Any(j => j.CompetitionsJeuSelectionneID.Equals(c.ID) && j.JeuxDeLaCompetitionID.Equals(j.ID)) ?
                        _context.CompetitionJeu.FirstOrDefault(j => j.CompetitionsJeuSelectionneID.Equals(c.ID) && j.JeuxDeLaCompetitionID.Equals(j.ID)).ID : 0,
                        JeuID = j.ID,
                        CompetitionID = c.ID,
                        Nom = j.Nom,
                        IsInCompetition = _context.CompetitionJeu.Any(cj => cj.CompetitionsJeuSelectionneID.Equals(c.ID) && cj.JeuxDeLaCompetitionID.Equals(j.ID))
                    }).ToList(),
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
                    NbJeux = c.JeuxDeLaCompetition.Count,
                    Nom = c.Nom,
                    ProprietaireID = c.ProprietaireID,
                    Proprietaire = c.Proprietaire.Pseudo,
                    TypeCompetitionID = c.TypeCompetitionID,
                    TypeCompetition = c.TypeCompetition.Nom
                }).FirstOrDefaultAsync(c => c.ID.Equals(id));

            //foreach (var jeu in Competition.JeuxDeLaCompetition)
            //{
            //    jeu.IsInCompetition = _context.CompetitionJeu.Any(j => j.CompetitionsJeuSelectionneID.Equals(Competition.ID) && j.JeuxDeLaCompetitionID.Equals(jeu.JeuID));
            //}

            if (Competition == null)
            {
                return NotFound();
            }
            ViewData["ProprietaireID"] = new SelectList(_context.Licencies, "ID", "Pseudo");
            ViewData["TypeCompetitionID"] = new SelectList(_context.TypesDeCompetition, "ID", "Nom");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Competition newCompetition = new Competition
            {
                ID = Competition.ID,
                CreeLe = Competition.CreeLe,
                ModifieeLe = DateTime.Now,
                DateDebut = Competition.DateDebut,
                DateFin = Competition.DateFin,
                Nom = Competition.Nom,
                TypeCompetitionID = Competition.TypeCompetitionID,
                ProprietaireID = Competition.ProprietaireID
            };
            _context.Attach(newCompetition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(Competition.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAddEquipeAsync(EquipeViewModel equipe)
        {
            try
            {
                CompetitionEquipe competitionEquipe = new CompetitionEquipe
                {
                    CompetitionID = equipe.CompetitionID,
                    EquipeID = equipe.EquipeID
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

        public async Task<IActionResult> OnPostAddJeuAsync(CompetitionJeuViewModel jeu)
        {
            try
            {
                CompetitionJeu competitionJeu = new CompetitionJeu
                {
                    CompetitionsJeuSelectionneID = jeu.CompetitionID,
                    JeuxDeLaCompetitionID = jeu.JeuID
                };

                _context.CompetitionJeu.Add(competitionJeu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage(new
                {
                    id = (int?)jeu.CompetitionID,
                });
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)jeu.CompetitionID,
            });
        }

        public async Task<IActionResult> OnPostRemoveJeuAsync(CompetitionJeuViewModel jeu)
        {

            CompetitionJeu competitionJeu = null;
            try
            {
                if (jeu.ID.Equals(0))
                {
                    competitionJeu = await _context.CompetitionJeu.FirstOrDefaultAsync(j => j.JeuxDeLaCompetitionID.Equals(jeu.JeuID) && j.CompetitionsJeuSelectionneID.Equals(jeu.CompetitionID));
                }
                else
                {
                    competitionJeu = await _context.CompetitionJeu.FirstOrDefaultAsync(j => j.ID.Equals(jeu.ID));
                }

                _context.CompetitionJeu.Remove(competitionJeu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToPage(new
                {
                    id = (int?)jeu.CompetitionID,
                });
                throw;
            }

            return RedirectToPage(new
            {
                id = (int?)jeu.CompetitionID,
            });
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.ID == id);
        }
    }
}
