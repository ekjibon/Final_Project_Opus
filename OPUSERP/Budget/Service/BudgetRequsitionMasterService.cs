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
    public class BudgetRequsitionMasterService: IBudgetRequsitionMasterService
    {
        private readonly ERPDbContext _context;

        public BudgetRequsitionMasterService(ERPDbContext context)
        {
            _context = context;
        }
        //Fiscal Year
        public async Task<IEnumerable<FiscalYear>> GetFiscalYear()
        {
            return await _context.fiscalYears.AsNoTracking().ToListAsync();
        }

        public async Task<FiscalYear> GetFiscalYearById(int id)
        {
            return await _context.fiscalYears.FindAsync(id);
        }

        public async Task<int> SaveFiscalYear(FiscalYear fiscalYear)
        {
            if (fiscalYear.Id != 0)
                _context.fiscalYears.Update(fiscalYear);
            else
                _context.fiscalYears.Add(fiscalYear);
            await _context.SaveChangesAsync();
            return fiscalYear.Id;
        }

        public async Task<bool> DeleteFiscalYearById(int id)
        {
            _context.fiscalYears.Remove(_context.fiscalYears.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMaster()
        {
            return await _context.budgetRequsitionMasters.Include(x=>x.fiscalYear).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BranchBudgetViewModel>> GetBudgetRequsitionMasterForBranchApprove(int statusId,string userName)
        {
            var result = await(from m in _context.budgetRequsitionMasters
                         join d in _context.budgetRequsitionDetails on m.Id equals d.budgetRequsitionMasterId
                         join f in _context.fiscalYears on m.fiscalYearId equals f.Id
                         join r in _context.Users on m.RequsitionBy equals r.userId
                         where m.status == statusId
                         select new BranchBudgetViewModel
                         {

                         }).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByBranchId(int id)
        {
            return await _context.budgetRequsitionMasters.Include(x=>x.fiscalYear).Where(x=>x.type==null).Where(x=>x.budgetBranchId==id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByuserId(int id)
        {
            return await _context.budgetRequsitionMasters.Include(x => x.fiscalYear).Where(x => x.RequsitionBy == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterForApproval(int userId)
        {
            return await _context.budgetRequisitionApprovedListViewModels.FromSql(@"Sp_GetBudgetRequisitionListForApproved @p0", userId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByBranchIdAndType(int id,int type)
        {
            return await _context.budgetRequsitionMasters.Include(x => x.fiscalYear).Where(x => x.budgetBranchId == id).Where(x=>x.type== type).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetRequsitionMaster> GetBudgetRequsitionMasterById(int id)
        {
            return await _context.budgetRequsitionMasters.Include(x=>x.budgetBranch).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBudgetRequsitionMaster(BudgetRequsitionMaster budgetRequsitionMaster)
        {
            if (budgetRequsitionMaster.Id != 0)
                _context.budgetRequsitionMasters.Update(budgetRequsitionMaster);
            else
                _context.budgetRequsitionMasters.Add(budgetRequsitionMaster);
            await _context.SaveChangesAsync();
            return budgetRequsitionMaster.Id;
        }

        public async Task<bool> DeleteBudgetRequsitionMasterById(int id)
        {
            _context.budgetRequsitionMasters.Remove(_context.budgetRequsitionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
       

        public async Task<bool> SaveBudgetRequsitionDetail(BudgetRequsitionDetail budgetRequsitionDetail)
        {
            if (budgetRequsitionDetail.Id != 0)
                _context.budgetRequsitionDetails.Update(budgetRequsitionDetail);
            else
                _context.budgetRequsitionDetails.Add(budgetRequsitionDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionDetail>> GetBudgetRequsitionDetail()
        {
            return await _context.budgetRequsitionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionDetail>> GetBudgetRequsitionDetailBymasterId(int id)
        {
            return await _context.budgetRequsitionDetails.Where(x => x.budgetRequsitionMasterId == id).Include(x=>x.budgetHead).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetRequsitionDetail> GetBudgetRequsitionDetailById(int id)
        {
            return await _context.budgetRequsitionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteBudgetRequsitionDetailById(int id)
        {
            _context.budgetRequsitionDetails.Remove(_context.budgetRequsitionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBudgetRequsitionDetailBymasterId(int id)
        {
            _context.budgetRequsitionDetails.RemoveRange(_context.budgetRequsitionDetails.Where(x=>x.budgetRequsitionMasterId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColumnHeading>> GetAllColumnBySp()
        {
            var data = _context.columnHeadings.FromSql("sp_GetColumnName");
            return await data.ToListAsync();
        }
        public void UpdateBudgetRequsitionStatusById(int reqId, int status)
        {
            var user = _context.budgetRequsitionMasters.Find(reqId);
            user.status = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void UpdateBudgetRequsitionStatusFinById(int reqId, int status)
        {
            var user = _context.budgetRequsitionMasterFins.Find(reqId);
            user.status = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateBudgetRequsitionIsProcessByYear(int year)
        {
            var users = _context.budgetRequsitionMasters.Where(x=>x.fiscalYearId == year).ToList();
            foreach(var user in users)
            {
                user.isProcess = 1;
                user.updatedAt = DateTime.Now;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }


        public async Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFin()
        {
            return await _context.budgetRequsitionMasterFins.Include(x => x.fiscalYear).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BranchBudgetViewModel>> GetBudgetRequsitionMasterFinForBranchApprove(int statusId, string userName)
        {
            var result = await (from m in _context.budgetRequsitionMasterFins
                                join d in _context.budgetRequsitionDetailFins on m.Id equals d.budgetRequsitionMasterId
                                join f in _context.fiscalYears on m.fiscalYearId equals f.Id
                                join r in _context.Users on m.RequsitionBy equals r.userId
                                where m.status == statusId
                                select new BranchBudgetViewModel
                                {

                                }).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByBranchId(int id)
        {
            return await _context.budgetRequsitionMasterFins.Include(x => x.fiscalYear).Where(x => x.type == null).Where(x => x.budgetBranchId == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByuserId(int id)
        {
            var data= await _context.budgetRequsitionMasterFins.Where(x => x.RequsitionBy == id).AsNoTracking().ToListAsync();
            return data;
        }

       

        public async Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByBranchIdAndType(int id, int type)
        {
            return await _context.budgetRequsitionMasterFins.Include(x => x.fiscalYear).Where(x => x.budgetBranchId == id).Where(x => x.type == type).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetRequsitionMasterFin> GetBudgetRequsitionMasterFinById(int id)
        {
            return await _context.budgetRequsitionMasterFins.Include(x => x.budgetBranch).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBudgetRequsitionMasterFin(BudgetRequsitionMasterFin budgetRequsitionMaster)
        {
            if (budgetRequsitionMaster.Id != 0)
                _context.budgetRequsitionMasterFins.Update(budgetRequsitionMaster);
            else
                _context.budgetRequsitionMasterFins.Add(budgetRequsitionMaster);
            await _context.SaveChangesAsync();
            return budgetRequsitionMaster.Id;
        }

        public async Task<bool> DeleteBudgetRequsitionMasterFinById(int id)
        {
            _context.budgetRequsitionMasterFins.Remove(_context.budgetRequsitionMasterFins.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<bool> SaveBudgetRequsitionDetailFin(BudgetRequsitionDetailFin budgetRequsitionDetail)
        {
            if (budgetRequsitionDetail.Id != 0)
                _context.budgetRequsitionDetailFins.Update(budgetRequsitionDetail);
            else
                _context.budgetRequsitionDetailFins.Add(budgetRequsitionDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterFinForApproval(int userId)
        {
            return await _context.budgetRequisitionApprovedListViewModels.FromSql(@"Sp_GetBudgetRequisitionListFinForApproved @p0", userId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionDetailFin>> GetBudgetRequsitionDetailFin()
        {
            return await _context.budgetRequsitionDetailFins.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequsitionDetailFin>> GetBudgetRequsitionDetailFinBymasterId(int id)
        {
            return await _context.budgetRequsitionDetailFins.Where(x => x.budgetRequsitionMasterId == id).Include(x => x.budgetHead).Include(x=>x.partner).Include(x=>x.ledgerRelation).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetRequsitionDetailFin> GetBudgetRequsitionDetailFinById(int id)
        {
            return await _context.budgetRequsitionDetailFins.FindAsync(id);
        }

        public async Task<bool> DeleteBudgetRequsitionDetailFinById(int id)
        {
            _context.budgetRequsitionDetailFins.Remove(_context.budgetRequsitionDetailFins.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBudgetRequsitionDetailFinBymasterId(int id)
        {
            _context.budgetRequsitionDetailFins.RemoveRange(_context.budgetRequsitionDetailFins.Where(x => x.budgetRequsitionMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
