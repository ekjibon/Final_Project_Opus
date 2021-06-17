using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("IRCSignatory", Schema = "CRO")]
    public class IRCSignatory:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? serialNo { get; set; }
    }
}
