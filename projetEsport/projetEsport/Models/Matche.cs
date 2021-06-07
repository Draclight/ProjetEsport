using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class Matche
    {
        public int ID { get; set; }
        public int TypeMatcheID { get; set; }
        [Display(Name = "Type")]
        public TypeMatche TypeMatche { get; set; }
        public int CompetitionID { get; set; }
        public Competition Competition { get; set; }
        [Display(Name = "Equipes")]
        public ICollection<EquipeMatche> EquipesDisputes { get; set; }
        [Display(Name = "Victoire équipe A")]
        public int VictoireEquipeA { get; set; }
        [Display(Name = "Victoire équipe B")]
        public int VictoireEquipeB { get; set; }
        [Display(Name = "Date du matche")]
        public DateTime DateMatche { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
    }
}
