using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IVendorAddressService
    {
        Task<int> SaveVendorAddress(VendorAddress vendorAddress);
        Task<IEnumerable<VendorAddress>> GetVendorAddress();
        Task<VendorAddress> GetVendorAddressById(int id);
        Task<IEnumerable<VendorAddress>> GetVendorAddressByRegId(int regId);
        Task<bool> DeleteVendorAddressById(int id);
    }
}
