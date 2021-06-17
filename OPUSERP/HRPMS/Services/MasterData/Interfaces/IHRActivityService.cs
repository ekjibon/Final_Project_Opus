using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IHRActivityService
    {
        #region HR Activity

        Task<bool> SaveHRActivity(HRActivity hRActivity);
        Task<IEnumerable<HRActivity>> GetHRActivity();
        Task<HRActivity> GetHRActivityById(int id);
        Task<bool> DeleteHRActivityById(int id);

        #endregion

        #region HR Facility

        Task<bool> SaveHRFacility(HRFacility hRFacility);
        Task<IEnumerable<HRFacility>> GetHRFacility();
        Task<HRFacility> GetHRFacilityById(int id);
        Task<bool> DeleteHRFacilityById(int id);

        #endregion
    }
}
