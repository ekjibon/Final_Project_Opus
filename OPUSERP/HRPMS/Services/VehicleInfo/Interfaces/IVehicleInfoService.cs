using OPUSERP.HRPMS.Data.Entity.VehicleInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IVehicleInfoService
    {        
        Task<int> SaveVehicleInfo(VehicleEntry vehicleEntry);
        Task<IEnumerable<VehicleEntry>> GetVehicleInfo();
        Task<IEnumerable<VehicleEntry>> GetVehicleInfoByOrg(string org);
        Task<VehicleEntry> GetVehicleInfoById(int id);
        Task<bool> DeleteVehicleInfoById(int id);
        Task<string> GetVehicleNameById(int id);
    }
}
