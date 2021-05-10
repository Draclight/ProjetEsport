using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "ID")]
        public string ID { get; set; }
        [Display(Name = "Pseudo")]
        public string UserName { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
    }
}
