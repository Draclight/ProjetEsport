using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Areas.Admin.ViewModels
{
    public class EditTypeCompetitionViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime Cree { get; set; }
        public DateTime Modif { get; set; }
    }
}
