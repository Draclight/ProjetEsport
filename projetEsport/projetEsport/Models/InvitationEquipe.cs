using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class InvitationEquipe
    {
        public int ID { get; set; }
        public int EquipeID { get; set; }
        public Equipe Equipe { get; set; }
        public int LicencieID { get; set; }
        public Licencie Licencie { get; set; }
        [Display(Name = "Accepté?")]
        public bool IsAccepted { get; set; }
        [Display(Name = "Reçu")]
        public DateTime DateEnvoi { get; set; }
        [Display(Name = "Accepter")]
        public DateTime DateAccepter { get; set; }
    }
}
