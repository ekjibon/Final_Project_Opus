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
    public class HOBudgetRequsitionService: IHOBudgetRequsitionService
    {
        private readonly ERPDbContext _context;

        public HOBudgetRequsitionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HOBudgetRequsitionMaster>> GetBudgetRequsitionMaster()
        {
            return await _context.hOBudgetRequsitionMasters.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HOBudgetRequsitionMaster>> GetBudgetRequsitionMasterByUserId(int UserId)
        {
            return await _context.hOBudgetRequsitionMasters.Include(x => x.fiscalYear).Where(x=>x.RequsitionBy == UserId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterForApproval(int userId)
        {
            return await _context.budgetRequisitionApprovedListViewModels.FromSql(@"Sp_GetBudgetRequisitionListForApprovedHO @p0", userId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HOBudgetRequsitionDetail>> PROCESS_HOBudgetRequsitionDetails(int fiscalYearId, int hOBudgetRequsitionMasterId)
        {
            return await _context.hOBudgetRequsitionDetails.FromSql(@"SP_PROCESS_HOBudgetRequsition @p0,@p1", fiscalYearId, hOBudgetRequsitionMasterId).AsNoTracking().ToListAsync();
        }

        public async Task<HOBudgetRequsitionMaster> GetBudgetRequsitionMasterById(int id)
        {
            return await _context.hOBudgetRequsitionMasters.FindAsync(id);
        }

        public async Task<int> SaveBudgetRequsitionMaster(HOBudgetRequsitionMaster budgetRequsitionMaster)
        {
            if (budgetRequsitionMaster.Id != 0)
                _context.hOBudgetRequsitionMasters.Update(budgetRequsitionMaster);
            else
                _context.hOBudgetRequsitionMasters.Add(budgetRequsitionMaster);
            await _context.SaveChangesAsync();
            return budgetRequsitionMaster.Id;
        }

        public async Task<IEnumerable<HOBudgetRequsitionDetail>> PROCESSHOBudgetRequsition(int fiscalYearId)
        {
            var reqIdList = await _context.budgetRequsitionMasters.Where(x => x.fiscalYearId == fiscalYearId && x.isProcess==0).Select(x=>x.Id).ToListAsync();

            IEnumerable<BudgetRequsitionDetail> budgetRequsitionDetails = await _context.budgetRequsitionDetails.Where(x=>reqIdList.Contains((int)x.budgetRequsitionMasterId)).ToListAsync();

            List<int?> headList = budgetRequsitionDetails.Select(x => x.budgetHeadId).Distinct().ToList();

            List<HOBudgetRequsitionDetail> hOBudgetRequsitionDetails = new List<HOBudgetRequsitionDetail>();
            IEnumerable<BudgetHead> budgetHeads = await _context.budgetHeads.ToListAsync();
            foreach (int data in headList)
            {
                hOBudgetRequsitionDetails.Add(new HOBudgetRequsitionDetail
                {
                    budgetHeadId =data,
                    budgetHead = budgetHeads.Where(x=>x.Id==data).FirstOrDefault(),
                    firstMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.firstMonth),
                    secondMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.secondMonth),
                    thirdMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.thirdMonth),
                    fourthMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.fourthMonth),
                    fifthMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.fifthMonth),
                    sixthMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.sixthMonth),
                    seventhMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.seventhMonth),
                    eighthMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.eighthMonth),
                    ninethMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.ninethMonth),
                    tenthMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.tenthMonth),
                    eleventhMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.eleventhMonth),
                    twelvethMonth = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.twelvethMonth),
                    subTotal = budgetRequsitionDetails.Where(x=>x.budgetHeadId==data).Sum(x=>x.subTotal),
                });
            }
            return hOBudgetRequsitionDetails;
        }

        public async Task<bool> DeleteBudgetRequsitionMasterById(int id)
        {
            _context.hOBudgetRequsitionMasters.Remove(_context.hOBudgetRequsitionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<bool> SaveBudgetRequsitionDetail(HOBudgetRequsitionDetail budgetRequsitionDetail)
        {
            if (budgetRequsitionDetail.Id != 0)
                _context.hOBudgetRequsitionDetails.Update(budgetRequsitionDetail);
            else
                _context.hOBudgetRequsitionDetails.Add(budgetRequsitionDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HOBudgetRequsitionDetail>> GetBudgetRequsitionDetail()
        {
            return await _context.hOBudgetRequsitionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HOBudgetRequsitionDetail>> GetBudgetRequsitionDetailBymasterId(int id)
        {
            return await _context.hOBudgetRequsitionDetails.Where(x => x.hOBudgetRequsitionMasterId == id).Include(x => x.budgetHead).AsNoTracking().ToListAsync();
        }

        public async Task<HOBudgetRequsitionDetail> GetBudgetRequsitionDetailById(int id)
        {
            return await _context.hOBudgetRequsitionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteBudgetRequsitionDetailById(int id)
        {
            _context.hOBudgetRequsitionDetails.Remove(_context.hOBudgetRequsitionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBudgetRequsitionDetailBymasterId(int id)
        {
            _context.hOBudgetRequsitionDetails.RemoveRange(_context.hOBudgetRequsitionDetails.Where(x => x.hOBudgetRequsitionMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColumnHeading>> GetAllColumnBySp()
        {
            var data = _context.columnHeadings.FromSql("sp_GetColumnName");
            return await data.ToListAsync();
        }
        public void UpdateBudgetRequsitionStatusById(int reqId, int status)
        {
            var user = _context.hOBudgetRequsitionMasters.Find(reqId);
            user.status = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
