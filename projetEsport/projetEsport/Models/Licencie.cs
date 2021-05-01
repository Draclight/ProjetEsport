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
        public String Prenom { get; set; }
        [Display(Name = "Nom")]
        public String Nom { get; set; }
        [Display(Name = "Identifiant")]
        public String IdUtilisateur { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        #endregion

        #region Clées
        [Display(Name = "Numéro de l'équipe")]
        public int? EquipeID { get; set; }
        [Display(Name = "Equipe")]
        public Equipe? Equipe { get; set; }
        [Display(Name = "Mes Compétitions")]
        public IList<Competition> Competitions { get; set; }
        #endregion
    }
}
