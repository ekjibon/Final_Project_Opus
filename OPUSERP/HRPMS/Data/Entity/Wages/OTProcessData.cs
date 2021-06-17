using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("OTProcessData", Schema = "HR")]
    public class OTProcessData:Base
    {
        public int? wageEmployeeInfoId { get; set; }
        public WagesEmployeeInfo wageEmployeeInfo { get; set; }
        public DateTime? date { get; set; }
        public decimal? OTMin { get; set; }
        public decimal? OTAmount { get; set; }
    }
}
