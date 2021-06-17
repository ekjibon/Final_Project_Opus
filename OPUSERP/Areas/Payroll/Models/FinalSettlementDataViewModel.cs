using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class FinalSettlementDataViewModel
    {
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string AccNo { get; set; }
        public string WalletNo { get; set; }
        public string mobileNumberOffice { get; set; }
        public string Division { get; set; }
        public string Designation { get; set; }
        public string JoiningDate { get; set; }
        public string dateOfPermanent { get; set; }
        public string EmploymentStatus { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal? DailyAllowance { get; set; }
        public decimal? MobileAllowance { get; set; }
        public decimal? InternetAllowance { get; set; }
        public string ResignDate { get; set; }
        public string LastWorkingDate { get; set; }
        public string LastDayofSalPaid { get; set; }
        public decimal? BasicSal { get; set; }
        public decimal? Gross { get; set; }
        public decimal? BasicSalPerDay { get; set; }
        public decimal? GrossSalaryPerDay { get; set; }
        public int? NPServed { get; set; }
        public int? NPNotServed { get; set; }
        public string LOS { get; set; }
        public string LOC { get; set; }
        public decimal? LofSvsCG { get; set; }
        public int? SalDueDays { get; set; }
        public decimal? AdvanceDueOthers { get; set; }
        public decimal? AdvanceDueHardwares { get; set; }
        public string ResignStatus { get; set; }
        public decimal? Gratuity { get; set; }
        public decimal? PFAmount { get; set; }
        public decimal? PFCAmount { get; set; }
        public decimal? SalaryDue { get; set; }
        public decimal? PoratedBonusAmount { get; set; }
        public decimal? PoratedBonusAdd { get; set; }
        public decimal? BasicDue { get; set; }
        public decimal? HouseRentDue { get; set; }
        public decimal? ConveyanceDue { get; set; }
        public decimal? FixedAllowST { get; set; }
        public decimal? MobileAllowanceST { get; set; }
        public decimal? InternetAllowanceST { get; set; }
    }
}
