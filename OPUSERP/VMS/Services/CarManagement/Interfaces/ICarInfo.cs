using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Services.CarManagement.Interfaces
{
    public interface ICarInfo
    {
        #region Car Info 
        Task<IEnumerable<SourceType>> GetSourceType();
        Task<int> SaveVehicleInfo(VehicleInformation vehicleInformation);
        Task<IEnumerable<VehicleInformation>> GetVehicleInformation();
        Task<IEnumerable<VehicleInformation>> GetVehicleInformationBySourceTypeID(int sourceTypeId);
        Task<VehicleInformation> GetVehicleInformationById(int id);
        Task<bool> DeleteVehicleInfoById(int id);
        #endregion

        #region Spare parts
        Task<int> SaveSpareParts(SpareParts spareParts);
        Task<IEnumerable<SpareParts>> GetSpareParts();
        Task<SpareParts> GetSparePartsById(int id);
        Task<bool> DeleteSparePartsById(int id);

        #endregion
    }
}
