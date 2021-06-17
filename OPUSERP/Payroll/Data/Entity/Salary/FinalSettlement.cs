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
    [Table("FinalSettlement", Schema = "Payroll")]
    public class FinalSettlement : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public string seperationStatus { get; set; }
        public int? noticePeriod_Emt { get; set; }
        public int? noticePeriod_Served { get; set; }
        public string lastWorkingDay { get; set; }
        public string salaryDisbUpto { get; set; }
        public string lenghtOfService { get; set; }
        public decimal? leaveBalance { get; set; }
        public decimal? salDue { get; set; }
        public decimal? specDue { get; set; }
        public decimal? carADue { get; set; }
        public decimal? leaveEncashment { get; set; }
        public decimal? fBonus1 { get; set; }
        public decimal? fBonus2 { get; set; }
        public string fB1Remarks { get; set; }
        public string fB2Remarks { get; set; }
        public decimal? perfBonus { get; set; }
        public decimal? lFAAdjust { get; set; }
        public decimal? mobilePhone { get; set; }
        public decimal? mobileData { get; set; }
        public string workingBeyendDays { get; set; }
        public int? daysToYear { get; set; }
        public decimal? hardInstall { get; set; }
        public decimal? advSal { get; set; }
        public decimal? gratuity { get; set; }
        public decimal? lofSvsCG { get; set; }
        public decimal? oDeduction1 { get; set; }
        public decimal? oDeduction2 { get; set; }
        public string cODeduction1 { get; set; }
        public string cODeduction2 { get; set; }
        public decimal? oAddition1 { get; set; }
        public decimal? oAddition2 { get; set; }
        public string cOAddition1 { get; set; }
        public string cOAddition2 { get; set; }
        public decimal? grossSalary { get; set; }
        public decimal? basicSalary { get; set; }
        public decimal? specialAllowance { get; set; }
        public decimal? carAllowance { get; set; }
        public DateTime? appDate { get; set; }
        public int? trStatus { get; set; }
        public decimal? noticePeriodDeducion { get; set; }
        public decimal? incomeTax { get; set; }
        public decimal? walletAmount { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? pFAmount { get; set; }
        public decimal? mobileAllowance { get; set; }
        public decimal? dailyAllowance { get; set; }
        public decimal? internetAllowance { get; set; }
        public decimal? porataBonus { get; set; }
        public decimal? deductedBonus { get; set; }
        public decimal? totalDeduction { get; set; }
        public decimal? totalBenefit { get; set; }
        public decimal? totalOtherBenefit { get; set; }
        public decimal? pFAmountInter { get; set; }
        public decimal? deductedAmount { get; set; }
        public decimal? pFAmountFifty { get; set; }
        public decimal? pFAmountHund { get; set; }
        public decimal? poratedBonusAmount { get; set; }
        public string lStatus { get; set; }
        public decimal? basicDue { get; set; }
        public decimal? houseRentDue { get; set; }
        public decimal? conveyanceDue { get; set; }
        public decimal? medicalDue { get; set; }
        public decimal? pFCompanyInter { get; set; }
        public decimal? carallowanceDeduction { get; set; }
        public decimal? fixedallowanceDeduction { get; set; }
        public decimal? mobileallowanceDeduction { get; set; }
        public decimal? internetallowanceDeduction { get; set; }
        public decimal? pFLoan { get; set; }
        public decimal? pFOthersadjustment { get; set; }
        public decimal? noticePeriodRequired { get; set; }
        public decimal? salaryadjustment { get; set; }
        public decimal? wPPF { get; set; }


    }
}
