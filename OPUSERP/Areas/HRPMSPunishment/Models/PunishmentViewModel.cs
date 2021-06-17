using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSPunishment.Models
{
    public class PunishmentViewModel
    {
        public int? PunishmentID { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string letterNo { get; set; }

        public DateTime punishmentDate { get; set; }

        public string punishmentFor { get; set; }

        public string remarks { get; set; }

        public int? SLNo { get; set; }

        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<HRPMS.Data.Entity.PunishmentCharge.Punishment> punishments { get; set; }
    }
}
