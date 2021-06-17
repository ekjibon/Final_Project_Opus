using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Models;

namespace OPUSERP.VMS.Services.VehicleInfo.Interfaces
{
    public interface IVMSVehicleInfoService
    {
        #region Aspiration
        Task<int> SaveAspiration(Aspiration aspiration);
        Task<IEnumerable<Aspiration>> GetAspiration();
        Task<Aspiration> GetAspirationById(int id);
        Task<bool> DeleteAspirationById(int id);
        #endregion

        #region Block Type
        Task<int> SaveBlockType(BlockType blockType);
        Task<IEnumerable<BlockType>> GetBlockType();
        Task<BlockType> GetBlockTypeById(int id);
        Task<bool> DeleteBlockTypeById(int id);
        #endregion

        #region Brake System
        Task<int> SaveBrakeSystem(BrakeSystem brakeSystem);
        Task<IEnumerable<BrakeSystem>> GetBrakeSystem();
        Task<BrakeSystem> GetBrakeSystemById(int id);
        Task<bool> DeleteBrakeSystemById(int id);
        #endregion

        #region CamType
        Task<int> SaveCamType(CamType camType);
        Task<IEnumerable<CamType>> GetCamType();
        Task<CamType> GetCamTypeById(int id);
        Task<bool> DeleteCamTypeById(int id);
        #endregion

        #region Document Attachment
        Task<int> SaveDocumentAttachment(DocumentPhotoAttachment documentAttachment);
        Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentByActionId(int vid,string actionType,string documentType);
        Task<DocumentPhotoAttachment> GetDocumentAttachmentById(int id);
        Task<bool> DeleteDocumentAttachmentById(int id);
        #endregion

        #region Drive Type
        Task<int> SaveDriveType(DriveType driveType);
        Task<IEnumerable<DriveType>> GetDriveType();
        Task<DriveType> GetDriveTypeById(int id);
        Task<bool> DeleteDriveTypeById(int id);
        #endregion

        #region Fuel Induction
        Task<int> SaveFuelInduction(FuelInduction fuelInduction);
        Task<IEnumerable<FuelInduction>> GetFuelInduction();
        Task<FuelInduction> GetFuelInductionById(int id);
        Task<bool> DeleteFuelInductionById(int id);
        #endregion

        #region Fuel Type
        Task<int> SaveFuelType(FuelType fuelType);
        Task<IEnumerable<FuelType>> GetFuelType();
        Task<FuelType> GetFuelTypeById(int id);
        Task<bool> DeleteFuelTypeById(int id);
        #endregion

        #region Linked Vehicle
        Task<int> SaveLinkedVehicle(LinkedVehicle linkedVehicle);
        Task<IEnumerable<LinkedVehicle>> GetLinkedVehicle();
        Task<LinkedVehicle> GetLinkedVehicleById(int id);
        Task<bool> DeleteLinkedVehicleById(int id);
        #endregion

        #region Odometer
        Task<int> SaveOdometer(Odometer odometer);
        Task<IEnumerable<Odometer>> GetOdometer();
        Task<Odometer> GetOdometerByVehicleId(int vid);
        Task<Odometer> GetOdometerBySourceTypeId(int vid, string sourceType, int soruceId);
        Task<Odometer> GetOdometerById(int id);
        Task<bool> DeleteOdometerById(int id);
        #endregion

        #region Transmission Type
        Task<int> SaveTransmissionType(TransmissionType transmissionType);
        Task<IEnumerable<TransmissionType>> GetTransmissionType();
        Task<TransmissionType> GetTransmissionTypeById(int id);
    
        Task<bool> DeleteTransmissionTypeById(int id);
        #endregion

        #region Vehicle Body Sub Type
        Task<int> SaveVehicleBodySubType(VehicleBodySubType vehicleBodySubType);
        Task<IEnumerable<VehicleBodySubType>> GetVehicleBodySubType();
        Task<VehicleBodySubType> GetVehicleBodySubTypeById(int id);
        Task<bool> DeleteVehicleBodySubTypeById(int id);
        #endregion

        #region Vehicle Body Type
        Task<int> SaveVehicleBodyType(VehicleBodyType vehicleBodyType);
        Task<IEnumerable<VehicleBodyType>> GetVehicleBodyType();
        Task<VehicleBodyType> GetVehicleBodyTypeById(int id);
        Task<bool> DeleteVehicleBodyTypeById(int id);
        #endregion

        #region Vehicle Comment
        Task<int> SaveVehicleComment(VehicleComment vehicleComment);
        Task<IEnumerable<VehicleComment>> GetCommentByVehicleId(int vid);
        Task<VehicleComment> GetVehicleCommentById(int id);
        Task<bool> DeleteVehicleCommentById(int id);
        #endregion

        #region Vehicle Engine Transmission
        Task<int> SaveVehicleEngineTransmission(VehicleEngineTransmission vehicleEngineTransmission);
        Task<IEnumerable<VehicleEngineTransmission>> GetVehicleEngineTransmission();
        Task<VehicleEngineTransmission> GetVehicleEngineTransmissionById(int id);
        Task<VehicleEngineTransmission> GetVehicleEngineTransmissionByInfoId(int id);
        Task<bool> DeleteVehicleEngineTransmissionById(int id);
        #endregion

