using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.PF.Interfaces
{
    public interface IPFService
    {
        #region FDR
        Task<int> SaveFdrInvestment(FdrInvestment fdrInvestment);
        Task<IEnumerable<FdrInvestment>> GetAllFdrInvestment();
        Task<FdrInvestment> GetFdrInvestmentById(int id);
        Task<bool> DeleteFdrInvestmentById(int id);
        #endregion
        #region PF Advance
        Task<int> SavePFAdvance(PFLoan pFLoan);
        Task<IEnumerable<PFLoan>> GetAllPFLoan();
        Task<IEnumerable<PFLoan>> GetPFLoanBysalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<PFLoan>> GetPFLoanByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<IEnumerable<PFLoan>> GetPFLoanByemployeeId(int employeeInfoId);
        Task<PFLoan> GetPFLoanById(int id);
        Task<bool> DeletePFLoanById(int id);

        #endregion
        #region PF Loan Schedule
        Task<bool> SavePFLoanSchedule(PFLoanSchedule pFLoanSchedule);
        Task<IEnumerable<PFLoanSchedule>> GetAllPFLoanSchedule();
        Task<IEnumerable<PFLoanSchedule>> GetPFLoanScheduleBySalaryPeriodId(int salaryPeriodId);
        Task<IEnumerable<PFLoanSchedule>> GetPFLoanScheduleByemployeeInfoId(int salaryPeriodId, int employeeInfoId);
        Task<PFLoanSchedule> GetPFLoanScheduleById(int id);
        Task<bool> DeletePFLoanScheduleById(int id);
        Task<bool> DeletePFLoanScheduleByPFId(int id);
        Task<IEnumerable<PFLoanScheduleViewModel>> GetLoanScheduleViewModel(int pfLoanId);
        Task<bool> UpdatePFScheduleByPaymentId(int id);
        Task<bool> UpdatePFSchedulePaymentByPaymentId(int id,int installNo,int PaymentId);

        #endregion
        #region PF Loan Payment
        Task<int> SavePFLoanPayment(PFLoanPayment pFLoanPayment);
        Task<IEnumerable<PFLoanPayment>> GetAllPFLoanPayment();
        Task<IEnumerable<PFLoanPayment>> GetPFLoanPaymentByemployeeInfoId(int employeeInfoId);
        Task<bool> DeletePFLoanPaymentById(int id);

        #endregion
    }
}
