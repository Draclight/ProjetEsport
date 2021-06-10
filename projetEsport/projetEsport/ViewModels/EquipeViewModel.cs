using Microsoft.AspNetCore.Mvc;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class EquipeViewModel
    {
        public int ID { get; set; }
        public int EquipeID { get; set; }
        public String Nom { get; set; }
        [Display(Name = "Jeu")]
        public int JeuID { get; set; }
        [Display(Name = "Jeu")]
        public string JeuNom { get; set; }
        [Display(Name = "Approuvé?")]
        public bool IsApproved { get; set; }
        [Display(Name = "Créé le")]
        public DateTime CreeLe { get; set; }
        [Display(Name = "Modifié le")]
        public DateTime ModifieeLe { get; set; }
        public ICollection<InvitationViewModel> Invitations { get; set; }
        [Display(Name = "Licencié")]
        public IList<LicencieViewModel> LicenciesAInviter { get; set; }
        public IList<LicencieViewModel> Membres { get; set; }
        [Display(Name = "Joue la compétition")]
        public bool IsInCompetition { get; set; }
        [Display(Name = "Joue la compétition")]
        public bool EncoreEnCompetition { get; set; }
        public int CompetitionID { get; set; }
        public bool IsProprietaire { get; set; }
        public bool Vainqueur { get; set; }
    }
}
