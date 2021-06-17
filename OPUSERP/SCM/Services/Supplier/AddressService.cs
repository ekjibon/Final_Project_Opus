using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier
{
    public class AddressService: IAddressService
    {
        private readonly ERPDbContext _context;

        public AddressService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAddress(Address address)
        {
            if (address.Id != 0)
            {                
                _context.Addresses.Update(address);
            }
            else
            {                
                _context.Addresses.Add(address);
            }

            await _context.SaveChangesAsync();
            return address.Id;
        }

        public async Task<IEnumerable<Address>> GetAddress()
        {
            return await _context.Addresses.AsNoTracking().ToListAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<Address> GetAddressByOrganizationId(int id)
        {
            return await _context.Addresses.Where(x => x.organizationId == id).Include(x => x.division).Include(x => x.district).Include(x => x.thana).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressListByOrganizationId(int id)
        {
            return await _context.Addresses.Where(x => x.organizationId == id)
                .Include(x => x.division)
                .Include(x => x.district)
                .Include(x => x.thana)
                .Include(x => x.addressType)
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.Addresses.Remove(_context.Addresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAddressByOrganizationId(int id)
        {
            _context.Addresses.RemoveRange(_context.Addresses.Where(x=>x.organizationId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
