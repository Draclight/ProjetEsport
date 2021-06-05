using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.ViewModels
{
    public class DashboardViewModel
    {
        [Display(Name = "Licenciés de l'année")]
        public int NbLicenciesAnnee { get; set; }
        [Display(Name = "Licenciés du mois")]
        public int NbLicenciesMois { get; set; }
        [Display(Name = "Licenciés du jour")]
        public int NbLicenciesJour { get; set; }

        public string GetCurrentMonth(int monthNumber)
        {
            string month = string.Empty;

            switch (monthNumber)
            {
                case 1:
                    month = "Janvier";
                    break;
                case 2:
                    month = "Février";
                    break;
                case 3:
                    month = "Mars";
                    break;
                case 4:
                    month = "Avril";
                    break;
                case 5:
                    month = "Mai";
                    break;
                case 6:
                    month = "Juin";
                    break;
                case 7:
                    month = "Juillet";
                    break;
                case 8:
                    month = "Aout";
                    break;
                case 9:
                    month = "Septembre";
                    break;
                case 10:
                    month = "octobre";
                    break;
                case 11:
                    month = "Novembre";
                    break;
                case 12:
                    month = "Décembre";
                    break;
            }

            return month;
        }
    }
}
