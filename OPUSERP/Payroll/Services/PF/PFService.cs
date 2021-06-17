using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.PF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.PF
{
    public class PFService:IPFService
    {
        private readonly ERPDbContext _context;
        public PFService(ERPDbContext context)
        {
            _context = context;
        }
        #region FDR

        public async Task<int> SaveFdrInvestment(FdrInvestment fdrInvestment)
        {
            if (fdrInvestment.Id != 0)
            {
                _context.FdrInvestments.Update(fdrInvestment);
            }
            else
            {
                _context.FdrInvestments.Add(fdrInvestment);
            }
            await _context.SaveChangesAsync();
            return fdrInvestment.Id;
        }

        public async Task<IEnumerable<FdrInvestment>> GetAllFdrInvestment()
        {
            return await _context.FdrInvestments.AsNoTracking().ToListAsync();
        }

        public async Task<FdrInvestment> GetFdrInvestmentById(int id)
        {
            return await _context.FdrInvestments.FindAsync(id);
        }

        public async Task<bool> DeleteFdrInvestmentById(int id)
        {
            _context.FdrInvestments.RemoveRange(_context.FdrInvestments.Where(a => a.Id == id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
        #region PF Advance

        public async Task<int> SavePFAdvance(PFLoan pFLoan)
        {
            if (pFLoan.Id != 0)
                _context.PFLoans.Update(pFLoan);
            else
                _context.PFLoans.Add(pFLoan);
            await _context.SaveChangesAsync();
            return pFLoan.Id;
        }

        public async Task<IEnumerable<PFLoan>> GetAllPFLoan()
        {
            return await _context.PFLoans.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PFLoan>> GetPFLoanBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.PFLoans.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PFLoan>> GetPFLoanByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.PFLoans.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId == salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PFLoan>> GetPFLoanByemployeeId( int employeeInfoId)
        {
            List<int> lstPF =  _context.PFLoanSchedules.Include(x=>x.pFLoan).Where(x => x.isComplete == 0 && x.pFLoan.employeeInfoId == employeeInfoId).Select(x=>(int)x.pFLoanId).ToList();
            return await _context.PFLoans.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x =>  x.employeeInfoId == employeeInfoId&&lstPF.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<PFLoan> GetPFLoanById(int id)
        {
            return await _context.PFLoans.FindAsync(id);
        }

        public async Task<bool> DeletePFLoanById(int id)
        {
            _context.PFLoans.Remove(_context.PFLoans.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region PF Loan Schedule

        public async Task<bool> SavePFLoanSchedule(PFLoanSchedule pFLoanSchedule)
        {
            if (pFLoanSchedule.Id != 0)
                _context.PFLoanSchedules.Update(pFLoanSchedule);
            else
                _context.PFLoanSchedules.Add(pFLoanSchedule);
          return 1 ==  await _context.SaveChangesAsync();
          
        }

        public async Task<IEnumerable<PFLoanSchedule>> GetAllPFLoanSchedule()
        {
            return await _context.PFLoanSchedules.Include(x => x.pFLoan).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PFLoanSchedule>> GetPFLoanScheduleBySalaryPeriodId(int salaryPeriodId)
        {
            return await _context.PFLoanSchedules.Include(x => x.pFLoan).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PFLoanSchedule>> GetPFLoanScheduleByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.PFLoanSchedules.Include(x => x.pFLoan.employeeInfo).Where(x => x.salaryPeriodId <= salaryPeriodId && x.pFLoan.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<PFLoanSchedule> GetPFLoanScheduleById(int id)
        {
            return await _context.PFLoanSchedules.FindAsync(id);
        }
        public async Task<IEnumerable<PFLoanScheduleViewModel>> GetLoanScheduleViewModel(int pfLoanId)
        {
            PFLoan pfdata =  _context.PFLoans.Where(x => x.Id == pfLoanId).FirstOrDefault();
            List<PFLoanScheduleViewModel> data = new List<PFLoanScheduleViewModel>();
            List<SalaryPeriod> salaryPeriods = await _context.salaryPeriods.Include(x => x.salaryYear).ToListAsync();
            List<PFLoanSchedule> pFLoanSchedules =await _context.PFLoanSchedules.Include(x=>x.pFLoan.employeeInfo).Where(x => x.pFLoanId == pfLoanId).ToListAsync();
            foreach (PFLoanSchedule d in pFLoanSchedules)
            {
                string periodName = salaryPeriods.Where(x => x.Id == d.salaryPeriodId)?.FirstOrDefault()?.periodName;
                string status = string.Empty;
                if (d.isComplete == 1)
                {
                    status = "Paid";
                }
                else
                {
                    status = "Not Paid";
                }
                if (periodName == null)
                {
                    periodName = "";
                }
                data.Add(new PFLoanScheduleViewModel
                {
                    periodName=periodName,
                    noOfInstallment=d.noOfInstallment,
                    advanceAmount=pfdata.advanceAmount,
                    installmentAmount=d.installmentAmount,
                    restAmount=d.restAmount,
                    status=status

                });
            }
            return data;

        }
        public async Task<bool> UpdatePFScheduleByPaymentId(int id)
        {
           IEnumerable<PFLoanSchedule> pFLoanSchedule = await _context.PFLoanSchedules.Where(x=>x.PFLoanPaymentId==id).ToListAsync();
            foreach (PFLoanSchedule data in pFLoanSchedule)
            {
                data.isComplete = 0;
                data.PFLoanPaymentId = null;
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> UpdatePFSchedulePaymentByPaymentId(int id, int installNo, int paymentId)
        {
            int pFLoanScheduleId = await _context.PFLoanSchedules.Where(x => x.pFLoanId == id &&x.isComplete==0).Select(x=>x.Id).FirstOrDefaultAsync();
            for (int i = 0; i < installNo; i++)
            {
                PFLoanSchedule data = await _context.PFLoanSchedules.Where(x => x.Id == pFLoanScheduleId).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.isComplete = 1;
                    data.PFLoanPaymentId = paymentId;
                    await _context.SaveChangesAsync();
                }
                pFLoanScheduleId++;
            }
            return true;
        }
        public async Task<bool> DeletePFLoanScheduleById(int id)
        {
            _context.PFLoanSchedules.Remove(_context.PFLoanSchedules.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletePFLoanScheduleByPFId(int id)
        {
            _context.PFLoanSchedules.RemoveRange(_context.PFLoanSchedules.Where(x=>x.pFLoanId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region PF Loan Payment

        public async Task<int> SavePFLoanPayment(PFLoanPayment pFLoanPayment)
        {
            if (pFLoanPayment.Id != 0)
                _context.PFLoanPayments.Update(pFLoanPayment);
            else
                _context.PFLoanPayments.Add(pFLoanPayment);
           await _context.SaveChangesAsync();
            return pFLoanPayment.Id;

        }

        public async Task<IEnumerable<PFLoanPayment>> GetAllPFLoanPayment()
        {
            return await _context.PFLoanPayments.Include(x => x.employeeInfo).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        
        public async Task<IEnumerable<PFLoanPayment>> GetPFLoanPaymentByemployeeInfoId(int employeeInfoId)
        {
            return await _context.PFLoanPayments.Include(x => x.employeeInfo).Where(x =>x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        
        public async Task<bool> DeletePFLoanPaymentById(int id)
        {
            _context.PFLoanPayments.Remove(_context.PFLoanPayments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
       
        #endregion
    }
}
