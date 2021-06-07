using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class Equipe
    {
        #region Propriétés
        public int ID { get; set; }
        [Required]
        public String Nom { get; set; }
        [Display(Name = "Approuvé?")]
        public bool IsApproved { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        #endregion

        #region Clées
        public ICollection<Licencie> Membres { get; set; }
        ICollection<CompetitionEquipe> CompetitionsParticipe { get; set; }
        ICollection<InvitationEquipe> InvitationsEquipe { get; set; }
        public int JeuID { get; set; }
        public Jeu Jeu { get; set; }
        [Display(Name = "Matches")]
        public ICollection<EquipeMatche> MatchesDisputes { get; set; }
        #endregion
    }
}
