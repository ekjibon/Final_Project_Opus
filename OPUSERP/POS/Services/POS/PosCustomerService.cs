using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class PosCustomerService: IPosCustomerService
    {
        private readonly ERPDbContext _context;

        public PosCustomerService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PosCustomer>> GetPosCustomer()
        {
            return await _context.posCustomers.AsNoTracking().ToListAsync();
        }

        public async Task<PosCustomer> GetPosCustomerById(int id)
        {
            return await _context.posCustomers.FindAsync(id);
        }

        public async Task<int> SavePosCustomer(PosCustomer posCustomer)
        {
            if (posCustomer.Id != 0)
                _context.posCustomers.Update(posCustomer);
            else
                _context.posCustomers.Add(posCustomer);
             await _context.SaveChangesAsync();
            return posCustomer.Id;
        }

        public async Task<bool> DeletePosCustomerById(int id)
        {
            _context.posCustomers.Remove(_context.posCustomers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
