﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class CompetitionJeu
    {
        public int ID { get; set; }
        public int CompetitionID { get; set; }
        public Competition Competition { get; set; }
        public int JeuID { get; set; }
        public Jeu Jeu { get; set; }
    }
}