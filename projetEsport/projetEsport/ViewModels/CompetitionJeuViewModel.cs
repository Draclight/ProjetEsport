using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class CompetitionJeuViewModel
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Competition")]
        public int CompetitionID { get; set; }
        public bool IsInCompetition { get; set; }
    }
}
