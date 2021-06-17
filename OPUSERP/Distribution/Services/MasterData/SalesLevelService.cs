using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData
{
    public class SalesLevelService:ISalesLevelService
    {
        private readonly ERPDbContext _context;

        public SalesLevelService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSalesLevelById(int id)
        {
            _context.SalesLevels.Remove(_context.SalesLevels.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesLevel>> GetAllSalesLevel()
        {
            return await _context.SalesLevels.Include(x=>x.salesLevel).AsNoTracking().ToListAsync();
        }

        public async Task<SalesLevel> GetSalesLevelById(int id)
        {
            return await _context.SalesLevels.Where(x=>x.Id==id).Include(x=>x.salesLevel).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SalesLevel>> GetAllSalesLevelbyparentId(int Id)
        {
            return await _context.SalesLevels.Include(x => x.salesLevel).Where(x=>x.salesLevelId==Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<SalesLevel>> GetAllSalesLevelbyparentridId(int Id,int rid)
        {
            List<int?> lstsalesdeployid = new List<int?>();
            lstsalesdeployid = _context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == rid).Select(x => x.salesLevelId).ToList();
            if (Id == 0)
            {
                return await _context.SalesLevels.Include(x => x.salesLevel).Where(x => x.salesLevelId==null&&lstsalesdeployid.Contains(x.Id)).AsNoTracking().ToListAsync();

            }
            else
            {
                return await _context.SalesLevels.Include(x => x.salesLevel).Where(x => x.salesLevelId == Id && lstsalesdeployid.Contains(x.Id)).AsNoTracking().ToListAsync();
            }
           
        }
        public async Task<bool> SaveSalesLevel(SalesLevel salesLevel)
        {
            if (salesLevel.Id != 0)
                _context.SalesLevels.Update(salesLevel);
            else
                _context.SalesLevels.Add(salesLevel);

            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
