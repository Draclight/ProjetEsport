using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class InvitationViewModel
    {
        public int ID { get; set; }
        public int LicencieID { get; set; }
        [Display(Name = "Equipe")]
        public string NomEquipe { get; set; }
        public IList<string> Membres { get; set; }
        [Display(Name = "Reçu")]
        public DateTime Quand { get; set; }
        public bool Accepter { get; set; }
    }
}
