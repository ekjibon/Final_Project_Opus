using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class ProcessSalaryRemarksViewModel
    {
        public int? empId { get; set; }   
        public int? salperId { get; set; }
        public string comments { get; set; }
        public decimal? proposedAmount { get; set; }

        public IEnumerable<SalaryProcessLog> salaryProcessLogs { get; set; }
    }
}
