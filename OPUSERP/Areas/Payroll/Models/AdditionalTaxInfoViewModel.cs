using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class AdditionalTaxInfoViewModel
    {
        public int AdditionalTaxInfoId { get; set; }
        public int employeeInfoId { get; set; }

        public int taxYearId { get; set; }


        public int salaryHeadId { get; set; }

        public string exemptionRule { get; set; }
        public decimal exemptionAmount { get; set; }

   
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public SalaryHead salaryHead { get; set; }
        public IEnumerable<TaxYear> taxYears { get; set; }
        public TaxYear taxYear { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public IEnumerable<AdditionalTaxInfo> additionalTaxInfos { get; set; }
        public AdditionalTaxInfo additionalTax { get; set; }
       
       
    }
}
