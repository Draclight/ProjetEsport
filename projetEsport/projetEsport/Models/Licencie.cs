using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class Licencie
    {
        #region Propriétés
        public int ID { get; set; }
        [Display(Name = "Pseudo")]
        public String Pseudo { get; set; }
        [Display(Name = "Prénom")]
        public String Penom { get; set; }
        [Display(Name = "Nom")]
        public String Nom { get; set; }
        [Display(Name = "Identifiant utilisateur")]
        public String IdUtilisateur { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        public bool PremierConnexion { get; set; }
        #endregion

        #region Clées
        [Display(Name = "Numéro de l'équipe")]
        public int? EquipeID { get; set; }
        [Display(Name = "Equipe")]
        public Equipe? Equipe { get; set; }
        #endregion
    }
}
