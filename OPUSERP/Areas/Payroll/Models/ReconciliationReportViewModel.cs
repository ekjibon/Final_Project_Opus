using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class ReconciliationReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal? amount { get; set; }        
        public string previousMonth { get; set; }
        public int? previousAmount { get; set; }
        public string currentMonth { get; set; }
        public int? currentAmount { get; set; }
        public int? closingAmount { get; set; }
        public string salaryHeadName { get; set; }
        public string salaryHeadType { get; set; }
        public int? salaryPeriodId { get; set; }
        public int? SortOrder { get; set; }
       
    }
}
