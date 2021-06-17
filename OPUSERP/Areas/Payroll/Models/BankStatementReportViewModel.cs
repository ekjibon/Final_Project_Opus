using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class BankStatementReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string periodName { get; set; }
        public string branchUnitName { get; set; }
        public string projectName { get; set; }
        public string bankName { get; set; }
        public string lockLabel { get; set; }
        public string bankAccountNo { get; set; }
        public string companyBankAccNo { get; set; }
        public string companyBankName { get; set; }
        public string companyBankBranchName { get; set; }
        public string address { get; set; }
        public string walletNo { get; set; }
        public string walletType { get; set; }

        public decimal? netPayable { get; set; }
        public decimal? bankPayable { get; set; }
        public decimal? walletPayable { get; set; }
        public decimal? cashPayable { get; set; }
    }
}
