using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class BelongingsItemService: IBelongingsItemService
    {
        private readonly ERPDbContext _context;

        public BelongingsItemService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveBelongingItem(BelongingItem belongingItem)
        {
            if (belongingItem.Id != 0)
                _context.belongingItems.Update(belongingItem);
            else
                _context.belongingItems.Add(belongingItem);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BelongingItem>> GetBelongingItem()
        {
            return await _context.belongingItems.AsNoTracking().ToListAsync();
        }
        
        public async Task<BelongingItem> GetBelongingItemById(int id)
        {
            return await _context.belongingItems.FindAsync(id);
        }

        public async Task<bool> DeleteBelongingItemById(int id)
        {
            _context.belongingItems.Remove(_context.belongingItems.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
