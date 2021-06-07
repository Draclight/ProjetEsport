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
        [Display(Name = "Type")]
        public TypeCompetition TypeCompetition { get; set; }
        public int ProprietaireID { get; set; }
        [Display(Name = "Propriétaire")]
        public Licencie Proprietaire { get; set; }
        [Display(Name = "Jeu")]
        public int JeuID{ get; set; }
        [Display(Name = "Jeu")]
        public Jeu Jeu { get; set; }
        [Display(Name = "Equipes")]
        public ICollection<CompetitionEquipe> EquipesDeLaCompetition { get; set; }
        [Display(Name = "Matches")]
        public ICollection<Matche> MatchesDisputes { get; set; }
        #endregion
    }
}
