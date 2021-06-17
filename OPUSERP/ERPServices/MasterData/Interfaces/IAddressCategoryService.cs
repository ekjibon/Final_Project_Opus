
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
   public interface IAddressCategoryService
    {
        Task<bool> SaveAddressCategory(AddressCategory addressCategory);
        Task<IEnumerable<AddressCategory>> GetAddressCategory();
        Task<AddressCategory> GetAddressCategoryById(int id);
        Task<bool> DeleteAddressCategoryById(int id);
        Task<int> GetAddressCategoryCheck(int id, string name);
        Task<IEnumerable<FiscalYear>> GetAllFiscalYear();
    }
}
