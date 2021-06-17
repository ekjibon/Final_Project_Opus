using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IStatusService
    {
        Task<bool> SaveActivityStatus(ActivityStatus activityStatus);
        Task<IEnumerable<ActivityStatus>> GetActivityStatus();
        Task<ActivityStatus> GetActivityStatusById(int id);
        Task<bool> DeleteActivityStatusById(int id);

        Task<bool> SaveServiceStatus(ServiceStatus serviceStatus);
        Task<IEnumerable<ServiceStatus>> GetServiceStatus();
        Task<ServiceStatus> GetServiceStatusById(int id);
        Task<bool> DeleteServiceStatusById(int id);
    }
}
