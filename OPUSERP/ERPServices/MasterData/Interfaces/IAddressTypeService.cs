using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IAddressTypeService
    {
        Task<bool> SaveAddressType(AddressType addressType);
        Task<IEnumerable<AddressType>> GetAllAddressType();
        Task<AddressType> GetAddressTypeById(int id);
        Task<bool> DeleteAddressTypeById(int id);
    }
}
