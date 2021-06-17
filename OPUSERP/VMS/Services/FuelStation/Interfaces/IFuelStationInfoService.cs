using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Services.FuelStation.Interfaces
{
    public interface IFuelStationInfoService
    {
        #region Fuel Station
        Task<int> SaveFuelStationInfo(FuelStationInfo aspiration);
        Task<IEnumerable<FuelStationInfo>> GetFuelStationInfo();
        Task<FuelStationInfo> GetFuelStationInfoById(int id);
        Task<bool> DeleteFuelStationInfosById(int id);
        #endregion
        #region Contacts
        Task<int> Savecontact(VMSContact contact);
        Task<IEnumerable<VMSContact>> Getcontact();
        Task<VMSContact> GetcontactById(int id);
        Task<bool> DeletecontactById(int id);
        Task<IEnumerable<System.Object>> Getcontactbystationid(int Id);
        #endregion
        #region sationfuelinfo
        Task<int> SavestationFuelInfo(StationFuelInfo stationFuelInfo);
        Task<IEnumerable<StationFuelInfo>> GetStationFuelInfo();
        Task<IEnumerable<StationFuelInfo>> GetStationFuelInfobystationId(int Id);
        Task<StationFuelInfo> GetStationFuelInfoById(int id);
        Task<bool> DeleteStationFuelInfoById(int id);
        Task<bool> DeleteStationFuelInfoBystationId(int id);
        #endregion
        #region Fuel Station Comment
        Task<int> SaveFuelStationComment(FuelStationComment fuelStationComment);
        Task<IEnumerable<FuelStationComment>> GetCommentByFuelStationId(int vid);
        Task<FuelStationComment> GetFuelStationCommentById(int id);
        Task<bool> DeleteFuelStationCommentById(int id);
        #endregion


    }
}
