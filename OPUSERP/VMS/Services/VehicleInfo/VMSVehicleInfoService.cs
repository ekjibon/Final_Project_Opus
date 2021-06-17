using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Models;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.VMS.Services.VehicleInfo
{
    public class VMSVehicleInfoService : IVMSVehicleInfoService
    {
        private readonly ERPDbContext _context;

        public VMSVehicleInfoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Aspiration
        public async Task<int> SaveAspiration(Aspiration aspiration)
        {
            if (aspiration.Id != 0)
            {
                aspiration.updatedBy = aspiration.createdBy;
                aspiration.updatedAt = DateTime.Now;
                _context.Aspirations.Update(aspiration);
            }
            else
            {
                aspiration.createdAt = DateTime.Now;
                _context.Aspirations.Add(aspiration);
            }

            await _context.SaveChangesAsync();
            return aspiration.Id;
        }

        public async Task<IEnumerable<Aspiration>> GetAspiration()
        {
            return await _context.Aspirations.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<Aspiration> GetAspirationById(int id)
        {
            return await _context.Aspirations.FindAsync();
        }

        public async Task<bool> DeleteAspirationById(int id)
        {
            _context.Aspirations.Remove(_context.Aspirations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Block Type
        public async Task<int> SaveBlockType(BlockType blockType)
        {
            if (blockType.Id != 0)
            {
                blockType.updatedBy = blockType.createdBy;
                blockType.updatedAt = DateTime.Now;
                _context.BlockTypes.Update(blockType);
            }
            else
            {
                blockType.createdAt = DateTime.Now;
                _context.BlockTypes.Add(blockType);
            }

            await _context.SaveChangesAsync();
            return blockType.Id;
        }

        public async Task<IEnumerable<BlockType>> GetBlockType()
        {
            return await _context.BlockTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<BlockType> GetBlockTypeById(int id)
        {
            return await _context.BlockTypes.FindAsync();
        }

        public async Task<bool> DeleteBlockTypeById(int id)
        {
            _context.BlockTypes.Remove(_context.BlockTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Brake System
        public async Task<int> SaveBrakeSystem(BrakeSystem brakeSystem)
        {
            if (brakeSystem.Id != 0)
            {
                brakeSystem.updatedBy = brakeSystem.createdBy;
                brakeSystem.updatedAt = DateTime.Now;
                _context.BrakeSystems.Update(brakeSystem);
            }
            else
            {
                brakeSystem.createdAt = DateTime.Now;
                _context.BrakeSystems.Add(brakeSystem);
            }

            await _context.SaveChangesAsync();
            return brakeSystem.Id;
        }

        public async Task<IEnumerable<BrakeSystem>> GetBrakeSystem()
        {
            return await _context.BrakeSystems.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<BrakeSystem> GetBrakeSystemById(int id)
        {
            return await _context.BrakeSystems.FindAsync();
        }

        public async Task<bool> DeleteBrakeSystemById(int id)
        {
            _context.BrakeSystems.Remove(_context.BrakeSystems.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Cam Type
        public async Task<int> SaveCamType(CamType camType)
        {
            if (camType.Id != 0)
            {
                camType.updatedBy = camType.createdBy;
                camType.updatedAt = DateTime.Now;
                _context.CamTypes.Update(camType);
            }
            else
            {
                camType.createdAt = DateTime.Now;
                _context.CamTypes.Add(camType);
            }

            await _context.SaveChangesAsync();
            return camType.Id;
        }

        public async Task<IEnumerable<CamType>> GetCamType()
        {
            return await _context.CamTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<CamType> GetCamTypeById(int id)
        {
            return await _context.CamTypes.FindAsync();
        }

        public async Task<bool> DeleteCamTypeById(int id)
        {
            _context.CamTypes.Remove(_context.CamTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Document Attachment
        public async Task<int> SaveDocumentAttachment(DocumentPhotoAttachment documentAttachment)
        {
            if (documentAttachment.Id != 0)
            {
                documentAttachment.updatedBy = documentAttachment.createdBy;
                documentAttachment.updatedAt = DateTime.Now;
                _context.DocumentPhotoAttachments.Update(documentAttachment);
            }
            else
            {
                documentAttachment.createdAt = DateTime.Now;
                _context.DocumentPhotoAttachments.Add(documentAttachment);
            }

            await _context.SaveChangesAsync();
            return documentAttachment.Id;
        }

        public async Task<IEnumerable<DocumentPhotoAttachment>> GetDocumentAttachmentByActionId(int vid,string actionType,string documentType)
        {
            return await _context.DocumentPhotoAttachments.Where(x=>x.actionTypeId==vid && x.actionType==actionType && x.documentType==documentType).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<DocumentPhotoAttachment> GetDocumentAttachmentById(int id)
        {
            return await _context.DocumentPhotoAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocumentAttachmentById(int id)
        {
            _context.DocumentPhotoAttachments.Remove(_context.DocumentPhotoAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Drive Type
        public async Task<int> SaveDriveType(DriveType driveType)
        {
            if (driveType.Id != 0)
            {
                driveType.updatedBy = driveType.createdBy;
                driveType.updatedAt = DateTime.Now;
                _context.DriveTypes.Update(driveType);
            }
            else
            {
                driveType.createdAt = DateTime.Now;
                _context.DriveTypes.Add(driveType);
            }

            await _context.SaveChangesAsync();
            return driveType.Id;
        }

        public async Task<IEnumerable<DriveType>> GetDriveType()
        {
            return await _context.DriveTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<DriveType> GetDriveTypeById(int id)
        {
            return await _context.DriveTypes.FindAsync();
        }

        public async Task<bool> DeleteDriveTypeById(int id)
        {
            _context.DriveTypes.Remove(_context.DriveTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Fuel Induction
        public async Task<int> SaveFuelInduction(FuelInduction fuelInduction)
        {
            if (fuelInduction.Id != 0)
            {
                fuelInduction.updatedBy = fuelInduction.createdBy;
                fuelInduction.updatedAt = DateTime.Now;
                _context.FuelInductions.Update(fuelInduction);
            }
            else
            {
                fuelInduction.createdAt = DateTime.Now;
                _context.FuelInductions.Add(fuelInduction);
            }

            await _context.SaveChangesAsync();
            return fuelInduction.Id;
        }

        public async Task<IEnumerable<FuelInduction>> GetFuelInduction()
        {
            return await _context.FuelInductions.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<FuelInduction> GetFuelInductionById(int id)
        {
            return await _context.FuelInductions.FindAsync();
        }

        public async Task<bool> DeleteFuelInductionById(int id)
        {
            _context.FuelInductions.Remove(_context.FuelInductions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Fuel Type
        public async Task<int> SaveFuelType(FuelType fuelType)
        {
            if (fuelType.Id != 0)
            {
                fuelType.updatedBy = fuelType.createdBy;
                fuelType.updatedAt = DateTime.Now;
                _context.FuelTypes.Update(fuelType);
            }
            else
            {
                fuelType.createdAt = DateTime.Now;
                _context.FuelTypes.Add(fuelType);
            }

            await _context.SaveChangesAsync();
            return fuelType.Id;
        }

        public async Task<IEnumerable<FuelType>> GetFuelType()
        {
            return await _context.FuelTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<FuelType> GetFuelTypeById(int id)
        {
            return await _context.FuelTypes.FindAsync(id);
        }

        public async Task<bool> DeleteFuelTypeById(int id)
        {
            _context.FuelTypes.Remove(_context.FuelTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Linked Vehicle
        public async Task<int> SaveLinkedVehicle(LinkedVehicle linkedVehicle)
        {
            if (linkedVehicle.Id != 0)
            {
                linkedVehicle.updatedBy = linkedVehicle.createdBy;
                linkedVehicle.updatedAt = DateTime.Now;
                _context.LinkedVehicles.Update(linkedVehicle);
            }
            else
            {
                linkedVehicle.createdAt = DateTime.Now;
                _context.LinkedVehicles.Add(linkedVehicle);
            }

            await _context.SaveChangesAsync();
            return linkedVehicle.Id;
        }

        public async Task<IEnumerable<LinkedVehicle>> GetLinkedVehicle()
        {
            return await _context.LinkedVehicles.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<LinkedVehicle> GetLinkedVehicleById(int id)
        {
            return await _context.LinkedVehicles.FindAsync();
        }

        public async Task<bool> DeleteLinkedVehicleById(int id)
        {
            _context.LinkedVehicles.Remove(_context.LinkedVehicles.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Odometer
        public async Task<int> SaveOdometer(Odometer odometer)
        {
            try
            {
                if (odometer.Id != 0)
                {
                    odometer.updatedBy = odometer.createdBy;
                    odometer.updatedAt = DateTime.Now;
                    _context.Odometers.Update(odometer);
                }
                else
                {
                    odometer.createdAt = DateTime.Now;
                    _context.Odometers.Add(odometer);
                }

                await _context.SaveChangesAsync();
                return odometer.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<Odometer>> GetOdometer()
        {
            return await _context.Odometers.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<Odometer> GetOdometerByVehicleId(int vid)
        {
            return await _context.Odometers.Where(x=>x.vehicleInformationId==vid).LastOrDefaultAsync();
        }

        public async Task<Odometer> GetOdometerBySourceTypeId(int vid,string sourceType,int soruceId)
        {
            return await _context.Odometers.Where(x => x.vehicleInformationId == vid && x.sourceType==sourceType && x.sourceTypeId== soruceId).LastOrDefaultAsync();
        }

        public async Task<Odometer> GetOdometerById(int id)
        {
            return await _context.Odometers.FindAsync(id);
        }

        public async Task<bool> DeleteOdometerById(int id)
        {
            _context.Odometers.Remove(_context.Odometers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region TransmissionType
        public async Task<int> SaveTransmissionType(TransmissionType transmissionType)
        {
            if (transmissionType.Id != 0)
            {
                transmissionType.updatedBy = transmissionType.createdBy;
                transmissionType.updatedAt = DateTime.Now;
                _context.TransmissionTypes.Update(transmissionType);
            }
            else
            {
                transmissionType.createdAt = DateTime.Now;
                _context.TransmissionTypes.Add(transmissionType);
            }

            await _context.SaveChangesAsync();
            return transmissionType.Id;
        }

        public async Task<IEnumerable<TransmissionType>> GetTransmissionType()
        {
            return await _context.TransmissionTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<TransmissionType> GetTransmissionTypeById(int id)
        {
            return await _context.TransmissionTypes.FindAsync();
        }

        public async Task<bool> DeleteTransmissionTypeById(int id)
        {
            _context.TransmissionTypes.Remove(_context.TransmissionTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Body Sub Type
        public async Task<int> SaveVehicleBodySubType(VehicleBodySubType vehicleBodySubType)
        {
            if (vehicleBodySubType.Id != 0)
            {
                vehicleBodySubType.updatedBy = vehicleBodySubType.createdBy;
                vehicleBodySubType.updatedAt = DateTime.Now;
                _context.VehicleBodySubTypes.Update(vehicleBodySubType);
            }
            else
            {
                vehicleBodySubType.createdAt = DateTime.Now;
                _context.VehicleBodySubTypes.Add(vehicleBodySubType);
            }

            await _context.SaveChangesAsync();
            return vehicleBodySubType.Id;
        }

        public async Task<IEnumerable<VehicleBodySubType>> GetVehicleBodySubType()
        {
            return await _context.VehicleBodySubTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleBodySubType> GetVehicleBodySubTypeById(int id)
        {
            return await _context.VehicleBodySubTypes.FindAsync();
        }

        public async Task<bool> DeleteVehicleBodySubTypeById(int id)
        {
            _context.VehicleBodySubTypes.Remove(_context.VehicleBodySubTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Body Type
        public async Task<int> SaveVehicleBodyType(VehicleBodyType vehicleBodyType)
        {
            if (vehicleBodyType.Id != 0)
            {
                vehicleBodyType.updatedBy = vehicleBodyType.createdBy;
                vehicleBodyType.updatedAt = DateTime.Now;
                _context.VehicleBodyTypes.Update(vehicleBodyType);
            }
            else
            {
                vehicleBodyType.createdAt = DateTime.Now;
                _context.VehicleBodyTypes.Add(vehicleBodyType);
            }

            await _context.SaveChangesAsync();
            return vehicleBodyType.Id;
        }

        public async Task<IEnumerable<VehicleBodyType>> GetVehicleBodyType()
        {
            return await _context.VehicleBodyTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleBodyType> GetVehicleBodyTypeById(int id)
        {
            return await _context.VehicleBodyTypes.FindAsync();
        }

        public async Task<bool> DeleteVehicleBodyTypeById(int id)
        {
            _context.VehicleBodyTypes.Remove(_context.VehicleBodyTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Comment
        public async Task<int> SaveVehicleComment(VehicleComment vehicleComment)
        {
            if (vehicleComment.Id != 0)
            {
                vehicleComment.updatedBy = vehicleComment.createdBy;
                vehicleComment.updatedAt = DateTime.Now;
                _context.VehicleComments.Update(vehicleComment);
            }
            else
            {
                vehicleComment.createdAt = DateTime.Now;
                _context.VehicleComments.Add(vehicleComment);
            }

            await _context.SaveChangesAsync();
            return vehicleComment.Id;
        }

        public async Task<IEnumerable<VehicleComment>> GetCommentByVehicleId(int vid)
        {
            return await _context.VehicleComments.Where(x=>x.vehicleInformationId==vid).Include(x=>x.vehicleInformation).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleComment> GetVehicleCommentById(int id)
        {
            return await _context.VehicleComments.FindAsync();
        }

        public async Task<bool> DeleteVehicleCommentById(int id)
        {
            _context.VehicleComments.Remove(_context.VehicleComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Engine Transmission
        public async Task<int> SaveVehicleEngineTransmission(VehicleEngineTransmission vehicleEngineTransmission)
        {
            if (vehicleEngineTransmission.Id != 0)
            {
                vehicleEngineTransmission.updatedBy = vehicleEngineTransmission.createdBy;
                vehicleEngineTransmission.updatedAt = DateTime.Now;
                _context.VehicleEngineTransmissions.Update(vehicleEngineTransmission);
            }
            else
            {
                vehicleEngineTransmission.createdAt = DateTime.Now;
                _context.VehicleEngineTransmissions.Add(vehicleEngineTransmission);
            }

            await _context.SaveChangesAsync();
            return vehicleEngineTransmission.Id;
        }

        public async Task<IEnumerable<VehicleEngineTransmission>> GetVehicleEngineTransmission()
        {
            return await _context.VehicleEngineTransmissions.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleEngineTransmission> GetVehicleEngineTransmissionById(int id)
        {
            return await _context.VehicleEngineTransmissions.FindAsync(id);
        }
        public async Task<VehicleEngineTransmission> GetVehicleEngineTransmissionByInfoId(int id)
        {
            return await _context.VehicleEngineTransmissions.Where(x => x.vehicleInformationId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteVehicleEngineTransmissionById(int id)
        {
            _context.VehicleEngineTransmissions.Remove(_context.VehicleEngineTransmissions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Fluid
        public async Task<int> SaveVehicleFluid(VehicleFluid vehicleFluid)
        {
            if (vehicleFluid.Id != 0)
            {
                vehicleFluid.updatedBy = vehicleFluid.createdBy;
                vehicleFluid.updatedAt = DateTime.Now;
                _context.VehicleFluids.Update(vehicleFluid);
            }
            else
            {
                vehicleFluid.createdAt = DateTime.Now;
                _context.VehicleFluids.Add(vehicleFluid);
            }

            await _context.SaveChangesAsync();
            return vehicleFluid.Id;
        }

        public async Task<IEnumerable<VehicleFluid>> GetVehicleFluid()
        {
            return await _context.VehicleFluids.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleFluid> GetVehicleFluidById(int id)
        {
            return await _context.VehicleFluids.FindAsync(id);
        }
        public async Task<IEnumerable<VehicleFluid>> GetVehicleFluidByInfoId(int id)
        {
            return await _context.VehicleFluids.Include(x=>x.fuelType).Where(x=>x.vehicleInformationId==id).AsNoTracking().ToListAsync();
        }
      
        public async Task<bool> DeleteVehicleFluidById(int id)
        {
            _context.VehicleFluids.Remove(_context.VehicleFluids.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteVehicleFluidByInfoId(int id)
        {
            _context.VehicleFluids.RemoveRange(_context.VehicleFluids.Where(x=>x.vehicleInformationId==id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Group
        public async Task<int> SaveVehicleGroup(VehicleGroup vehicleGroup)
        {
            if (vehicleGroup.Id != 0)
            {
                vehicleGroup.updatedBy = vehicleGroup.createdBy;
                vehicleGroup.updatedAt = DateTime.Now;
                _context.VehicleGroups.Update(vehicleGroup);
            }
            else
            {
                vehicleGroup.createdAt = DateTime.Now;
                _context.VehicleGroups.Add(vehicleGroup);
            }

            await _context.SaveChangesAsync();
            return vehicleGroup.Id;
        }

        public async Task<IEnumerable<VehicleGroup>> GetVehicleGroup()
        {
            return await _context.VehicleGroups.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleGroup> GetVehicleGroupById(int id)
        {
            return await _context.VehicleGroups.FindAsync();
        }

        public async Task<bool> DeleteVehicleGroupById(int id)
        {
            _context.VehicleGroups.Remove(_context.VehicleGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Information
        public async Task<int> SaveVehicleInformation(VehicleInformation vehicleInformation)
        {
            try
            {
                if (vehicleInformation.Id != 0)
                {
                    vehicleInformation.updatedBy = vehicleInformation.createdBy;
                    vehicleInformation.updatedAt = DateTime.Now;
                    _context.VehicleInformations.Update(vehicleInformation);
                }
                else
                {
                    vehicleInformation.createdAt = DateTime.Now;
                    _context.VehicleInformations.Add(vehicleInformation);
                }

                await _context.SaveChangesAsync();
                return vehicleInformation.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<VehicleInformation>> GetVehicleInformation()
        {
            return await _context.VehicleInformations.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VehicleInformation>> GetFullVehicleInformation()
        {
            //var result =await (from V in _context.VehicleInformations
            //             join S in _context.VehicleStatuses on V.vehicleStatusId equals S.Id into VS
            //                   from C in VS.DefaultIfEmpty()
            //                   join T in _context.VehicleTypes on V.vehicleTypeId equals T.Id into VT
            //                   from B in VT.DefaultIfEmpty()
            //                   join G in _context.VehicleGroups on V.vehicleGroupId equals G.Id into VG
            //                   from A in VG.DefaultIfEmpty()


            //             select new VehicleInformation
            //             {
            //                 Id=V.Id,
            //                 vehicleName=V.vehicleName,
            //                 licensePlate=V.licensePlate,
            //                 operatorId=V.operatorId,
            //                 vehicleStatus=V.vehicleStatus,
            //                 vehicleType=V.vehicleType,
            //                 vehicleGroup=(V.vehicleGroup==null)? V.vehicleGroup:new VehicleGroup()
            //             }).AsNoTracking().ToListAsync();
            //return result;
            return await _context.VehicleInformations
                .Include(x => x.vehicleStatus)
                .Include(x => x.vehicleType)
                .Include(x => x.vehicleGroup)
                .OrderByDescending(x=>x.Id).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VehicleInformation>> GetFullVehicleInformationWithStatus(int statusId)
        {
            return await _context.VehicleInformations.Where(x=>x.vehicleStatusId==statusId)
                .Include(x => x.vehicleStatus)
                .Include(x => x.vehicleType)
                .Include(x => x.vehicleGroup)
                .AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleInformation> GetVehicleInformationDetailsById(int id)
        {
            try
            {
                var record = await _context.VehicleInformations
                    .Include(x=>x.vehicleStatus)
                    .Include(x=>x.vehicleType)
                    .Include(x=>x.vehicleManufacture)
                    .Include(x => x.vehicleModel)
                    .Include(x => x.vehicleGroup)
                    .Include(x => x.vehicleBodyType)
                    .Include(x => x.vehicleBodySubType)
                    .Include(x => x.vehicleOwnership)
                    .Where(x=>x.Id==id).FirstOrDefaultAsync();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VehicleInformation> GetVehicleInformationById(int id)
        {
            try
            {
                var record = await _context.VehicleInformations.FindAsync(id);
                return record;
            }
            catch (Exception ex) {
                throw ex;

            }
        }

        public async Task<bool> DeleteVehicleInformationById(int id)
        {
            _context.VehicleInformations.Remove(_context.VehicleInformations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Manufacture
        public async Task<int> SaveVehicleManufacture(VehicleManufacture vehicleManufacture)
        {
            if (vehicleManufacture.Id != 0)
            {
                vehicleManufacture.updatedBy = vehicleManufacture.createdBy;
                vehicleManufacture.updatedAt = DateTime.Now;
                _context.VehicleManufactures.Update(vehicleManufacture);
            }
            else
            {
                vehicleManufacture.createdAt = DateTime.Now;
                _context.VehicleManufactures.Add(vehicleManufacture);
            }

            await _context.SaveChangesAsync();
            return vehicleManufacture.Id;
        }

        public async Task<IEnumerable<VehicleManufacture>> GetVehicleManufacture()
        {
            return await _context.VehicleManufactures.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleManufacture> GetVehicleManufactureById(int id)
        {
            return await _context.VehicleManufactures.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleManufactureById(int id)
        {
            _context.VehicleManufactures.Remove(_context.VehicleManufactures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region VehicleModel
        public async Task<int> SaveVehicleModel(VehicleModel vehicleModel)
        {
            if (vehicleModel.Id != 0)
            {
                vehicleModel.updatedBy = vehicleModel.createdBy;
                vehicleModel.updatedAt = DateTime.Now;
                _context.VehicleModels.Update(vehicleModel);
            }
            else
            {
                vehicleModel.createdAt = DateTime.Now;
                _context.VehicleModels.Add(vehicleModel);
            }

            await _context.SaveChangesAsync();
            return vehicleModel.Id;
        }

        public async Task<IEnumerable<VehicleModel>> GetVehicleModel()
        {
            return await _context.VehicleModels.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleModel> GetVehicleModelById(int id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleModelById(int id)
        {
            _context.VehicleModels.Remove(_context.VehicleModels.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Ownership
        public async Task<int> SaveVehicleOwnership(VehicleOwnership vehicleOwnership)
        {
            if (vehicleOwnership.Id != 0)
            {
                vehicleOwnership.updatedBy = vehicleOwnership.createdBy;
                vehicleOwnership.updatedAt = DateTime.Now;
                _context.VehicleOwnerships.Update(vehicleOwnership);
            }
            else
            {
                vehicleOwnership.createdAt = DateTime.Now;
                _context.VehicleOwnerships.Add(vehicleOwnership);
            }

            await _context.SaveChangesAsync();
            return vehicleOwnership.Id;
        }

        public async Task<IEnumerable<VehicleOwnership>> GetVehicleOwnership()
        {
            return await _context.VehicleOwnerships.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleOwnership> GetVehicleOwnershipById(int id)
        {
            return await _context.VehicleOwnerships.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleOwnershipById(int id)
        {
            _context.VehicleOwnerships.Remove(_context.VehicleOwnerships.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Specification
        public async Task<int> SaveVehicleSpecification(VehicleSpecification vehicleSpecification)
        {
            if (vehicleSpecification.Id != 0)
            {
                vehicleSpecification.updatedBy = vehicleSpecification.createdBy;
                vehicleSpecification.updatedAt = DateTime.Now;
                _context.VehicleSpecifications.Update(vehicleSpecification);
            }
            else
            {
                vehicleSpecification.createdAt = DateTime.Now;
                _context.VehicleSpecifications.Add(vehicleSpecification);
            }

            await _context.SaveChangesAsync();
            return vehicleSpecification.Id;
        }

        public async Task<IEnumerable<VehicleSpecification>> GetVehicleSpecification()
        {
            return await _context.VehicleSpecifications.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleSpecification> GetVehicleSpecificationById(int id)
        {
            return await _context.VehicleSpecifications.FindAsync(id);
        }
        public async Task<VehicleSpecification> GetVehicleSpecificationByInfoId(int id)
        {
            return await _context.VehicleSpecifications.Where(x=>x.vehicleInformationId==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteVehicleSpecificationById(int id)
        {
            _context.VehicleSpecifications.Remove(_context.VehicleSpecifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Status
        public async Task<int> SaveVehicleStatus(VehicleStatus vehicleStatus)
        {
            if (vehicleStatus.Id != 0)
            {
                vehicleStatus.updatedBy = vehicleStatus.createdBy;
                vehicleStatus.updatedAt = DateTime.Now;
                _context.VehicleStatuses.Update(vehicleStatus);
            }
            else
            {
                vehicleStatus.createdAt = DateTime.Now;
                _context.VehicleStatuses.Add(vehicleStatus);
            }

            await _context.SaveChangesAsync();
            return vehicleStatus.Id;
        }

        public async Task<IEnumerable<VehicleStatus>> GetVehicleStatus()
        {
            return await _context.VehicleStatuses.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleStatus> GetVehicleStatusById(int id)
        {
            return await _context.VehicleStatuses.FindAsync(id);
        }

       
        public async Task<bool> DeleteVehicleStatusById(int id)
        {
            _context.VehicleStatuses.Remove(_context.VehicleStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateVehicleStatusVehicleById(int vehicleId, int statusId)
        {
            var vehicleStatus = new VehicleInformation() { Id = vehicleId, vehicleStatusId = statusId };
            _context.VehicleInformations.Attach(vehicleStatus);
            _context.Entry(vehicleStatus).Property(x => x.vehicleStatusId).IsModified = true;

            _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Type
        public async Task<int> SaveVehicleType(VehicleType vehicleType)
        {
            if (vehicleType.Id != 0)
            {
                vehicleType.updatedBy = vehicleType.createdBy;
                vehicleType.updatedAt = DateTime.Now;
                _context.VehicleTypes.Update(vehicleType);
            }
            else
            {
                vehicleType.createdAt = DateTime.Now;
                _context.VehicleTypes.Add(vehicleType);
            }

            await _context.SaveChangesAsync();
            return vehicleType.Id;
        }

        public async Task<IEnumerable<VehicleType>> GetVehicleType()
        {
            try
            {
                var record= await _context.VehicleTypes.AsNoTracking().AsNoTracking().ToListAsync();
                return record;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<VehicleType> GetVehicleTypeById(int id)
        {
            return await _context.VehicleTypes.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleTypeById(int id)
        {
            _context.VehicleTypes.Remove(_context.VehicleTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Wheel Tire
        public async Task<int> SaveVehicleWheelTire(VehicleWheelTire vehicleWheelTire)
        {
            if (vehicleWheelTire.Id != 0)
            {
                vehicleWheelTire.updatedBy = vehicleWheelTire.createdBy;
                vehicleWheelTire.updatedAt = DateTime.Now;
                _context.VehicleWheelTires.Update(vehicleWheelTire);
            }
            else
            {
                vehicleWheelTire.createdAt = DateTime.Now;
                _context.VehicleWheelTires.Add(vehicleWheelTire);
            }

            await _context.SaveChangesAsync();
            return vehicleWheelTire.Id;
        }

        public async Task<IEnumerable<VehicleWheelTire>> GetVehicleWheelTire()
        {
            return await _context.VehicleWheelTires.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleWheelTire> GetVehicleWheelTireById(int id)
        {
            return await _context.VehicleWheelTires.FindAsync(id);
        }
        public async Task<VehicleWheelTire> GetVehicleWheelTireByInfoId(int id)
        {
            return await _context.VehicleWheelTires.Where(x => x.vehicleInformationId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteVehicleWheelTireById(int id)
        {
            _context.VehicleWheelTires.Remove(_context.VehicleWheelTires.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region Dashboard

        public async Task<IEnumerable<VehicleStatus>> GetVehicleStatusByUser(string userName)
        {
            var vehicleStatus = await (from VS in _context.VehicleStatuses
                                       join VI in _context.VehicleInformations on VS.Id equals VI.vehicleStatusId
                                       where VI.createdBy == userName
                                       group new { VS, VI } by new { VS.Id, VS.vehicleStatusName, VS.colorCode } into g
                                       select new VehicleStatus
                                       {
                                           Id = g.Key.Id,
                                           vehicleStatusName = g.Key.vehicleStatusName,
                                           colorCode = g.Key.colorCode,
                                           count = g.Count()
                                       }).AsNoTracking().ToListAsync();

            return vehicleStatus;
        }

        public async Task<DashboardDataModel> GetVehicleIssueByUser(string userName)
        {
            int OpenIssue = await _context.VehicleServiceIssues.Where(x => x.dueDate < DateTime.Now && x.createdBy==userName).CountAsync();
            int OverDue = await _context.VehicleServiceIssues.Where(x => x.dueDate > DateTime.Now && x.createdBy == userName).CountAsync();
            DashboardDataModel model = new DashboardDataModel
            {
                value1 = OpenIssue,
                value2 = OverDue
            };
            return  model;
        }

        public async Task<DashboardDataModel> GetVehicleServiceReminderByUser(string userName)
        {
            int DueSoon = await _context.VehicleServiceReminders.Where(x => x.createdAt < DateTime.Now && x.createdBy == userName).CountAsync();
            int OverDue = await _context.VehicleServiceReminders.Where(x => x.createdAt > DateTime.Now && x.createdBy == userName).CountAsync();
            DashboardDataModel model = new DashboardDataModel
            {
                value1 = DueSoon,
                value2 = OverDue
            };
            return model;
        }

        public async Task<IEnumerable<ChartViewModel>> GetLetestMeterReadingByUser(string userName)
        {
            return await _context.chartViewModels.FromSql($"SP_GETLetestMeterReading {userName}").ToListAsync();
        }

        public async Task<IEnumerable<ChartViewModel>> GetFuelCostByUser(string userName)
        {
            return await _context.chartViewModels.FromSql($"SP_GETFuelCost {userName}").ToListAsync();
        }

        public async Task<IEnumerable<ChartViewModel>> GetCostPerMeterByUser(string userName)
        {
            return await _context.chartViewModels.FromSql($"SP_GETCostPerMeter {userName}").ToListAsync();
        }

        public IQueryable<ChartViewModel> GetTotalCostByUser(string userName)
        {
            return _context.chartViewModels.FromSql($"SP_GETTotalCost {userName}");
        }

        public async Task<IEnumerable<CommentsViewModel>> GetLetestCommentsByUser(string userName)
        {
            return await _context.commentsViewModels.FromSql($"SP_GetRecentComments {userName}").ToListAsync();
        }

        #endregion

    }
}
