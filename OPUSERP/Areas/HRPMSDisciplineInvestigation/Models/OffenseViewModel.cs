using OPUSERP.Areas.HRPMSDisciplineInvestigation.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSDisciplineInvestigation.Models
{
    public class OffenseViewModel
    {
        public int? offenseID { get; set; }

        public string offense { get; set; }

        public string description { get; set; }

        public OffenseLn fLang { get; set; }

        public IEnumerable<Offense> offenses { get; set; }
    }
}