        #region Vehicle Fluid
        Task<int> SaveVehicleFluid(VehicleFluid vehicleFluid);
        Task<IEnumerable<VehicleFluid>> GetVehicleFluid();
        Task<VehicleFluid> GetVehicleFluidById(int id);
        Task<IEnumerable<VehicleFluid>> GetVehicleFluidByInfoId(int id); 
         Task<bool> DeleteVehicleFluidById(int id);
        Task<bool> DeleteVehicleFluidByInfoId(int id);
        #endregion

        #region Vehicle Group
        Task<int> SaveVehicleGroup(VehicleGroup vehicleGroup);
        Task<IEnumerable<VehicleGroup>> GetVehicleGroup();
        Task<VehicleGroup> GetVehicleGroupById(int id);
        Task<bool> DeleteVehicleGroupById(int id);
        #endregion

        #region Vehicle Information
        Task<int> SaveVehicleInformation(VehicleInformation vehicleInformation);
        Task<IEnumerable<VehicleInformation>> GetVehicleInformation();
        Task<IEnumerable<VehicleInformation>> GetFullVehicleInformation();
        Task<IEnumerable<VehicleInformation>> GetFullVehicleInformationWithStatus(int statusId);
        Task<VehicleInformation> GetVehicleInformationById(int id);
        Task<VehicleInformation> GetVehicleInformationDetailsById(int id);
        Task<bool> DeleteVehicleInformationById(int id);
        #endregion

        #region Vehicle Manufacture
        Task<int> SaveVehicleManufacture(VehicleManufacture vehicleManufacture);
        Task<IEnumerable<VehicleManufacture>> GetVehicleManufacture();
        Task<VehicleManufacture> GetVehicleManufactureById(int id);
        Task<bool> DeleteVehicleManufactureById(int id);
        #endregion

        #region Vehicle Model
        Task<int> SaveVehicleModel(VehicleModel vehicleModel);
        Task<IEnumerable<VehicleModel>> GetVehicleModel();
        Task<VehicleModel> GetVehicleModelById(int id);
        Task<bool> DeleteVehicleModelById(int id);
        #endregion

        #region Vehicle Ownership
        Task<int> SaveVehicleOwnership(VehicleOwnership vehicleOwnership);
        Task<IEnumerable<VehicleOwnership>> GetVehicleOwnership();
        Task<VehicleOwnership> GetVehicleOwnershipById(int id);
        Task<bool> DeleteVehicleOwnershipById(int id);
        #endregion

        #region Vehicle Specification
        Task<int> SaveVehicleSpecification(VehicleSpecification vehicleSpecification);
        Task<IEnumerable<VehicleSpecification>> GetVehicleSpecification();
        Task<VehicleSpecification> GetVehicleSpecificationById(int id);
        Task<VehicleSpecification> GetVehicleSpecificationByInfoId(int id);
        Task<bool> DeleteVehicleSpecificationById(int id);
        #endregion

        #region Vehicle Status
        Task<int> SaveVehicleStatus(VehicleStatus vehicleStatus);
        Task<IEnumerable<VehicleStatus>> GetVehicleStatus();
        Task<VehicleStatus> GetVehicleStatusById(int id);
        Task<bool> DeleteVehicleStatusById(int id);
        void UpdateVehicleStatusVehicleById(int vehicleId, int statusId);
        #endregion

        #region Vehicle Type
        Task<int> SaveVehicleType(VehicleType vehicleType);
        Task<IEnumerable<VehicleType>> GetVehicleType();
        Task<VehicleType> GetVehicleTypeById(int id);
        Task<bool> DeleteVehicleTypeById(int id);
        #endregion

        #region Vehicle Wheel Tire
        Task<int> SaveVehicleWheelTire(VehicleWheelTire vehicleWheelTire);
        Task<IEnumerable<VehicleWheelTire>> GetVehicleWheelTire();
        Task<VehicleWheelTire> GetVehicleWheelTireById(int id);
        Task<VehicleWheelTire> GetVehicleWheelTireByInfoId(int id);
        Task<bool> DeleteVehicleWheelTireById(int id);
        #endregion

        #region Dashboard
        Task<IEnumerable<VehicleStatus>> GetVehicleStatusByUser(string userName);
        Task<DashboardDataModel> GetVehicleIssueByUser(string userName);
        Task<DashboardDataModel> GetVehicleServiceReminderByUser(string userName);
        Task<IEnumerable<ChartViewModel>> GetLetestMeterReadingByUser(string userName);
        Task<IEnumerable<ChartViewModel>> GetFuelCostByUser(string userName);
        Task<IEnumerable<ChartViewModel>> GetCostPerMeterByUser(string userName);
        IQueryable<ChartViewModel> GetTotalCostByUser(string userName);
        Task<IEnumerable<CommentsViewModel>> GetLetestCommentsByUser(string userName);
        #endregion
    }
}
