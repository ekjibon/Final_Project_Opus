using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration
{
    public class VendorAddressService: IVendorAddressService
    {
        private readonly ERPDbContext _context;

        public VendorAddressService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVendorAddress(VendorAddress vendorAddress)
        {
            if (vendorAddress.Id != 0)
            {
                vendorAddress.updatedBy = vendorAddress.createdBy;
                vendorAddress.updatedAt = DateTime.Now;
                _context.vendorAddresses.Update(vendorAddress);
            }
            else
            {
                vendorAddress.createdAt = DateTime.Now;
                _context.vendorAddresses.Add(vendorAddress);
            }
            await _context.SaveChangesAsync();
            return vendorAddress.Id;
        }

        public async Task<IEnumerable<VendorAddress>> GetVendorAddress()
        {
            return await _context.vendorAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<VendorAddress> GetVendorAddressById(int id)
        {
            return await _context.vendorAddresses.Where(x => x.Id == id).Include(x => x.registrationForm).Include(x => x.addressType).Include(x => x.country).Include(x => x.division).Include(x => x.district).Include(x => x.thana).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VendorAddress>> GetVendorAddressByRegId(int regId)
        {
            return await _context.vendorAddresses.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).Include(x => x.addressType).Include(x => x.country).Include(x => x.division).Include(x => x.district).Include(x => x.thana).ToListAsync();
        }

        public async Task<bool> DeleteVendorAddressById(int id)
        {
            _context.vendorAddresses.Remove(_context.vendorAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
