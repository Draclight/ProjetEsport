﻿using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class CompetitionViewModel
    {
        public Competition Competition { get; set; }
        public int CompetitiionId { get; set; }
        [Display(Name = "Equipes")]
        public int NbEquipes { get; set; }
        [Display(Name = "Jeux")]
        public int NbJeux { get; set; }
        public int JeuID { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Sélectionné")]
        public bool IsInCompetition { get; set; }
    }
}
