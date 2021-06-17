using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmpTaxDeductFinalViewModel
    {
       
        public decimal? taxDeducted { get; set; }
      
        public string challanNotes { get; set; }
        public string assessmentYear { get; set; }
        public string taxYearName { get; set; }



    }
}
