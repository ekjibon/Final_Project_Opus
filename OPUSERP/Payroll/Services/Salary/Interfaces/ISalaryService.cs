using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.Salary.Interfaces
{
    public interface ISalaryService
    {

        #region Gratuity process
        Task<bool> SaveSendEmailLogStatus(SendEmailLogStatus salaryYear);
        Task<IEnumerable<SendEmailLogStatus>> GetSendEmailLogStatus();

        Task<bool> SaveGratiutyProcessData(GratiutyProcessData salaryYear);
        Task<IEnumerable<GratiutyProcessData>> GetAllGratiutyProcessData();
        Task<IEnumerable<GratuityReportViewModel>> GetAllGratuityReportViewModel();
        List<DateTime?> GetAllGratiutyProcessDataDates();
        Task<IEnumerable<GratuityReportViewModel>> GetAllGratuityReportViewModelByDate(DateTime date);
        #endregion

        #region Salary Year

        Task<bool> SaveSalaryYear(SalaryYear salaryYear);
        Task<IEnumerable<SalaryYear>> GetAllSalaryYear();
        Task<SalaryYear> GetSalaryYearById(int id);
        Task<bool> DeleteSalaryYearById(int id);
        #endregion

        #region Employee Tax
        Task<bool> SaveEmployeesTax(EmployeesTax employeesTax);
        Task<IEnumerable<EmployeesTax>> GetAllEmployeesTax();
        Task<EmployeesTax> GetEmployeesTaxById(int id);
        Task<bool> DeleteEmployeeTaxById(int id);
        Task<bool> UpdateEmployeesStatus(int? id, int taxyearid);
        #endregion

        #region Salary Grade
        Task<bool> SaveSalaryGrade(SalaryGrade orga);
        Task<IEnumerable<SalaryGrade>> GetAllSalaryGrade();
        Task<SalaryGrade> GetSalaryGradeById(int id);
        Task<bool> DeleteSalaryGradeById(int id);
        #endregion

        #region Tax Year
        Task<bool> SaveTaxYear(TaxYear taxYear);
        Task<IEnumerable<TaxYear>> GetAllTaxYear();
        Task<bool> UpdateTaxInSalaryall(int PeriodId);
        Task<bool> UpdateTaxInSalaryallstruc();
        Task<bool> UpdateTaxInSalary(int PeriodId, int employeeId, int Id);
        Task<bool> UpdateTaxInstruc(int employeeId, int Id);
        Task<TaxYear> TaxYearbyId(int id);
        #endregion

        #region Tax slab Type
        Task<bool> SaveTaxSlab(SlabType slabType);
        Task<IEnumerable<SlabType>> GetAllSlabType();
        #endregion

        #region Rebate slab Type
        Task<bool> SaveRebateSlab(RebateSlabType rebateSlabType);
        Task<IEnumerable<RebateSlabType>> GetAllRebateSlabType();
        #endregion

        #region Rebate Setting
        Task<bool> SaveRebateSetting(InvestmentRebateSettings investmentRebateSettings);
        Task<IEnumerable<InvestmentRebateSettings>> GetAllRebateSetting();
        Task<IEnumerable<InvestmentRebateSettings>> GetAllRebateSettingbytaxyearid(int Id);
        #endregion

        #region Salary Head
        Task<bool> SaveSalaryHead(SalaryHead salaryHead);
        Task<IEnumerable<SalaryHead>> GetAllSalaryHead();
        Task<IEnumerable<SalaryHead>> GetAllSalaryHeadByFilter(string filter);
        Task<bool> DeleteSalaryHeadById(int id);
        #endregion

        #region Salary Type
        Task<bool> SaveSalaryType(SalaryType salaryType);
        Task<IEnumerable<SalaryType>> GetAllSalaryType();
        #endregion

        #region Bonus Type
        Task<bool> SaveBonusType(BonusType bonusType);
        Task<IEnumerable<BonusType>> GetAllBonusType();
        #endregion

        #region Bonus Category

        Task<bool> SaveBonusCategory(BonousCategory bonusCategory);
        Task<IEnumerable<BonousCategory>> GetAllBonusCategory();
        Task<bool> DeleteBonusCategoryById(int id);

        #endregion

        #region Bonus Sub Category

        Task<bool> SaveBonusSubCategory(BonousSubCategory bonusSubCategory);
        Task<IEnumerable<BonousSubCategory>> GetAllBonusSubCategory();
        Task<IEnumerable<BonousSubCategory>> GetBonusSubCategoryByMasterId(int masterId);
        Task<bool> DeleteBonusSubCategoryById(int id);

        #endregion

        #region Bonous Structure

        Task<int> SaveBonousStructure(BonousStructure bonousStructure);
        Task<IEnumerable<BonousStructure>> GetAllBonousStructure();
        Task<bool> DeleteBonousStructureById(int id);

        #endregion

        #region Employees Bonous Structure

        Task<int> SaveEmployeesBonusStructure(EmployeesBonusStructure bonousStructure);
        Task<IEnumerable<EmployeesBonusStructure>> GetEmployeesBonusStructureByBonusId(int bonusId);
        Task<bool> DeleteEmployeesBonusStructureBybonusId(int id);

        #endregion

        #region Salary Calulation Type
        Task<bool> SaveSalaryCalulationType(SalaryCalulationType salaryCalulationType);
        Task<IEnumerable<SalaryCalulationType>> GetAllSalaryCalulationType();
        #endregion

        #region Wallet Type
        Task<bool> SaveWalletType(WalletType walletType);
        Task<IEnumerable<WalletType>> GetAllWalletType();
        Task<bool> DeleteWalletTypeById(int id);
        #endregion

        #region EmployeesCashSetup
        Task<int> SaveEmployeesCashSetup(EmployeesCashSetup employeesCashSetup);
        Task<IEnumerable<EmployeesCashSetup>> GetAllEmployeesCashSetup();
        Task<IEnumerable<EmployeesCashSetup>> GetEmployeesCashSetupByEmployeeId(int empId);
        Task<bool> DeleteEmployeesCashSetupById(int id);
        #endregion

        #region Salary Period
        Task<bool> SaveSalaryPeriod(SalaryPeriod salaryPeriod);
        Task<IEnumerable<SalaryPeriod>> GetAllSalaryPeriod();
        Task<IEnumerable<SalaryPeriod>> GetSalaryPeriodById(int PeriodId);
        Task<IEnumerable<SalaryPeriod>> GetDuplicateSalaryPeriodById(int PeriodId, int yearId, int typeId, string month);
        Task<bool> EditSalaryPeriodForlockLabel(int Id, int lockLabel);
        Task<bool> SetSalaryPeriodLock(int Id, int lockLabel, string lockBy);
        Task<SalaryPeriod> GetSalaryPeriodNameById(int periodId);
        Task<SalaryPeriod> GetSalaryPeriodmax();
        Task<bool> DeleteSalaryPeriodById(int id);
        #endregion

        #region Salary Slab
        Task<bool> SaveSalarySlab(SalarySlab salarySlab);
        Task<IEnumerable<SalarySlab>> GetAllSalarySlab();
        Task<IEnumerable<SalarySlab>> GetSalarySlabBysalaryGradeId(int salaryGradeId);
        Task<SalarySlab> GetSalarySlabById(int Id);
        #endregion

        #region Salary Grade Percent
        Task<bool> SaveSalaryGradePercent(SalaryGradePercent salaryGradePercent);
        Task<IEnumerable<SalaryGradePercent>> GetAllSalaryGradePercent();
        Task<SalaryGradePercent> GetSalaryGradePercentBysalaryHeadId(int salaryGradeId, int salaryHeadId);
        #endregion

        #region Employees Salary Structure
        Task<bool> SaveEmployeesSalaryStructure(EmployeesSalaryStructure employeesSalaryStructure);
        Task<IEnumerable<EmployeesSalaryStructure>> GetAllEmployeesSalaryStructure();
        Task<IEnumerable<EmployeesSalaryStructure>> GetEmployeesSalaryStructureByEmpId(int empId);
        Task<EmployeesSalaryStructure> GetEmpStructureByEmpId(int empId);
        Task<bool> DeleteEmployeesSalaryStructureByempId(int empId);
        Task<bool> EditEmployeesSalaryStructure(int Id, decimal amount, string status);
        Task<IEnumerable<FsSalaryStructureViewModel>> GetFsStructure(int empId, int day);
        #endregion

        #region Salary Structure History

        Task<IEnumerable<SalaryProcessDataViewModel>> SaveStructureHistory(int employeeInfoId, string changeBy);
        Task<IEnumerable<SalaryProcessDataViewModel>> UpdateStructureHistory(int structureId, string changeBy);
        Task<IEnumerable<System.Object>> GetStructureHistoryByEmpId(int empId);

        #endregion

        #region Wages Salary Structure
        Task<bool> SaveWagesSalaryStructure(WagesSalaryStructure wagesSalaryStructure);
        Task<IEnumerable<WagesSalaryStructure>> GetAllWagesSalaryStructure();
        Task<IEnumerable<WagesSalaryStructure>> GetWagesSalaryStructureByEmpId(int empId);
        Task<bool> DeleteWagesSalaryStructureByempId(int empId);
        Task<bool> EditWagesSalaryStructure(int Id, decimal amount, string status);
        Task<bool> DeleteWagesSalaryStructureById(int Id);
        #endregion

        #region Process Emp Salary Structure
        Task<bool> SaveProcessEmpSalaryStructure(ProcessEmpSalaryStructure processEmpSalaryStructure);
        Task<IEnumerable<ProcessEmpSalaryStructure>> GetProcessEmpSalaryStructureByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<ProcessEmpSalaryStructure>> GetProcessEmpSalaryStructureBysalaryPeriodId(int salaryPeriodId);
        Task<bool> DeleteProcessEmpSalaryStructureByempId(int empId, int salaryPeriodId);
        Task<bool> DeleteProcessEmpSalaryStructureBysalaryPeriodId(int salaryPeriodId);
        Task<decimal> GetNetPayableByemployeeInfoId(int employeeInfoId, int salaryPeriodId);
        Task<bool> EditProcessEmpSalaryStructureForLeaveWithoutPay(int employeeInfoId, int salaryPeriodId, int noOfDays);
        Task<bool> EditProcessEmpSalaryStructureForAdvanceAdjustment(int employeeInfoId, int salaryPeriodId, int salaryHeadId, decimal amount);
        Task<bool> EditProcessEmpSalaryStructureForEmployeeArrear(int employeeInfoId, int salaryPeriodId, decimal totalamount, decimal amount);
        #endregion

        #region Process Emp Salary Master
        Task<bool> SaveProcessEmpSalaryMaster(ProcessEmpSalaryMaster processEmpSalaryMaster);
        Task<IEnumerable<ProcessEmpSalaryMaster>> GetProcessEmpSalaryMasterByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<ProcessEmpSalaryMaster>> GetProcessEmpSalaryMasterBysalaryPeriodId(int salaryPeriodId);
        Task<bool> DeleteProcessEmpSalaryMasterByempId(int empId, int salaryPeriodId);
        Task<bool> DeleteProcessEmpSalaryMasterBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<SalaryProcessDataViewModel>> ProcessEmpSalaryMasterBySp(int? salaryPeriodId, int? employeeInfoId);
        #endregion

        #region Salary Process Log
        Task<bool> SaveSalaryProcessLog(SalaryProcessLog salaryProcessLog);
        Task<IEnumerable<SalaryProcessLog>> GetAllSalaryProcessLog();
        #endregion

        #region Salary Status Log
        Task<int> SaveSalaryStatusLog(SalaryStatusLog salaryStatusLog);
        Task<IEnumerable<SalaryStatusLog>> GetSalaryStatusLogByPeriodId(int periodId);
        #endregion

        #region Process Salary Remarks

        Task<int> SaveProcessSalaryRemarks(ProcessSalaryRemarks processSalaryRemarks);
        Task<bool> DeleteProcessSalaryRemarks(int? empId, int? periodId);
        #endregion

        #region Leave Without Pay
        Task<bool> SaveLeaveWithoutPay(LeaveWithoutPay leaveWithoutPay);
        Task<IEnumerable<LeaveWithoutPay>> GetAllLeaveWithoutPay();
        Task<LeaveWithoutPay> GetLeaveWithoutPayById(int id);
        Task<bool> DeleteLeaveWithoutPayById(int id);
        Task<IEnumerable<LeaveWithoutPay>> GetLeaveWithoutPayBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<LeaveWithoutPay>> GetLeaveWithoutPayByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        
        #endregion

        #region Employee Arrear
        Task<bool> SaveEmployeeArrear(EmployeeArrear employeeArrear);
        Task<IEnumerable<EmployeeArrear>> GetAllEmployeeArrear();
        Task<IEnumerable<EmployeeArrear>> GetEmployeeArrearBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<EmployeeArrear>> GetEmployeeArrearByEmpAndPeriodId(int empId,int periodId);
        Task<EmployeeArrear> GetEmployeeArrearById(int id);
        Task<bool> DeleteEmployeeArrearByEmpAndPeriodId(int empId, int periodId);        
        Task<IEnumerable<EmployeeArrear>> EmployeeArrearCalculationByEmpId(int empId, int periodId, decimal? totalAmount, decimal? bonusAmount);
        #endregion

        #region Advance Adjustment
        Task<int> SaveAdvanceAdjustment(AdvanceAdjustment advanceAdjustment);
        Task<IEnumerable<AdvanceAdjustment>> GetAllAdvanceAdjustment();
        Task<AdvanceAdjustment> GetAdvanceAdjustmentById(int id);
        Task<bool> DeleteAdvanceAdjustmentById(int id);
        Task<IEnumerable<AdvanceAdjustment>> GetAdvanceAdjustmentBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<AdvanceAdjustment>> GetAdvanceAdjustmentByemployeeInfoId(int salaryPeriodId, int employeeInfoId);

        Task<bool> SaveAdvanceAdjustmentDetail(AdvanceAdjustmentDetail advanceAdjustment);
        Task<IEnumerable<AdvanceAdjustmentDetail>> GetAllAdvanceAdjustmentDetailByMasterId(int id);
        void UpdateAdvanceAdjustmentDetail(int docId, decimal? amount);

        #endregion

        #region Loan Schedule

        Task<int> SaveLoanScheduleMaster(LoanScheduleMaster advanceAdjustment);
        Task<bool> SaveLoanScheduleDetail(LoanScheduleDetail advanceAdjustment);
        Task<IEnumerable<LoanScheduleMaster>> GetAllLoanScheduleMaster();
        Task<IEnumerable<LoanScheduleMaster>> GetAllLoanScheduleMasterByEmpId(int? empId);
        Task<IEnumerable<LoanScheduleDetail>> GetAllLoanScheduleDetailByMasterId(int id);
        Task<bool> DeleteLoanScheduleDetailById(int id);
        void UpdateLoanScheduleDetail(int docId, decimal? amount);
        Task<bool> UpdateLoanScheduleMasterApproval(int Id, int status);
        Task<IEnumerable<LoanScheduleReportViewModel>> GetLoanReportById(int masterId);

        #endregion

        #region Loan Policy

        Task<bool> SaveLoanPolicy(LoanPolicy loanPolicy);
        Task<IEnumerable<LoanPolicy>> GetAllLoanPolicy();
        Task<LoanPolicy> GetLoanPolicyById(int id);
        Task<bool> DeleteLoanPolicyById(int id);

        #endregion

        #region Loan Route

        Task<bool> SaveLoanRoute(LoanRoute loanRoute);
        Task<IEnumerable<LoanRoute>> GetLoanRouteByEmpId(int empId);
        Task<LoanRoute> GetLoanRouteById(int id);
        Task<bool> UpdateLoanRoute(int Id, int Type);
        Task<LoanRoute> GetLoanRouteByRouteOrder(int id, int order);

        #endregion

        #region Monthly Allowance
        Task<bool> SaveMonthlyAllowance(MonthlyAllowance monthlyAllowance);
        Task<IEnumerable<MonthlyAllowance>> GetAllMonthlyAllowance();
        Task<MonthlyAllowance> GetMonthlyAllowanceById(int id);
        Task<bool> DeleteMonthlyAllowanceById(int id);
        Task<IEnumerable<MonthlyAllowance>> GetMonthlyAllowanceBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<MonthlyAllowance>> GetMonthlyAllowanceByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<MealChargeViewModel>> GetMealChargeByPeriod(int salaryPeriodId);
        #endregion

        #region Attendance Summary

        Task<int> SaveAttendanceSummary(EmpAttendanceSummary monthlyAllowance);
        Task<IEnumerable<EmpAttendanceSummary>> GetAttendanceSummary();
        Task<EmpAttendanceSummary> GetAttendanceSummaryById(int id);
        Task<bool> DeleteAttendanceSummaryById(int id);
        Task<IEnumerable<EmpAttendanceSummary>> GetAttendanceSummaryBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<EmpAttendanceSummary>> GetDuplicateAttendanceSummary(int id, int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<Areas.HRPMSAttendence.Models.AttendanceSummaryViewModel>> GetAttendanceSummaryByPeriod(int? id, int? salaryPeriodId);
       
       
        #endregion

        #region Lfa Info
        Task<bool> SaveLfaInfo(LfaInfo lfaInfo);
        Task<IEnumerable<LfaInfo>> GetAllLfaInfo();
        Task<LfaInfo> GetLfaInfoByEmpId(int empId);
        Task<bool> DeleteLfaInfoById(int id);
        Task<ProcessEmpSalaryStructure> GetLastYearBasic(int empId, int periodId);
        #endregion

        #region Payroll Report
        Task<IEnumerable<PayslipReportViewModel>> GetPaySlipByEmpId(int employeeInfoId, int salaryPeriodId);
        Task<IEnumerable<PayslipReportViewModel>> GetPaySlipAdditionByEmpId(int employeeInfoId, int salaryPeriodId);
        Task<IEnumerable<PayslipReportViewModel>> GetPaySlipDeductionByEmpId(int employeeInfoId, int salaryPeriodId);
        Task<IEnumerable<MonthlySalaryReportViewModel>> GetMonthlySalaryReportByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId);
        Task<IEnumerable<BankStatementReportViewModel>> GetBankStatementByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId);
        Task<IEnumerable<GratuityReportViewModel>> GetGratuityReport();
        Task<IEnumerable<ReconciliationReportViewModel>> GetReconcilationStatement(int? empId, int? salaryPeriodId, int? presalaryPeriodId, int? typeId);
        Task<IEnumerable<EmpTaxDetailsViewModel>> GetEmpTaxDetails(int employeeInfoId, int taxYearId);
        Task<IEnumerable<EmpTaxSlabViewModel>> GetEmpTaxSlab(int employeeInfoId, int taxYearId);
        Task<IEnumerable<EmpRebatableTaxViewModel>> GetEmpRebatableTax(int employeeInfoId, int taxYearId);
        Task<IEnumerable<EmpTaxDeductFinalViewModel>> GetEmpTaxDeductFinal(int employeeInfoId, int taxYearId);
        Task<IEnumerable<TaxableamountViewModel>> TaxableamountViewModels(int employeeInfoId, int taxYearId);
        Task<IEnumerable<TaxableSlabViewModel>> TaxableslabViewModels(int employeeInfoId, int taxYearId);
        Task<IEnumerable<TaxablePFViewModel>> TaxablePFViewModels(int employeeInfoId, int taxYearId);
        Task<IEnumerable<EmployeesTax>> TaxCalculateforall(int taxYearId);

        #endregion

        #region Advance Payment
        Task<bool> SaveAdvancePayment(AdvancePayment advancePayment);
        Task<IEnumerable<AdvancePayment>> GetAllAdvancePayment();
        Task<IEnumerable<AdvancePayment>> GetAdvancePaymentBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<AdvancePayment>> GetAdvancePaymentByadvanceAdjustmentId(int advanceAdjustmentId);
        Task<IEnumerable<AdvancePayment>> GetAdvancePaymentByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<AdvancePayment> GetAdvancePaymentById(int id);
        Task<bool> DeleteAdvancePaymentBysalaryPeriodId(int salaryPeriodId);
        Task<bool> DeleteAdvancePaymentByemployeeInfoId(int employeeInfoId, int salaryPeriodId);

        #endregion

        #region reportformat
        Task<bool> SaveReportFormat(ReportFormat reportFormat);
        Task<IEnumerable<ReportFormat>> GetReportFormat();
        Task<IEnumerable<ReportFormat>> GetReportFormatByReportType(string reportType);
        Task<bool> DeleteformatById(string reportType);
        Task<IEnumerable<UniversalCodaXLTempleteViewModel>> GetUniversalCodaXLTempleteViewModels(int PeriodId, int EmployeeId);
        #endregion

        #region salaryActivityPerchect
        Task<bool> SavesalaryActivityPercent(SalaryActivityPercent salaryActivityPercent);
        Task<IEnumerable<SalaryActivityPercent>> GetsalaryActivityPercent();
        Task<IEnumerable<SalaryActivityPercent>> GetsalaryActivityPercentByEmpId(int empId);
        Task<bool> DeletesalaryActivityPercentById(int empId);
        #endregion

        #region salaryActivityPerchect
        Task<bool> SaveCodeManagement(CodeManagement codeManagement);
        Task<IEnumerable<CodeManagement>> GetCodeManagement();
        Task<IEnumerable<CodeManagement>> GetCodeManagementByEmpPeriodId(int empId, int PeriodId);
        Task<IEnumerable<CodeManagement>> GetCodeManagementByPeriodId(int PeriodId);
        Task<bool> DeleteCodeManagementsByEmpPeriodId(int empId, int PeriodId);
        Task<bool> DeleteCodeManagementsByPeriodId(int PeriodId);
        #endregion

        #region Salary/Bonus Process
        Task<IEnumerable<SalaryProcessDataViewModel>> EmpSalaryProcess(int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<SalaryProcessDataViewModel>> EmpBonusProcess(int salaryPeriodId, int salaryHeadId, int employeeInfoId,DateTime? lastDayofPeriod,string userName, string bonusFor);
        Task<IEnumerable<SalaryProcessDataViewModel>> WagesSalaryProcess(int salaryPeriodId, int employeeInfoId);
        #endregion

        #region Wages Pay slip

        Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipByEmpId(int employeeInfoId, int salaryPeriodId);
        Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipAdditionByEmpId(int employeeInfoId, int salaryPeriodId);
        Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipDeductionByEmpId(int employeeInfoId, int salaryPeriodId);

        Task<IEnumerable<MonthlySalaryReportViewModel>> GetWagesMonthlySalaryReportByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId);

        #endregion
    }
}
