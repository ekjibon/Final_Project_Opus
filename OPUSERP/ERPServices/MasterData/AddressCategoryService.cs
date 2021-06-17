using Microsoft.EntityFrameworkCore;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData
{
    public class AddressCategoryService: IAddressCategoryService
    {
        private readonly ERPDbContext _context;

        public AddressCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddressCategory>> GetAddressCategory()
        {
            return await _context.addressCategories.AsNoTracking().ToListAsync();
        }

        public async Task<AddressCategory> GetAddressCategoryById(int id)
        {
            return await _context.addressCategories.FindAsync(id);
        }

        public async Task<bool> SaveAddressCategory(AddressCategory addressCategory)
        {
            if (addressCategory.Id != 0)
                _context.addressCategories.Update(addressCategory);
            else
                _context.addressCategories.Add(addressCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAddressCategoryById(int id)
        {
            _context.addressCategories.Remove(_context.addressCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetAddressCategoryCheck(int id, string name)
        {
            var Result = await _context.addressCategories.Where(x => x.name == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<FiscalYear>> GetAllFiscalYear()
        {
            return await _context.fiscalYears.AsNoTracking().ToListAsync();
        }
    }
}
