using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class IncomeTaxChallanViewModel
    {
        public int IncomeTaxchallanId { get; set; }
        public int periodId { get; set; }
   

  


        public string challanNo { get; set; }
        public DateTime challanDate { get; set; }
      
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public IEnumerable<TaxChallan> taxChallans { get; set; }
        public TaxChallan taxChallan { get; set; }
       
    }
}
