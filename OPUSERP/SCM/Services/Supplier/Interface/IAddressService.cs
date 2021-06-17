using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier.Interface
{
   public interface IAddressService
    {
        Task<int> SaveAddress(Address address);
        Task<IEnumerable<Address>> GetAddress();
        Task<Address> GetAddressById(int id);
        Task<bool> DeleteAddressById(int id);

        Task<IEnumerable<Address>> GetAddressListByOrganizationId(int id);
        Task<Address> GetAddressByOrganizationId(int id);
        Task<bool> DeleteAddressByOrganizationId(int id);
    }
}
