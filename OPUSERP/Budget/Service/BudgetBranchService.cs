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
    public class BudgetBranchService: IBudgetBranchService
    {
        private readonly ERPDbContext _context;

        public BudgetBranchService(ERPDbContext context)
        {
            _context = context;
        }


        //Award Info

        public async Task<bool> SaveBudgetBranchAddress(BudgetBranchAddress budgetBranchAddress)
        {
            if (budgetBranchAddress.Id != 0)
                _context.budgetBranchAddresses.Update(budgetBranchAddress);
            else
                _context.budgetBranchAddresses.Add(budgetBranchAddress);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetBranchAddress>> GetBudgetBranchAddress()
        {
            return await _context.budgetBranchAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BudgetBranchAddress>> GetBudgetBranchAddressByBranchId(int id)
        {
            return await _context.budgetBranchAddresses.Where(x=>x.budgetBranchId==id).AsNoTracking().ToListAsync();
        }

        public async Task<BudgetBranchAddress> GetBudgetBranchAddressById(int id)
        {
            return await _context.budgetBranchAddresses.FindAsync(id);
        }

        public async Task<bool> DeleteBudgetBranchAddressById(int id)
        {
            _context.budgetBranchAddresses.Remove(_context.budgetBranchAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
