using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.FinalSettlements.Interfaces;


namespace OPUSERP.Payroll.Services.FinalSettlements
{
    public class FinalSettlementService : IFinalSettlementService
    {
        private readonly ERPDbContext _context;

        public FinalSettlementService(ERPDbContext context)
        {
            _context = context;
        }

        #region FinalSettlement
        public async Task<bool> SaveFinalSettlement(FinalSettlement finalSettlement)
        {
            if (finalSettlement.Id != 0)
            {
                _context.FinalSettlements.Update(finalSettlement);
            }
            else
            {
                _context.FinalSettlements.Add(finalSettlement);
            }
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FinalSettlement>> GetAllFinalSettlements()
        {
            return await _context.FinalSettlements.Include(a => a.employeeInfo).AsNoTracking().ToListAsync();
        }
        public async Task<FinalSettlement> GetFinalSettlementsByemployeeInfoId(int employeeInfoId)
        {
            return await _context.FinalSettlements.Include(a => a.employeeInfo).Where(x =>  x.employeeInfoId == employeeInfoId).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<FinalSettlementDataViewModel> GetFinalSettlementDataByEmpId(int employeeInfoId)
        {
            return await _context.finalSettlementDataViewModels.FromSql($"spSettelmentInfo {employeeInfoId}").AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion



    }
}