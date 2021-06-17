﻿using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class AddressService : IAddressService
    {
        private readonly ERPDbContext _context;

        public AddressService(ERPDbContext context)
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
            if(country.Id != 0)
                _context.Countries.Update(country);
            else
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
            if(division.Id != 0 )
                _context.Divisions.Update(division);
            else
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
            return await _context.Districts.Include(x => x.division).OrderBy(a=>a.districtName).AsNoTracking().ToListAsync();
        }

        public async Task<District> GetDistrictById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<bool> SaveDistrict(District district)
        {
            if(district.Id != 0)
                _context.Districts.Update(district);
            else
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
            if(thana.Id != 0)
                _context.Thanas.Update(thana);
            else
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
    }
}
