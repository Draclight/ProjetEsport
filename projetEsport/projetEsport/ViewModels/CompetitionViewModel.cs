using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class CompetitionViewModel
    {
        public int ID { get; set; }
        [Required]
        public String Nom { get; set; }
        [Display(Name = "Début")]
        [Required]
        public DateTime DateDebut { get; set; }
        [Display(Name = "Fin")]
        public DateTime? DateFin { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        [Display(Name = "Equipes")]
        public int NbEquipes { get; set; }
        public int JeuID { get; set; }
        public CompetitionJeuViewModel Jeu { get; set; }
        [Display(Name = "Proprietaire")]
        public int ProprietaireID { get; set; }
        [Display(Name = "Proprietaire")]
        public string Proprietaire { get; set; }
        [Display(Name = "Type de Competition")]
        public int TypeCompetitionID { get; set; }
        [Display(Name = "Type de Competition")]
        public string TypeCompetition { get; set; }
        [Display(Name = "Jeux de la compétion")]
        public IList<CompetitionJeuViewModel> JeuxDeLaCompetition { get; set; }
        [Display(Name = "Equipes de la compétiion")]
        public IList<EquipeViewModel> EquipesDeLaCompetition { get; set; }
        public bool IsPropriétaire { get; set; }
    }
}
