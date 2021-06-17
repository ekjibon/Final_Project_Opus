using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier.Interface
{
   public interface ISupplierItemInfoService
    {
        Task<int> SaveSupplierItemInfo(SupplierItemInfo supplierItemInfo);
        Task<IEnumerable<SupplierItemInfo>> GetAllSupplierItemInfo();
        Task<SupplierItemInfo> GetSupplierItemInfoById(int id);
        Task<bool> DeleteSupplierItemInfoById(int id);

        Task<IEnumerable<SupplierItemInfo>> GetSupplierItemInfobyOrganizationId(int Id);
        Task<bool> DeleteSupplierItemInfoByOrganizationId(int id);
    }
}
