using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class MatcheViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Matche")]
        public int MatchID { get; set; }
        public IList<EquipeViewModel> EquipesDuMatche { get; set; }
        [Display(Name = "Equipe A")]
        public int EquipeAID { get; set; }
        [Display(Name = "Equipe A")]
        public string EquipeANom { get; set; }
        [Display(Name = "Equipe B")]
        public int EquipeBID { get; set; }
        [Display(Name = "Equipe B")]
        public string EquipeBNom { get; set; }
        [Display(Name = "Résultat A")]
        public int NbVictoiresEquipeA { get; set; }
        [Display(Name = "Résultat B")]
        public int NbVictoiresEquipeB { get; set; }
        [Display(Name = "Type de matche")]
        public int TypeMatcheID { get; set; }
        [Display(Name = "Type de matche")]
        public string TypeMatche { get; set; }
        [Display(Name = "Compétition")]
        public int CompetitionID { get; set; }
        [Display(Name = "Compétition")]
        public string CompetitionNom { get; set; }
        [Display(Name = "Jeu")]
        public int JeuID { get; set; }
        [Display(Name = "Jeu")]
        public string JeuNom { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        [Display(Name = "Propriétaire de la competition")]
        public bool IsProprietaire { get; set; }
    }
}
