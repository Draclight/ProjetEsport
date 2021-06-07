using Microsoft.AspNetCore.Identity;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class LicencieViewModel
    {
        //New
        public int ID { get; set; }
        [Display(Name = "Pseudo")]
        public String Pseudo { get; set; }
        [Display(Name = "Prénom")]
        public String Prenom { get; set; }
        [Display(Name = "Nom")]
        public String Nom { get; set; }
        [Display(Name = "Utilisateur")]
        public string UtilisateurID { get; set; }
        [Display(Name = "Utilisateur")]
        public String Utilisateur { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        [Display(Name = "Equipe")]
        public int? EquipeID { get; set; }
        [Display(Name = "Equipe")]
        public string Equipe { get; set; }
        [Display(Name = "Créateur")]
        public bool Createur { get; set; }
        [Display(Name = "Invité")]
        public bool InviteDansEquipe { get; set; }
        [Display(Name = "Rôles")]
        public IList<RoleViewModel> Roles { get; set; }
        [Display(Name = "Invitations")]
        public IList<InvitationViewModel> Invitations { get; set; }
        [Display(Name = "Competitions")]
        public IList<CompetitionViewModel> Competitions { get; set; }

    }
}
