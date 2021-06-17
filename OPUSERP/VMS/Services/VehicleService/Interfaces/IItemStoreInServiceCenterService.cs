using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;

namespace OPUSERP.VMS.Services.VehicleService.Interfaces
{
   public interface IItemStoreInServiceCenterService
    {
        Task<bool> SaveItemStoreInServiceCenter(ItemStoreInServiceCenter itemStoreInServiceCenter);
        Task<IEnumerable<ItemStoreInServiceCenter>> GetItemStoreInServiceCenter();
        Task<ItemStoreInServiceCenter> GetItemStoreInServiceCenterById(int id);
        Task<bool> DeleteItemStoreInServiceCenterById(int id);
        Task<IEnumerable<ItemStoreInServiceCenter>> GetItemStoreInServiceCenterByServiceHistoryId(int Id);
        Task<bool> DeleteItemStoreInServiceCenterByHistoryId(int id);
    }
}
