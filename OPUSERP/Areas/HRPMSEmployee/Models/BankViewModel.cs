using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class BankViewModel
    {
        public int bankInfoId { get; set; }
        public string employeeID { get; set; }

        public int? fnCodeId { get; set; }
        public int? bankId { get; set; }
        public string fnCode { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accountNumber { get; set; }
        public int? walletTypeId { get; set; }
        public string walletNumber { get; set; }

        public string ibus { get; set; }
        public string employeeNameCode { get; set; }        

        public BankLn fLang { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<BankInfo> bankInfos { get; set; }
        public IEnumerable<WagesBankInfo> wagesBankInfos { get; set; }
        public IEnumerable<FinanceCode> financeCodes { get; set; }
        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<WalletType> walletTypes { get; set; }
    }
}
