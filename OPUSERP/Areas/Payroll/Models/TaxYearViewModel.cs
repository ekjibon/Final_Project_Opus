using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class TaxYearViewModel
    {
        public int editId { get; set; }
        public string taxYearName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public decimal? taxLimit { get; set; }
        public int? lockLabel { get; set; }
        public decimal? allowablePerquisite { get; set; }
        public string assessmentYear { get; set; }

        public IEnumerable<TaxYear> taxYearsList { get; set; }
        public TaxYear taxYear { get; set; }
    }
}
