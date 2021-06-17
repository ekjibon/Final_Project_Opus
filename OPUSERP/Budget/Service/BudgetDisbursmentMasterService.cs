using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service
{
    public class BudgetDisbursmentMasterService: IBudgetDisbursmentMasterService
    {
        private readonly ERPDbContext _context;

        public BudgetDisbursmentMasterService(ERPDbContext context)
        {
            _context = context;
        }
       
       

        public async Task<IEnumerable<BudgetDisbursementMaster>> GetBudgetDisbursementMaster()
        {
            return await _context.budgetDisbursementMasters.Include(x => x.fiscalYear).Include(x => x.budgetBranch).AsNoTracking().ToListAsync();
        }

       

        public async Task<IEnumerable<BudgetDisbursementMaster>> GetBudgetDisbursementMasterByBranchId(int id)
        {
            return await _context.budgetDisbursementMasters.Include(x=>x.fiscalYear).Where(x=>x.budgetBranchId==id).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetDisbursementMaster> GetBudgetDisbursementMasterById(int id)
        {
            return await _context.budgetDisbursementMasters.FindAsync(id);
        }

        public async Task<int> SaveBudgetDisbursementMaster(BudgetDisbursementMaster budgetDisbursementMaster)
        {
            if (budgetDisbursementMaster.Id != 0)
                _context.budgetDisbursementMasters.Update(budgetDisbursementMaster);
            else
                _context.budgetDisbursementMasters.Add(budgetDisbursementMaster);
            await _context.SaveChangesAsync();
            return budgetDisbursementMaster.Id;
        }

        public async Task<bool> DeleteBudgetDisbursementMasterById(int id)
        {
            _context.budgetDisbursementMasters.Remove(_context.budgetDisbursementMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
       

        public async Task<bool> SaveBudgetDisbursementDetail(BudgetDisbursementDetail budgetDisbursementDetail)
        {
            if (budgetDisbursementDetail.Id != 0)
                _context.budgetDisbursementDetails.Update(budgetDisbursementDetail);
            else
                _context.budgetDisbursementDetails.Add(budgetDisbursementDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetDisbursementDetail>> GetBudgetDisbursementDetail()
        {
            return await _context.budgetDisbursementDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetDisbursementDetail>> GetBudgetDisbursementDetailBymasterId(int id)
        {
            return await _context.budgetDisbursementDetails.Where(x => x.budgetDisbursementMasterId == id).Include(x=>x.budgetHead).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetDisbursementDetail> GetBudgetDisbursementDetailById(int id)
        {
            return await _context.budgetDisbursementDetails.FindAsync(id);
        }

        public async Task<bool> DeleteBudgetDisbursementDetailById(int id)
        {
            _context.budgetDisbursementDetails.Remove(_context.budgetDisbursementDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBudgetDisbursementDetailBymasterId(int id)
        {
            _context.budgetDisbursementDetails.RemoveRange(_context.budgetDisbursementDetails.Where(x=>x.budgetDisbursementMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
