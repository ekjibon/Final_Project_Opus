using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.HRPMSPunishment.Models
{
    public class ChargeViewModel
    {
        public int? ChargeID { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string letterNo { get; set; }

        public DateTime chargeDate { get; set; }

        public string chargeFor { get; set; }

        public string remarks { get; set; }

        public int? SLNo { get; set; }

        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Charge> charges { get; set; }
    }
}
