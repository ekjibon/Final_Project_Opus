using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class MealChargeViewModel
    {
        public int Id { get; set; }
        public int employeeInfoId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }
        public string periodName { get; set; }
        public string designation { get; set; }
        public string remarks { get; set; }
        public decimal? allowanceAmount { get; set; }
    }
}
