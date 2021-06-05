using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class CompetitionEquipe
    {
        public int ID { get; set; }
        public int EquipeID { get; set; }
        public Equipe Equipe { get; set; }
        public int CompetitionID { get; set; }
        public Competition Competition { get; set; }
    }
}
