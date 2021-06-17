using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IAddressesService
    {
        Task<bool> SaveAddresses(Address Addresses);
        Task<IEnumerable<Address>> GetAllAddresses();
        Task<Address> GetAddressesById(int id);
        Task<bool> DeleteAddressesById(int id);
        Task<IEnumerable<Address>> GetAddressesByLeadId(int leadId);
    }
}
