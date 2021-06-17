using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IActivityNameService
    {
        Task<bool> SaveActivityName(ActivityName activityName);
        Task<IEnumerable<ActivityName>> GetActivityName();
        Task<ActivityName> GetActivityNameById(int id);
        Task<bool> DeleteActivityNameById(int id);
        Task<IEnumerable<ActivityName>> GetActivityNameByType(int typeId);
    }
}
