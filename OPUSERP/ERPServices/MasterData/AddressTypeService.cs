using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData
{
    public class AddressTypeService : IAddressTypeService
    {
        private readonly ERPDbContext _context;

        public AddressTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAddressType(AddressType addressType)
        {
            if (addressType.Id != 0)
                _context.AddressTypes.Update(addressType);
            else
                _context.AddressTypes.Add(addressType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AddressType>> GetAllAddressType()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        public async Task<AddressType> GetAddressTypeById(int id)
        {
            return await _context.AddressTypes.FindAsync(id);
        }

        public async Task<bool> DeleteAddressTypeById(int id)
        {
            _context.AddressTypes.Remove(_context.AddressTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
