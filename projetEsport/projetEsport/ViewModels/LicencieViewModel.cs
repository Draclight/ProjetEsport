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
        public Licencie licencie { get; set; }
        [Display(Name = "Rôles")]
        public IList<RoleViewModel> Roles { get; set; }
        [Display(Name = "Invité")]
        public bool InviteDansEquipe { get; set; }
    }
}
