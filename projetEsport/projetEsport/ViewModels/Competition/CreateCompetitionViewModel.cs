using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class CreateCompetitionViewModel
    {
        public int JeuID { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Sélectionné")] 
        public bool IsInCompetition { get; set; }
    }
}
