using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Services.VehicleService.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.VehicleService
{
    public class ItemStoreInServiceCenterService: IItemStoreInServiceCenterService
    {
        private readonly ERPDbContext _context;

        public ItemStoreInServiceCenterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemStoreInServiceCenter>> GetItemStoreInServiceCenter()
        {
            return await _context.ItemStoreInServiceCenters.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ItemStoreInServiceCenter>> GetItemStoreInServiceCenterByServiceHistoryId(int Id)
        {
            return await _context.ItemStoreInServiceCenters.Where(x=>x.vehicleServiceHistoryId== Id).Include(x=>x.spareParts).AsNoTracking().ToListAsync();
        }

        public async Task<ItemStoreInServiceCenter> GetItemStoreInServiceCenterById(int id)
        {
            return await _context.ItemStoreInServiceCenters.FindAsync(id);
        }

        public async Task<bool> SaveItemStoreInServiceCenter(ItemStoreInServiceCenter itemStoreInServiceCenter)
        {
            if (itemStoreInServiceCenter.Id != 0)
                _context.ItemStoreInServiceCenters.Update(itemStoreInServiceCenter);
            else
                _context.ItemStoreInServiceCenters.Add(itemStoreInServiceCenter);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemStoreInServiceCenterById(int id)
        {
            _context.ItemStoreInServiceCenters.Remove(_context.ItemStoreInServiceCenters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemStoreInServiceCenterByHistoryId(int id)
        {
           _context.ItemStoreInServiceCenters.RemoveRange(_context.ItemStoreInServiceCenters.Where(x=>x.vehicleServiceHistoryId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
