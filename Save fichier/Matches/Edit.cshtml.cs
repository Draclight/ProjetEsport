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

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
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
        public MatcheViewModel Matche { get; set; }
        public Matche EditMatche { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditMatche = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.EquipesDisputes).ThenInclude(e => e.EquipesDisputes)
                .Include(m => m.TypeMatche).FirstOrDefaultAsync(m => m.ID == id);

            if (EditMatche == null)
            {
                return NotFound();
            }

            Matche = new MatcheViewModel
            {
                ID = EditMatche.ID,
                CompetitionID = EditMatche.CompetitionID,
                CompetitionNom = EditMatche.Competition.Nom,
                CreeLe = EditMatche.CreeLe,
                ModifieeLe = EditMatche.ModifieeLe,
                Date = EditMatche.DateMatche,
                EquipeAID = EditMatche.EquipesDisputes.ToArray()[0].EquipesDisputesID,
                EquipeANom = EditMatche.EquipesDisputes.ToArray()[0].EquipesDisputes.Nom,
                EquipeBID = EditMatche.EquipesDisputes.ToArray()[1].EquipesDisputesID,
                EquipeBNom = EditMatche.EquipesDisputes.ToArray()[1].EquipesDisputes.Nom,
                NbVictoiresEquipeA = EditMatche.VictoireEquipeA,
                NbVictoiresEquipeB = EditMatche.VictoireEquipeB,
                TypeMatcheID = EditMatche.TypeMatcheID,
                MatchID = EditMatche.ID
            };

            ViewData["CompetitionID"] = new SelectList(_context.Competitions.Where(c => c.ID.Equals(id)).ToList(), "ID", "Nom");
            ViewData["TypeMatcheID"] = new SelectList(_context.TypesDeMatche, "ID", "Nom");
            ViewData["EquipeID"] = new SelectList(_context.CompetitionEquipe.Include(ce => ce.Equipe).Where(ce => ce.CompetitionID.Equals(id)).ToList(), "EquipeID", "Equipe.Nom");

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

            try
            {
                //Matche
                EditMatche = await _context.Matches.FindAsync(Matche.ID);
                if (EditMatche != null)
                {
                    var date = DateTime.Now;
                    EditMatche.DateMatche = Matche.Date;
                    EditMatche.ModifieeLe = date;
                    EditMatche.TypeMatcheID = Matche.TypeMatcheID;
                    EditMatche.VictoireEquipeA = Matche.NbVictoiresEquipeA;
                    EditMatche.VictoireEquipeB = Matche.NbVictoiresEquipeB;

                    _context.Attach(EditMatche).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    //Equipes du matche
                    var equipesDuMatche = await _context.EquipeMatche.Include(em => em.EquipesDisputes).Where(em => em.MatchesDisputesID.Equals(EditMatche.ID)).ToArrayAsync();
                    equipesDuMatche[0].EquipesDisputesID = Matche.EquipeAID;
                    equipesDuMatche[1].EquipesDisputesID = Matche.EquipeBID;

                    _context.Attach(equipesDuMatche[0]).State = EntityState.Modified;
                    _context.Attach(equipesDuMatche[1]).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {

                if (!MatcheExists(Matche.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new
            {
                id = (int?)EditMatche.CompetitionID
            });
        }

        private bool MatcheExists(int id)
        {
            return _context.Matches.Any(e => e.ID == id);
        }
    }
}
