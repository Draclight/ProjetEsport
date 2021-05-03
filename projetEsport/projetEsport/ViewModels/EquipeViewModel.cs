using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class EquipeViewModel
    {
        public int ID { get; set; }
        public Equipe Equipe { get; set; }
        public ICollection<Licencie> Membres { get; set; }
        public ICollection<InvitationEquipe> Invitations { get; set; }
    }
}
