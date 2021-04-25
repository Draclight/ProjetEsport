using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class ClassementCompetition
    {
        #region Propriétés
        public int ID { get; set; }
        public int CompetitionID{ get; set; }
        public Competition Competition { get; set; }
        public int EquipeID { get; set; }
        public Equipe Equipe { get; set; }
        public int Position { get; set; }
        [Display(Name = "Victoires")]
        public int NbVictoire { get; set; }
        [Display(Name = "Défaites")]
        public int NbDefaite { get; set; }
        [Display(Name = "Nulle")]
        public int NbNulle { get; set; }
        #endregion
    }
}
