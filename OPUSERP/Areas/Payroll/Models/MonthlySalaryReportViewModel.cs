namespace OPUSERP.Areas.Payroll.Models
{
    public class MonthlySalaryReportViewModel
    {
        public int? Id { get; set; }
        public int? salaryPeriodId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string empType { get; set; }
        public string periodName { get; set; }
        public string lockLabel { get; set; }
        public string deptName { get; set; }
        public string branchUnitName { get; set; }
        public string projectName { get; set; }
        public decimal? structureBasic { get; set; }
        public string joiningDatePresentWorkstation { get; set; }
        public string walletNo { get; set; }
        public decimal? walletPayable { get; set; }
        public string bankName { get; set; }
        public string bankAccountNo { get; set; }
        public decimal? bankPayable { get; set; }
        public decimal? cashPayable { get; set; }

        public string Remarks { get; set; }
        public decimal? proposedAmount { get; set; }

        public decimal? Basic { get; set; }
        public decimal? HouseRent { get; set; }
        public decimal? Conveyance { get; set; }
        public decimal? Medical { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? DirectorRemuneration { get; set; }
        public decimal? Remuneration { get; set; }
        public decimal? HouseMaintenance { get; set; }
        public decimal? CarAllowance { get; set; }
        public decimal? SpecialAllowance { get; set; }
        public decimal? LFA { get; set; }
        public decimal? ELEnCash { get; set; }
        public decimal? TaxPaidCompany { get; set; }
        public decimal? SalaryAdjustment { get; set; }
        public decimal? Advance { get; set; }
        public decimal? FestivalBonus { get; set; }

        public decimal? OverTime { get; set; }
        public decimal? PerformanceBonus { get; set; }
        public decimal? OtherAddition { get; set; }
        public decimal? PFEmployer { get; set; }
        public decimal? DailyAllowance { get; set; }
        public decimal? MobileBillAllowance { get; set; }
        public decimal? InternetAllowance { get; set; }
        public decimal? LeaveEncashment { get; set; }

        public decimal? PersonalPay { get; set; }
        public decimal? UpKeepAllowance { get; set; }
        public decimal? UtilityAllowance { get; set; }
        public decimal? Annuity { get; set; }
        public decimal? Wages { get; set; }

        public decimal? PFOwn { get; set; }
        public decimal? ExceedCellBill { get; set; }
        public decimal? InstallmentDeduction { get; set; }
        public decimal? AdvanceDeduction { get; set; }
        public decimal? ExcessFuelBill { get; set; }
        public decimal? FamilyPackage { get; set; }
        public decimal? TransportDeduction { get; set; }
        public decimal? AbsentDeduction { get; set; }
        public decimal? MealCharge { get; set; }
        public decimal? OtherDeduction { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? HouseLoan { get; set; }
        public decimal? VehicleTax { get; set; }
        public decimal? LastYearTaxAdjustment { get; set; }
        public decimal? ThisYearAdjustment { get; set; }
        public decimal? DPSDeduction { get; set; }

        public decimal? hardshipAllowance { get; set; }

        public decimal? PFOC { get; set; }
        public decimal? GROSSFour { get; set; }
        public decimal? GROSS { get; set; }
        public decimal? GROSSWPF { get; set; }
        public decimal? TOTALALLOWANCE { get; set; }
        public decimal? TOTALDEDUCT { get; set; }
        public decimal? NET { get; set; }
    }
}
