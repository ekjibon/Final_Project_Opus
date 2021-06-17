using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IStatusService
    {
        #region Activity Status
        Task<bool> SaveActivityStatus(ActivityStatus activityStatus);
        Task<IEnumerable<ActivityStatus>> GetActivityStatus();
        Task<ActivityStatus> GetActivityStatusById(int id);
        Task<bool> DeleteActivityStatusById(int id);

        #endregion Service Status

        #region
        Task<bool> SaveServiceStatus(ServiceStatus serviceStatus);
        Task<IEnumerable<ServiceStatus>> GetServiceStatus();
        Task<ServiceStatus> GetServiceStatusById(int id);
        Task<bool> DeleteServiceStatusById(int id);


        #endregion

        #region
        Task<bool> SaveHrProgram(HrProgram hrProgram);
        Task<IEnumerable<HrProgram>> GetHrProgram();
        Task<HrProgram> GetHrProgramById(int id);
        Task<bool> DeleteHrProgramById(int id);

        #endregion

        #region
        Task<bool> SaveHrUnit(HrUnit hrUnit);
        Task<IEnumerable<HrUnit>> GetHrUnit();
        Task<HrUnit> GetHrUnitById(int id);
        Task<bool> DeleteHrUnitById(int id);
        #endregion
    }
}
