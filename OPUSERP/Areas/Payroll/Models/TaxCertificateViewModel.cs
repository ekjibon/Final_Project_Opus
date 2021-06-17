using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class TaxCertificateViewModel
    {
        public Company company { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public IEnumerable<TaxableamountViewModel> taxableamountViewModels { get; set; }
        public IEnumerable<TaxableSlabViewModel> taxableSlabViewModels { get; set; }
        public IEnumerable<TaxablePFViewModel> taxablePFViewModels { get; set; }
        public IEnumerable<InvestmentRebateSettings> InvestmentRebateSettings { get; set; }
        public TaxYear taxYear { get; set; }

    }
}
