using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Event.Interfaces
{
   public interface IEventCategoryService
    {
        Task<bool> SaveEventCategory(EventCategory eventCategory);
        Task<IEnumerable<EventCategory>> GetAllEventCategory();
        Task<EventCategory> GetEventCategoryById(int id);
        Task<bool> DeleteEventCategoryById(int id);
    }
}
