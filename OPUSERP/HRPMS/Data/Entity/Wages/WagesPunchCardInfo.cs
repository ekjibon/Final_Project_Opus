using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("WagesPunchCardInfo", Schema = "HR")]
    public class WagesPunchCardInfo:Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public string punchCardNo { get; set; }
    }
}
