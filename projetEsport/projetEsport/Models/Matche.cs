﻿using System;
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
        public int JeuID { get; set; }
        public Jeu Jeu { get; set; }
        [Display(Name = "Equipes")]
        public ICollection<EquipeMatche> EquipesDisputes { get; set; }
        public int VictoireAEquipe1 { get; set; }
        public int VictoireAEquipe2 { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
    }
}
