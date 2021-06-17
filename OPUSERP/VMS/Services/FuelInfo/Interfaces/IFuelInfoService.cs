using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Services.FuelInfo.Interfaces
{
    public interface IFuelInfoService
    {

      

        #region Fuel Information
        Task<int> SaveFuelInformation(FuelInformation fuelInformation);
        Task<IEnumerable<FuelInformation>> GetFuelInformation();
        Task<IEnumerable<FuelInfoReportViewModel>> GetFuelDetails(string filter); 
         Task<IEnumerable<FuelInformation>> GetFuelInformationByfuelId(int fuelId);
        Task<FuelInformation> GetFuelInformationById(int id);
        Task<bool> DeleteFuelInformationById(int id);
        #endregion

        #region Fuel Details
        Task<int> SaveFuelDetail(FuelDetail fuelDetail);
        Task<IEnumerable<FuelDetail>> GetFuelDetailByfuelId(int fuelId);
        Task<FuelDetail> GetFuelDetailById(int id);
        Task<bool> DeleteFuelDetailById(int id);
        Task<bool> DeleteFuelDetailByfuelId(int fuelId);
        
        #endregion

        #region Fuel Comment
        Task<int> SaveFuelComment(FuelComment fuelComment);
        Task<IEnumerable<FuelComment>> GetFuelComment();
        Task<IEnumerable<FuelComment>> GetFuelCommentByfuelId(int fuelId);
        Task<FuelComment> GetFuelCommentById(int id);
        Task<bool> DeleteFuelCommentById(int id);
        #endregion
    }
}
