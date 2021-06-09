﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
{
    [Authorize(Roles = "Administrateur")]
    public class CreateModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public CreateModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Matche = new MatcheViewModel();
            var date = DateTime.Now;
            Matche.CompetitionID = id;
            Matche.Date = date;
            Matche.CreeLe = date;
            Matche.ModifieeLe = date;

            ViewData["CompetitionID"] = new SelectList(_context.Competitions.Where(c => c.ID.Equals(id)).ToList(), "ID", "Nom");
            ViewData["TypeMatcheID"] = new SelectList(_context.TypesDeMatche, "ID", "Nom");

            var equipesEncoreEnCompetition = from ce in _context.CompetitionEquipe.Include(ce => ce.Equipe)
                          join em in _context.EquipeMatche on ce.EquipeID equals em.EquipesDisputesID
                          join m in _context.Matches on em.MatchesDisputesID equals m.ID
                          where ce.CompetitionID.Equals(id) && ce.EncoreEnCompetition && m.MatcheTeminer == true
                          select ce;

            var equipes = equipesEncoreEnCompetition.Count() > 0 ? equipesEncoreEnCompetition.ToList() : _context.CompetitionEquipe.Include(ce => ce.Equipe).ToList();

            ViewData["EquipeID"] = new SelectList(equipes, "EquipeID", "Equipe.Nom");

            return Page();
        }

        [BindProperty]
        public MatcheViewModel Matche { get; set; }
        public Matche NouveauMatche { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Matche.EquipeAID.Equals(Matche.EquipeBID))
            {
                return RedirectToPage(new
                {
                    id = (int?)Matche.CompetitionID
                });
            }

            var date = DateTime.Now;
            //Matche
            NouveauMatche = new Matche
            {
                CompetitionID = Matche.CompetitionID,
                CreeLe = date,
                ModifieeLe = date,
                DateMatche = Matche.Date,
                TypeMatcheID = Matche.TypeMatcheID,
                VictoireEquipeA = 0,
                VictoireEquipeB = 0
            };

            _context.Matches.Add(NouveauMatche);
            await _context.SaveChangesAsync();

            //Equipes du matche
            EquipeMatche equipeA = new EquipeMatche
            {
                EquipesDisputesID = Matche.EquipeAID,
                MatchesDisputesID = NouveauMatche.ID
            };

            EquipeMatche equipeB = new EquipeMatche
            {
                EquipesDisputesID = Matche.EquipeBID,
                MatchesDisputesID = NouveauMatche.ID
            };

            _context.EquipeMatche.Add(equipeA);
            _context.EquipeMatche.Add(equipeB);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                id = (int?)NouveauMatche.CompetitionID,
            });
        }
    }
}
