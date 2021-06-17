using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Country
        public async Task<IEnumerable<Country>> GetAllContry()
        {
            return await _context.countries.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetContryCheck(int id, string name)
        {
            var Division = await _context.countries.Where(x => x.countryName == name && x.Id != id).FirstOrDefaultAsync();
            if (Division == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Country> GetContryById(int id)
        {
            return await _context.countries.FindAsync(id);
        }

        public async Task<bool> SaveCountry(Country country)
        {
            if(country.Id != 0)
                _context.countries.Update(country);
            else
                _context.countries.Add(country);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteContryById(int id)
        {
            _context.countries.Remove(_context.countries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Division
        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            return await _context.divisions.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetDivisionCheck(int id, string name, int CountryId)
        {
            var Division = await _context.divisions.Where(x => x.divisionName == name && x.countryId == CountryId && x.Id != id).FirstOrDefaultAsync();
            if (Division == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Division> GetDivisionById(int id)
        {
            return await _context.divisions.FindAsync(id);
        }

        public async Task<bool> SaveDivision(Division division)
        {
            if(division.Id != 0 )
                _context.divisions.Update(division);
            else
                _context.divisions.Add(division);
                return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDivisionById(int id)
        {
            _context.divisions.Remove(_context.divisions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            return await _context.divisions.Where(X => X.countryId == CntId).ToListAsync();
        }
        #endregion

        #region District
        public async Task<IEnumerable<District>> GetAllDistrict()
        {
            return await _context.districts.Include(x => x.division).AsNoTracking().ToListAsync();
        }

        public async Task<District> GetDistrictById(int id)
        {
            return await _context.districts.FindAsync(id);
        }

        public async Task<int> GetDistrictCheck(int id, string name, int DivisionId)
        {
            var district = await _context.districts.Where(x => x.districtName == name && x.divisionId == DivisionId && x.Id != id).FirstOrDefaultAsync();
            if (district == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> SaveDistrict(District district)
        {
            if(district.Id != 0)
                _context.districts.Update(district);
            else
                _context.districts.Add(district);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDistrictById(int id)
        {
            _context.districts.Remove(_context.districts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        {
           return await _context.districts.Where(X => X.divisionId == DivisionId).ToListAsync();
        }
        #endregion

        #region Thana
        public async Task<IEnumerable<Thana>> GetAllThana()
        {
            return await _context.thanas.Include(x => x.district.division.country).Include(x => x.district.division).Include(x => x.district).AsNoTracking().ToListAsync();
        }
        public async Task<Thana> GetThanaById(int id)
        {
            return await _context.thanas.FindAsync(id);
        }

        public async Task<int> GetThanaCheck(int id,string name,int districtId)
        {
            var thana =  await _context.thanas.Where(x=>x.thanaName == name && x.districtId==districtId && x.Id!=id).FirstOrDefaultAsync();
            if (thana == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public async Task<bool> SaveThana(Thana thana)
        {
            if(thana.Id != 0)
                _context.thanas.Update(thana);
            else
                _context.thanas.Add(thana);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteThanaById(int id)
        {
            _context.thanas.Remove(_context.thanas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId)
        {
            return await _context.thanas.Where(x => x.districtId == DistrictId).ToListAsync();
        }
        #endregion
    }
}
