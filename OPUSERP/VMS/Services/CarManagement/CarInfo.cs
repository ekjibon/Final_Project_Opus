using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.CarManagement
{
    public class CarInfo:ICarInfo
    {
        private readonly ERPDbContext _context;

        public CarInfo(ERPDbContext context)
        {
            _context = context;
        }

        #region Car Info
        public async Task<IEnumerable<SourceType>> GetSourceType()
        {
            return await _context.SourceTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveVehicleInfo(VehicleInformation vehicleInformation)
        {
            if (vehicleInformation.Id != 0)
            {
                vehicleInformation.updatedAt = DateTime.Now;
                vehicleInformation.updatedBy = vehicleInformation.createdBy;
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

        public async Task<IEnumerable<VehicleInformation>> GetVehicleInformation()
        {
            return await _context.VehicleInformations.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VehicleInformation>> GetVehicleInformationBySourceTypeID(int sourceTypeId)
        {
            return await _context.VehicleInformations.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VehicleInformation> GetVehicleInformationById(int id)
        {
            return await _context.VehicleInformations.FindAsync();
        }

        public async Task<bool> DeleteVehicleInfoById(int id)
        {
            _context.VehicleInformations.Remove(_context.VehicleInformations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Spare Parts
        public async Task<int> SaveSpareParts(SpareParts spareParts)
        {
            if (spareParts.Id != 0)
            {
                spareParts.updatedBy = spareParts.createdBy;
                spareParts.updatedAt = DateTime.Now;
                _context.SpareParts.Update(spareParts);
            }
            else
            {
                spareParts.createdAt = DateTime.Now;
                _context.SpareParts.Add(spareParts);
            }
                
            await _context.SaveChangesAsync();
            return spareParts.Id;
        }

        public async Task<IEnumerable<SpareParts>> GetSpareParts()
        {
            return await _context.SpareParts.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<SpareParts> GetSparePartsById(int id)
        {
            return await _context.SpareParts.FindAsync();
        }

        public async Task<bool> DeleteSparePartsById(int id)
        {
            _context.SpareParts.Remove(_context.SpareParts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
