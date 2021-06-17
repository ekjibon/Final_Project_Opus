using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier.Interface
{
   public interface IOrganizationService
    {
        Task<int> SaveOrganization(Organization organization);
        Task<IEnumerable<Organization>> GetAllOrganization();
        Task<IEnumerable<Organization>> GetOrganizationByLedgerId();
        Task<Organization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationsById(int id);
        Task<bool> UpdateVendorStatus(int Id, int ledgerId);
        Task<Organization> GetorganizationBysupplierName(string SupplierName);
        Task<string> GetAutoOrganizationCode();
    }
}
