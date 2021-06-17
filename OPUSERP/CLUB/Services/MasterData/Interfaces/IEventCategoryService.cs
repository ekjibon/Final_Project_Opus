using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
   public interface IEventCategoryService
    {
        Task<bool> SaveEventCategory(EventCategory eventCategory);
        Task<IEnumerable<EventCategory>> GetAllEventCategory();
        Task<EventCategory> GetEventCategoryById(int id);
        Task<bool> DeleteEventCategoryById(int id);
    }
}
