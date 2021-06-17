using OPUSERP.Areas.HRPMSDisciplineInvestigation.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSDisciplineInvestigation.Models
{
    public class PunishmentViewModel
    {
        public int? punishmentID { get; set; }

        public string punishment { get; set; }

        public string description { get; set; }

        public PunishmentLn fLang { get; set; }

        public IEnumerable<NaturalPunishment> punishments { get; set; }

        //public IEnumerable<> description { get; set; }
    }
}
