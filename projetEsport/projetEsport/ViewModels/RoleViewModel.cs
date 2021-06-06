using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string LicencieUserId { get; set; }
        public int LicencieId { get; set; }
        [Display(Name = "Nom")]
        public string RoleName { get; set; }
        public bool IsAcquired { get; set; }
    }
}
