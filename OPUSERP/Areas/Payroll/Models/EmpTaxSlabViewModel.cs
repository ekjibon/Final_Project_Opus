using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmpTaxSlabViewModel
    {
        public string slabText { get; set; }
        public decimal? taxRate { get; set; }
        public decimal? slabAmount { get; set; }
        public decimal? taxAmount { get; set; }
        
       
    }
}
