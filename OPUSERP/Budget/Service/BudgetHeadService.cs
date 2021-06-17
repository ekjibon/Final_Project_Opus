using Microsoft.EntityFrameworkCore;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service
{
    public class BudgetHeadService: IBudgetHeadService
    {
        private readonly ERPDbContext _context;

        public BudgetHeadService(ERPDbContext context)
        {
            _context = context;
        }

        //Main Head

        public async Task<bool> SaveBudgetMainHead(BudgetMainHead budgetMainHead)
        {
            if (budgetMainHead.Id != 0)
                _context.budgetMainHeads.Update(budgetMainHead);
            else
                _context.budgetMainHeads.Add(budgetMainHead);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetMainHead>> GetBudgetMainHead()
        {
            return await _context.budgetMainHeads.AsNoTracking().ToListAsync();
        }

        public async Task<BudgetMainHead> GetBudgetMainHeadById(int id)
        {
            return await _context.budgetMainHeads.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBudgetMainHeadById(int id)
        {
            _context.budgetMainHeads.Remove(_context.budgetMainHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Sub Head

        public async Task<bool> SaveBudgetSubHead(BudgetSubHead budgetSubHead)
        {
            if (budgetSubHead.Id != 0)
                _context.budgetSubHeads.Update(budgetSubHead);
            else
                _context.budgetSubHeads.Add(budgetSubHead);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetSubHead>> GetBudgetSubHead()
        {
            return await _context.budgetSubHeads.AsNoTracking().Include(x => x.budgetMainHead).ToListAsync();
        }

        public async Task<IEnumerable<BudgetSubHead>> GetBudgetSubHeadByMainHeadId(int mainHeadId)
        {
            return await _context.budgetSubHeads.Where(x=>x.budgetMainHeadId==mainHeadId).Include(x => x.budgetMainHead).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetSubHead> GetBudgetSubHeadById(int id)
        {
            return await _context.budgetSubHeads.Include(x => x.budgetMainHead).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBudgetSubHeadById(int id)
        {
            _context.budgetSubHeads.Remove(_context.budgetSubHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Head

        public async Task<IEnumerable<BudgetHead>> GetBudgetHead()
        {
            return await _context.budgetHeads.Include(x=>x.budgetSubHead.budgetMainHead).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetHead> GetBudgetHeadById(int id)
        {
            return await _context.budgetHeads.Include(x=>x.budgetSubHead.budgetMainHead).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<BudgetHead> GetBudgetHeadByCode(string code)
        {
            return await _context.budgetHeads.Include(x => x.budgetSubHead.budgetMainHead).Where(x => x.code == code).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBudgetHead(BudgetHead budgetHead)
        {
            if (budgetHead.Id != 0)
                _context.budgetHeads.Update(budgetHead);
            else
                _context.budgetHeads.Add(budgetHead);
            await _context.SaveChangesAsync();
            return budgetHead.Id;
        }

        public async Task<bool> DeleteBudgetHeadById(int id)
        {
            _context.budgetHeads.Remove(_context.budgetHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        //Head Details

        public async Task<IEnumerable<BudgetHeadDetail>> GetBudgetHeadDetail()
        {
            return await _context.budgetHeadDetails.Include(x => x.budgetHead).Include(x=>x.ledger).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetHeadDetail> GetBudgetHeadDetailById(int id)
        {
            return await _context.budgetHeadDetails.Include(x => x.budgetHead).Include(x => x.ledger).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BudgetHeadDetail>> GetBudgetHeadDetailByHeadId(int id)
        {
            return await _context.budgetHeadDetails.Include(x => x.budgetHead).Include(x => x.ledger).Where(x => x.budgetHeadId == id).AsNoTracking().ToListAsync();
        }


        public async Task<int> SaveBudgetHeadDetail(BudgetHeadDetail budgetHeadDetail)
        {
            if (budgetHeadDetail.Id != 0)
                _context.budgetHeadDetails.Update(budgetHeadDetail);
            else
                _context.budgetHeadDetails.Add(budgetHeadDetail);
            await _context.SaveChangesAsync();
            return budgetHeadDetail.Id;
        }

        public async Task<bool> DeleteBudgetHeadDetailById(int id)
        {
            _context.budgetHeadDetails.Remove(_context.budgetHeadDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteBudgetHeadDetailByHeadId(int id)
        {
            _context.budgetHeadDetails.RemoveRange(_context.budgetHeadDetails.Where(x=>x.budgetHeadId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
