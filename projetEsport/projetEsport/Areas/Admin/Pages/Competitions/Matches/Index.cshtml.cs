using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEsport.Data;
using projetEsport.Models;
using projetEsport.ViewModels;

namespace projetEsport.Areas.Admin.Pages.Competitions.Matches
{
    [Authorize(Roles = "Administrateur")]
    public class IndexModel : PageModel
    {
        private readonly projetEsport.Data.ApplicationDbContext _context;

        public IndexModel(projetEsport.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MatcheViewModel> Matches { get; set; }
        [BindProperty]
        public int CompetitionID { get; set; }
        public bool MatchesTerminer { get; set; }

        public async Task OnGetAsync(int? id)
        {
            CompetitionID = (int)id;

            Matches = await _context.Matches
                .Include(m => m.Competition).ThenInclude(m => m.Jeu)
                .Include(m => m.EquipesDisputes).ThenInclude(e => e.EquipesDisputes)
                .Include(m => m.TypeMatche).Where(m => m.CompetitionID.Equals(id)).Select(m => new MatcheViewModel
                {
                    ID = m.ID,
                    EquipesDuMatche = _context.EquipeMatche.Include(e => e.EquipesDisputes).Where(e => e.MatchesDisputesID.Equals(m.ID)).Select(e => new EquipeViewModel
                    {
                        EquipeID = e.ID,
                        Nom = e.EquipesDisputes.Nom
                    }).ToList(),
                    CompetitionID = m.CompetitionID,
                    CompetitionNom = m.Competition.Nom,
                    CreeLe = m.CreeLe,
                    JeuID = m.Competition.JeuID,
                    JeuNom = m.Competition.Nom,
                    ModifieeLe = m.ModifieeLe,
                    TypeMatcheID = m.TypeMatcheID,
                    TypeMatche = m.TypeMatche.Nom,
                    NbVictoiresEquipeA = m.VictoireEquipeA,
                    NbVictoiresEquipeB = m.VictoireEquipeB,
                    Date = m.DateMatche,
                    Terminer = m.MatcheTeminer
                }).ToListAsync();


            foreach (MatcheViewModel m in Matches)
            {
                var vainqueur = _context.EquipeMatche.Include(em => em.EquipesDisputes).FirstOrDefault(e => e.MatchesDisputesID.Equals(m.ID) && e.Vainqueur);
                if (vainqueur != null)
                {
                    m.VainqueurId = vainqueur.ID;
                    m.VainqueurNom = vainqueur.EquipesDisputes.Nom;
                }
            }

            MatchesTerminer = Matches.All(m => m.Terminer);
        }

        public async Task<IActionResult> OnPostGenererMatchAsync()
        {
            //récupération des matches de la compétition
            var matches = _context.Matches.Where(m => m.CompetitionID.Equals(CompetitionID));
            if (matches.All(m => m.MatcheTeminer) || !matches.Any(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.Finale)))
            {
                //récup selon types de match
                var finale = matches.Where(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.Finale));
                var demiFinales = matches.Where(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.DemiFinale));
                var quartDeFinales = matches.Where(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.QuartDeFinale));
                var huitiemeDeFinale = matches.Where(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.HuitiemeDeFinale));
                var eliminatoires = matches.Where(m => m.TypeMatcheID.Equals(TypeMatchesViewModel.Eliminatoire));

                //equipes encore en compétition (ont gagné le tour précédent)
                var equipesRestantes = await _context.CompetitionEquipe.Include(ce => ce.Equipe).Where(ce => ce.CompetitionID.Equals(CompetitionID) && ce.EncoreEnCompetition).ToListAsync();
                IList<CompetitionEquipe> equipesAssigner = new List<CompetitionEquipe>();

                //Définition du nouveau type de match
                TypeMatche typeMatche = null;
                if (huitiemeDeFinale.Count().Equals(0) && equipesRestantes.Count() > 8 )
                {
                    typeMatche = await _context.TypesDeMatche.FirstOrDefaultAsync(tm => tm.ID.Equals((int)TypeMatchesViewModel.HuitiemeDeFinale));
                }
                else if (quartDeFinales.Count().Equals(0) && equipesRestantes.Count().Equals(8))
                {
                    typeMatche = await _context.TypesDeMatche.FirstOrDefaultAsync(tm => tm.ID.Equals((int)TypeMatchesViewModel.QuartDeFinale));
                }
                else if (demiFinales.Count().Equals(0) && equipesRestantes.Count().Equals(4))
                {
                    typeMatche = await _context.TypesDeMatche.FirstOrDefaultAsync(tm => tm.ID.Equals((int)TypeMatchesViewModel.DemiFinale));
                }
                else if (finale.Count().Equals(0) && equipesRestantes.Count().Equals(2))
                {
                    typeMatche = await _context.TypesDeMatche.FirstOrDefaultAsync(tm => tm.ID.Equals((int)TypeMatchesViewModel.Finale));
                }

                //parcours des équipes pour crééation des matches
                for (int i = 0; i < equipesRestantes.Count(); i++)
                {
                    var competitionEquipeA = equipesRestantes.ElementAt(i);
                    i++;
                    var competitionEquipeB = equipesRestantes.ElementAt(i);

                    var date = DateTime.Now;
                    Matche newMatche = new Matche
                    {
                        CompetitionID = CompetitionID,
                        CreeLe = date,
                        ModifieeLe = date,
                        DateMatche = DateTime.Today.AddDays(1),
                        TypeMatcheID = typeMatche.ID,
                        VictoireEquipeA = 0,
                        VictoireEquipeB = 0
                    };
                    _context.Matches.Add(newMatche);
                    await _context.SaveChangesAsync();

                    //Equipes du matche
                    EquipeMatche equipeA = new EquipeMatche
                    {
                        EquipesDisputesID = competitionEquipeA.EquipeID,
                        MatchesDisputesID = newMatche.ID
                    };

                    EquipeMatche equipeB = new EquipeMatche
                    {
                        EquipesDisputesID = competitionEquipeB.EquipeID,
                        MatchesDisputesID = newMatche.ID
                    };

                    _context.EquipeMatche.Add(equipeA);
                    _context.EquipeMatche.Add(equipeB);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index", new
            {
                id = (int?)CompetitionID
            });
        }
    }
}
