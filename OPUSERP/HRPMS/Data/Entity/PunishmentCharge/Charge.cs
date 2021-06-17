using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.PunishmentCharge
{
    [Table("Charge", Schema = "HR")]
    public class Charge:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public string letterNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime chargeDate { get; set; }

        public string chargeFor { get; set; }

        public string remarks { get; set; }
    }
}
