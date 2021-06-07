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
        [Display(Name = "Pseudo")]
        public string PseudoLicencie { get; set; }
        [Display(Name = "Equipe")]
        public int EquipeId { get; set; }
        [Display(Name = "Equipe")]
        public string NomEquipe { get; set; }
        [Display(Name = "Licencie invité")]
        public IList<MembreViewModel> Membres { get; set; }
        [Display(Name = "Reçu")]
        public DateTime DateEnvoi { get; set; }
        [Display(Name = "Accepter")]
        public DateTime DateAccepter { get; set; }
        [Display(Name = "Accepter")]
        public bool Accepter { get; set; }
        [Display(Name = "Envoyer")]
        public bool Envoyer { get; set; }
    }

    public class MembreViewModel
    {
        public string Pseudo { get; set; }
        public bool IsAccepter { get; set; }
    }
}
