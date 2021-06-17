using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IBelongingsItemService
    {
        Task<bool> SaveBelongingItem(BelongingItem belongingItem);
        Task<IEnumerable<BelongingItem>> GetBelongingItem();
        Task<BelongingItem> GetBelongingItemById(int id);
        Task<bool> DeleteBelongingItemById(int id);
    }
}
