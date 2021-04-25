using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class Competition
    {
        #region Propriétés
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
        #endregion

        #region Clées
        public int TypeCompetitionID { get; set; }
        public TypeCompetition TypeCompetition { get; set; }
        public ICollection<Equipe> Equipes { get; set; }
        public ICollection<Jeu> Jeux { get; set; }
        #endregion
    }
}
