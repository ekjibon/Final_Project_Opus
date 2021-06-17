using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class PayslipReportViewModel
    {
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string joiningDatePresentWorkstation { get; set; }
        public string division { get; set; }
        public string walletNo { get; set; }
        public string branchUnitName { get; set; }
        public string contractStartDate { get; set; }
        public string contractEndDate { get; set; }
        public string totalMonth { get; set; }
        public string projectName { get; set; }
        public string projectCode { get; set; }
        public string salaryCode { get; set; }
        public string isContract { get; set; }

        public int? addAmount { get; set; }
        public int? deducAmount { get; set; }
        public int? netPayable { get; set; }
        public decimal? NET { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string bankAccountNo { get; set; }
        public string periodName { get; set; }
        public decimal? daysWorking { get; set; }
        public int? haveArrears { get; set; }
        public decimal? walletPayable { get; set; }
        public decimal? bankPayable { get; set; }
        public decimal? cashPayable { get; set; }
        public string payDate { get; set; }

        public string salaryHeadName { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? amount { get; set; }
        public decimal? grossAmount { get; set; }
        public string walletType { get; set; }
    }
}
