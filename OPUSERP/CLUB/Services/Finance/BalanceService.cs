using OPUSERP.CLUB.Services.Finance.Interface;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance
{
    public class BalanceService: IBalanceService
    {
        private readonly ERPDbContext _context;

        public BalanceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveBalance(Balance balance)
        {
            if (balance.Id != 0)
                _context.balances.Update(balance);
            else
                _context.balances.Add(balance);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Balance>> GetBalance()
        {
            return await _context.balances.Include(x=>x.employee).Include(x => x.paymentHead).AsNoTracking().ToListAsync();
        }

        public async Task<Balance> GetBalanceById(int id)
        {
            return await _context.balances.FindAsync(id);
        }

        public async Task<bool> DeleteBalanceById(int id)
        {
            _context.balances.Remove(_context.balances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
