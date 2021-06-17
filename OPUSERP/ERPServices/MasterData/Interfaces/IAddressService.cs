
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IAddressService
    {
        #region Division

        Task<bool> SaveCountry(Country country);
        Task<IEnumerable<Country>> GetAllContry();
        Task<Country> GetContryById(int id);
        Task<bool> DeleteContryById(int id);
        //Task<Address> GetAddressByType(int LeadId);
        //Task<Addresses> GetAddressByType(string type, int LeadId);

        #endregion

        #region Division

        Task<bool> SaveDivision(Division division);
        Task<IEnumerable<Division>> GetAllDivision();
        Task<Division> GetDivisionById(int id);
        Task<bool> DeleteDivisionById(int id);
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);

        #endregion

        #region District

        Task<bool> SaveDistrict(District district);
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictById(int id);
        Task<bool> DeleteDistrictById(int id);
        Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId);

        #endregion

        #region Thana

        Task<bool> SaveThana(Thana thana);
        Task<IEnumerable<Thana>> GetAllThana();
        Task<Thana> GetThanaById(int id);
        Task<bool> DeleteThanaById(int id);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId);

        #endregion

        #region Post Office

        Task<bool> SavePostOffice(PostOffice postOffice);
        Task<IEnumerable<PostOffice>> GetAllPostOffice();
        Task<PostOffice> GetPostOfficeById(int id);
        Task<bool> DeletePostOfficeById(int id);
        Task<IEnumerable<PostOffice>> GetPostOfficeByDistrictId(int DistrictId);

        #endregion

        #region Address
        Task<int> SaveAddress(Address address);
        Task<IEnumerable<Address>> GetAddress();
        Task<Address> GetAddressById(int id);
        Task<Address> GetAddressByResourceId(int id);
        Task<bool> DeleteAddressById(int id);
        Task<Address> GetAddressByOrganizationId(int id);
        Task<IEnumerable<Address>> GetAddressListByResourceId(int id);
        Task<IEnumerable<Address>> GetAddressListByOrganizationId(int id);
        Task<Address> GetAddressByCompanyId(int id);
        Task<IEnumerable<Address>> GetAddressListByCompanyId(int id);
        #endregion

    }
}
