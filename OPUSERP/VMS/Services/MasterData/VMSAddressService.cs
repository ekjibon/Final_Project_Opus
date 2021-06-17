
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.VMS.Services.MasterData
{
    public class VMSAddressService : IVMSAddressService
    {
        private readonly ERPDbContext _context;

        public VMSAddressService(ERPDbContext context)
        {
            _context = context;
        }

        #region Country
        public async Task<IEnumerable<Country>> GetAllContry()
        {
            return await _context.Countries.AsNoTracking().ToListAsync();
        }

        public async Task<Country> GetContryById(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<bool> SaveCountry(Country country)
        {
            _context.Countries.Add(country);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteContryById(int id)
        {
            _context.Countries.Remove(_context.Countries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Division
        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            return await _context.Divisions.AsNoTracking().ToListAsync();
        }

        public async Task<Division> GetDivisionById(int id)
        {
            return await _context.Divisions.FindAsync(id);
        }

        public async Task<bool> SaveDivision(Division division)
        {
            _context.Divisions.Add(division);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDivisionById(int id)
        {
            _context.Divisions.Remove(_context.Divisions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            return await _context.Divisions.Where(X => X.countryId == CntId).ToListAsync();
        }
        #endregion

        #region District
        public async Task<IEnumerable<District>> GetAllDistrict()
        {
            return await _context.Districts.Include(x => x.division).AsNoTracking().ToListAsync();
        }

        public async Task<District> GetDistrictById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<bool> SaveDistrict(District district)
        {
            _context.Districts.Add(district);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDistrictById(int id)
        {
            _context.Districts.Remove(_context.Districts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        {
           return await _context.Districts.Where(X => X.divisionId == DivisionId).ToListAsync();
        }
        #endregion

        #region Thana
        public async Task<IEnumerable<Thana>> GetAllThana()
        {
            return await _context.Thanas.Include(x => x.district.division.country).Include(x => x.district.division).Include(x => x.district).AsNoTracking().ToListAsync();
        }
        public async Task<Thana> GetThanaById(int id)
        {
            return await _context.Thanas.FindAsync(id);
        }
        public async Task<bool> SaveThana(Thana thana)
        {
            _context.Thanas.Add(thana);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteThanaById(int id)
        {
            _context.Thanas.Remove(_context.Thanas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId)
        {
            return await _context.Thanas.Where(x => x.districtId == DistrictId).ToListAsync();
        }
        #endregion
        #region address
     
        public async Task<int> SaveAddress(VMSAddress address)
        {
            if (address.Id != 0)
            {
                address.updatedBy = address.createdBy;
                address.updatedAt = DateTime.Now;
                _context.VMSAddresses.Update(address);
            }
            else
            {
                address.createdAt = DateTime.Now;
                _context.VMSAddresses.Add(address);
            }

            await _context.SaveChangesAsync();
            return address.Id;
        }

        public async Task<IEnumerable<VMSAddress>> GetAddress()
        {
            return await _context.VMSAddresses.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VMSAddress> GetAddressById(int id)
        {
            return await _context.VMSAddresses.FindAsync(id);
        }
        public async Task<VMSAddress> GetAddressBystationId(int id)
        {
            return await _context.VMSAddresses.Where(x=>x.fuelStationInfoId==id).Include(x=>x.division).Include(x=>x.district).Include(x=>x.thana).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.VMSAddresses.Remove(_context.VMSAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    
        #endregion
    }
}
