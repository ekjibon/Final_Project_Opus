using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Services.FuelInfo.Interfaces;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.FuelInfo
{
    public class FuelInfoService: IFuelInfoService
    {
        private readonly ERPDbContext _context;

        public FuelInfoService(ERPDbContext context)
        {
            _context = context;
        }

       

        #region Fuel Information
        public async Task<int> SaveFuelInformation(FuelInformation fuelInformation)
        {
            if (fuelInformation.Id != 0)
            {
                fuelInformation.updatedBy = fuelInformation.createdBy;
                fuelInformation.updatedAt = DateTime.Now;
                _context.FuelInformations.Update(fuelInformation);
            }
            else
            {
                fuelInformation.createdAt = DateTime.Now;
                _context.FuelInformations.Add(fuelInformation);
            }

            await _context.SaveChangesAsync();
            return fuelInformation.Id;
        }

        public async Task<IEnumerable<FuelInformation>> GetFuelInformation()
        {
            return await _context.FuelInformations.AsNoTracking().Include(x=>x.fuelStationInfo).Include(x=>x.vehicleInformation).ToListAsync();
        }
        public async Task<IEnumerable<FuelInformation>> GetFuelInformationByfuelId(int fuelId)
        {
            return await _context.FuelInformations.AsNoTracking().Include(x => x.fuelStationInfo).Include(x => x.vehicleInformation).Where(x=>x.Id== fuelId).ToListAsync();
        }

        public async Task<FuelInformation> GetFuelInformationById(int id)
        {
            return await _context.FuelInformations.FindAsync(id);
        }

        public async Task<bool> DeleteFuelInformationById(int id)
        {
            _context.FuelInformations.Remove(_context.FuelInformations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Fuel Details
        public async Task<int> SaveFuelDetail(FuelDetail fuelDetail)
        {
            if (fuelDetail.Id != 0)
            {
                fuelDetail.updatedBy = fuelDetail.createdBy;
                fuelDetail.updatedAt = DateTime.Now;
                _context.FuelDetails.Update(fuelDetail);
            }
            else
            {
                fuelDetail.createdAt = DateTime.Now;
                _context.FuelDetails.Add(fuelDetail);
            }

            await _context.SaveChangesAsync();
            return fuelDetail.Id;
        }

        public async Task<IEnumerable<FuelDetail>> GetFuelDetailByfuelId(int fuelId)
        {
            return await _context.FuelDetails.AsNoTracking().Include(x=>x.fuelType).Where(x=>x.fuelInformationId== fuelId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<FuelInfoReportViewModel>> GetFuelDetails(string filter)
        {
          
            var result = (from fd in _context.FuelDetails
                          join fi in _context.FuelInformations on fd.fuelInformationId equals fi.Id
                          join fs in _context.FuelStationInfos on fi.fuelStationInfoId equals fs.Id
                          join vh in _context.VehicleInformations on fi.vehicleInformationId equals vh.Id
                          join ft in _context.FuelTypes on fd.fuelTypeId equals ft.Id
                      
                          select new FuelInfoReportViewModel
                          {
                              vehicleName = vh.vehicleName,
                              referenceNo = fi.referenceNo,
                              fuelStation =fs.fuelStationName,
                              date = fi.fuelTakenDate,
                              fuelType=ft.fuelTypeName,
                              unit=ft.unit,
                              quantity=fd.quantity,
                              rate=fd.rate,
                              total=fd.rate*fd.quantity,
                              stationId=fs.Id,
                              fuelTypeId=ft.Id,
                              vehicleId=vh.Id


                          }).ToListAsync();

          
            

            return await result;
        }

        public async Task<FuelDetail> GetFuelDetailById(int id)
        {
            return await _context.FuelDetails.FindAsync();
        }

        public async Task<bool> DeleteFuelDetailById(int id)
        {
            _context.FuelDetails.Remove(_context.FuelDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteFuelDetailByfuelId(int fuelId)
        {
            _context.FuelDetails.RemoveRange(_context.FuelDetails.Where(x => x.fuelInformationId == fuelId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Fuel Comment
        public async Task<int> SaveFuelComment(FuelComment fuelComment)
        {
            if (fuelComment.Id != 0)
            {
                fuelComment.updatedBy = fuelComment.createdBy;
                fuelComment.updatedAt = DateTime.Now;
                _context.FuelComments.Update(fuelComment);
            }
            else
            {
                fuelComment.createdAt = DateTime.Now;
                _context.FuelComments.Add(fuelComment);
            }

            await _context.SaveChangesAsync();
            return fuelComment.Id;
        }

        public async Task<IEnumerable<FuelComment>> GetFuelComment()
        {
            return await _context.FuelComments.AsNoTracking().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<FuelComment>> GetFuelCommentByfuelId(int fuelId)
        {
            return await _context.FuelComments.AsNoTracking().Where(x=>x.fuelInformationId == fuelId).ToListAsync();
        }

        public async Task<FuelComment> GetFuelCommentById(int id)
        {
            return await _context.FuelComments.FindAsync();
        }

        public async Task<bool> DeleteFuelCommentById(int id)
        {
            _context.FuelComments.Remove(_context.FuelComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
