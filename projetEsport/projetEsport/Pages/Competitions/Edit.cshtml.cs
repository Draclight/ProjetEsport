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

namespace projetEsport.Pages.Competitions
{
    [Authorize(Roles = "Administrateur,Organisateur")]
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
                        IsInCompetition = _context.CompetitionEquipe.Any(ce => ce.CompetitionID.Equals(c.ID) && ce.EquipeID.Equals(e.ID))
                    }).ToList(),
                    JeuID = c.JeuID,
                    Jeu = new CompetitionJeuViewModel
                    {
                        ID = c.JeuID,
                        Nom = c.Jeu.Nom
                    },
                    ModifieeLe = c.ModifieeLe,
                    NbEquipes = c.EquipesDeLaCompetition.Count,
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

            ViewData["ProprietaireID"] = new SelectList(_context.Licencies, "ID", "Pseudo");
            ViewData["TypeCompetitionID"] = new SelectList(_context.TypesDeCompetition, "ID", "Nom");
            ViewData["JeuID"] = new SelectList(_context.Jeux, "ID", "Nom");
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Competition editCompetition = await _context.Competitions.Include(c => c.Proprietaire).FirstAsync(c => c.ID.Equals(Competition.ID));
            editCompetition.ModifieeLe = DateTime.Now;
            editCompetition.DateDebut = Competition.DateDebut;
            editCompetition.DateFin = Competition.DateFin;
            editCompetition.Nom = Competition.Nom;
            editCompetition.TypeCompetitionID = Competition.TypeCompetitionID;
            if (!_context.CompetitionEquipe.Any(c => c.CompetitionID.Equals(editCompetition.ID)))
            {
                editCompetition.JeuID = Competition.JeuID;
            }

            _context.Attach(editCompetition).State = EntityState.Modified;

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

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.ID == id);
        }
    }
}
