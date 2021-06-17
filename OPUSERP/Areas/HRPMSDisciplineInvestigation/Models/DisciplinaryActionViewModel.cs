using OPUSERP.Areas.HRPMSDisciplineInvestigation.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSDisciplineInvestigation.Models
{
    public class DisciplinaryActionViewModel
    {
        public int? disciplinaryId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string designation { get; set; }

        public int? offenseId { get; set; }

        public string punishmentId { get; set; }

        public string punishmentDate { get; set; }

        public string startFrom { get; set; }

        public string endTo { get; set; }

        public string goNo { get; set; }

        public string goAttachment { get; set; }

        public string remarks { get; set; }

        public DisciplinaryLn fLang { get; set; }

        public IEnumerable<DisciplinaryAction> disciplinaries { get; set; }
        public IEnumerable<Offense> offenses { get; set; }
        public IEnumerable<NaturalPunishment> punishments { get; set; }
    }
}
