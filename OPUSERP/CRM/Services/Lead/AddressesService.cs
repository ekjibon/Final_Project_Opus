using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
   
    public class AddressesService : IAddressesService
    {
        private readonly ERPDbContext _context;

        public AddressesService(ERPDbContext context)
        {
            _context = context;
        }

        #region Address        

        public async Task<bool> SaveAddresses(Address Addresses)
        {
            if (Addresses.Id != 0)
                _context.Address.Update(Addresses);
            else
                _context.Address.Add(Addresses);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _context.Address.ToListAsync();
        }

        public async Task<Address> GetAddressesById(int id)
        {
            return await _context.Address.FindAsync(id);
        }

        public async Task<bool> DeleteAddressesById(int id)
        {
            _context.Address.Remove(_context.Address.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesByLeadId(int leadId)
        {
            return await _context.Address.AsNoTracking().Where(x => x.leadsId == leadId).Include(x => x.division).Include(x => x.postOffice).Include(x => x.district).Include(x => x.thana).ToListAsync();
        }

        #endregion
    }
}
