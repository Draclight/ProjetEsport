using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class EquipeMatche
    {
        public int ID { get; set; }
        public int EquipesDisputesID { get; set; }
        public Equipe EquipesDisputes { get; set; }
        public int MatchesDisputesID { get; set; }
        public Matche MatchesDisputes { get; set; }
    }
}
