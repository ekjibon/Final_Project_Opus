using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("ProcessEmpSalaryMaster", Schema = "Payroll")]
    public class ProcessEmpSalaryMaster : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public decimal? netPayable { get; set; }
        public decimal? walletPayable { get; set; }
        public decimal? bankPayable { get; set; }
        public decimal? cashPayable { get; set; }
        [MaxLength(200)]
        public string bankName { get; set; }
        [MaxLength(50)]
        public string bankAccountNo { get; set; }
        [MaxLength(50)]
        public string walletNo { get; set; }
        [MaxLength(250)]
        public string division { get; set; }
        [MaxLength(250)]
        public string department { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(50)]
        public string companyBankAccNo { get; set; }
        [MaxLength(150)]
        public string companyBankName { get; set; }
        [MaxLength(150)]
        public string companyBankBranchName { get; set; }
        [MaxLength(550)]
        public string bankAddress { get; set; }
        [MaxLength(150)]
        public string walletType { get; set; }
    }
}
