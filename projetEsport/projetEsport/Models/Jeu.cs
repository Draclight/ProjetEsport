using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class Jeu
    {
        #region Propriétés
        public int ID { get; set; }
        [Display(Name = "Nom")]
        [Required]
        public String Nom { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        #endregion

        #region Clées
        [Display(Name = "Competitions")]
        public ICollection<CompetitionJeu> CompetitionsJeuSelectionne { get; set; }
        [Display(Name = "Equipes")]
        public ICollection<Equipe> EquipesCreePourJeu { get; set; }
        [Display(Name = "Matches")]
        public ICollection<Matche> MatchesDisputes { get; set; }
        #endregion
    }
}
