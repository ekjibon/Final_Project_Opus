using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IHRPMSActivityTypeService
    {
        Task<bool> SaveHRPMSActivityType(HRPMSActivityType hRPMSActivityType);
        Task<IEnumerable<HRPMSActivityType>> GetHRPMSActivityType();
        Task<HRPMSActivityType> GetHRPMSActivityTypeById(int id);
        Task<bool> DeleteHRPMSActivityTypeById(int id);
    }
}
