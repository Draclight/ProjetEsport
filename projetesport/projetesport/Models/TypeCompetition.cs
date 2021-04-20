﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetesport.Models
{
    public class TypeCompetition
    {
        #region Propriétés
        public int ID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public String Nom { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        #endregion

        #region Clées
        public ICollection<TypeCompetition> Competitions { get; set; }
        #endregion
    }
}
