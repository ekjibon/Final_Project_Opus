using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class PayrollReportViewModel
    { 
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<PayslipReportViewModel> payslipReportViewModels { get; set; }
        public IEnumerable<PayslipReportViewModel> payslipAdditionViewModels { get; set; }
        public IEnumerable<PayslipReportViewModel> payslipDeductionViewModels { get; set; }
        public IEnumerable<MonthlySalaryReportViewModel> monthlySalaryReportViewModels { get; set; }
        public IEnumerable<BankStatementReportViewModel> bankStatementReportViewModels { get; set; }
        public IEnumerable<GratuityReportViewModel> gratuityReportViewModels { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<UniversalCodaXLTempleteViewModel> universalCodaXLTempleteViewModels { get; set; }
        public IEnumerable<TaxYear> taxYears { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        public IEnumerable<EmpTaxDetailsViewModel> empTaxDetailsViewModels { get; set; }
        public IEnumerable<EmpTaxSlabViewModel> empTaxSlabViewModels { get; set; }
        public IEnumerable<EmpRebatableTaxViewModel> empRebatableTaxViewModels { get; set; }
        public IEnumerable<EmpTaxDeductFinalViewModel> empTaxDeductFinalViewModels { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
