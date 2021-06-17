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
    public class SupplierItemInfoService: ISupplierItemInfoService
    {
        private readonly ERPDbContext _context;

        public SupplierItemInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveSupplierItemInfo(SupplierItemInfo supplierItemInfo)
        {
            if (supplierItemInfo.Id != 0)
            {
                _context.SupplierItemInfos.Update(supplierItemInfo);
            }
            else
            {
                _context.SupplierItemInfos.Add(supplierItemInfo);
            }
            await _context.SaveChangesAsync();
            return supplierItemInfo.Id;
        }

        public async Task<IEnumerable<SupplierItemInfo>> GetAllSupplierItemInfo()
        {
            return await _context.SupplierItemInfos.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<SupplierItemInfo>> GetSupplierItemInfobyOrganizationId(int Id)
        {
            return await _context.SupplierItemInfos.Where(x => x.organizationId == Id).Include(x => x.item).AsNoTracking().ToListAsync();
        }

        public async Task<SupplierItemInfo> GetSupplierItemInfoById(int id)
        {
            return await _context.SupplierItemInfos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteSupplierItemInfoById(int id)
        {
            _context.SupplierItemInfos.Remove(_context.SupplierItemInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSupplierItemInfoByOrganizationId(int id)
        {
            _context.SupplierItemInfos.RemoveRange(_context.SupplierItemInfos.Where(x=>x.organizationId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
