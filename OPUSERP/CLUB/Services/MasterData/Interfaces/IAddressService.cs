using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IAddressService
    {
        Task<bool> SaveCountry(Country country);
        Task<IEnumerable<Country>> GetAllContry();
        Task<Country> GetContryById(int id);
        Task<bool> DeleteContryById(int id);
        //for validation
        Task<int> GetContryCheck(int id, string name);

        Task<bool> SaveDivision(Division division);
        Task<IEnumerable<Division>> GetAllDivision();
        Task<Division> GetDivisionById(int id);
        Task<bool> DeleteDivisionById(int id);
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);
        //for validation
        Task<int> GetDivisionCheck(int id, string name, int CountryId);

        Task<bool> SaveDistrict(District district);
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictById(int id);
        Task<bool> DeleteDistrictById(int id);
        Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId);
        //for validation
        Task<int> GetDistrictCheck(int id, string name, int DivisionId);

        Task<bool> SaveThana(Thana thana);
        Task<IEnumerable<Thana>> GetAllThana();
        Task<Thana> GetThanaById(int id);
        Task<bool> DeleteThanaById(int id);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId);
        //for validation
        Task<int> GetThanaCheck(int id, string name, int districtId);

    }
}
