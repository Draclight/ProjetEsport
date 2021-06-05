using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Models
{
    public class CompetitionJeu
    {
        public int ID { get; set; }
        public int CompetitionsJeuSelectionneID { get; set; }
        public Competition CompetitionsJeuSelectionne { get; set; }
        public int JeuxDeLaCompetitionID { get; set; }
        public Jeu JeuxDeLaCompetition { get; set; }
    }
}
