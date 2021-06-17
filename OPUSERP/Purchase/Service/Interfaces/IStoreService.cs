
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service.Interfaces
{
    public interface IStoreService
    {
        #region Store Type
        Task<int> SaveStoreType(StoreType storeType);
        Task<IEnumerable<StoreType>> GetAllStoreType();
        Task<bool> DeleteStoreTypeById(int id);
        #endregion

        #region Store 
        Task<int> SaveStore(Store store);
        Task<IEnumerable<Store>> GetAllStore();
        Task<Store> GetStoreById(int id);
        Task<Store> GetStoreByStoreName(string StoreName);
        Task<bool> DeleteStoreById(int id);
        #endregion

        #region Store Address
        Task<int> SaveStoreAddress(StoreAddress storeAddress);
        Task<StoreAddress> GetStoreAddressByStoreId(int StoreId);
        Task<IEnumerable<StoreAddress>> GetStoreAddressListByStoreId(int StoreId);
        Task<bool> DeleteStoreAddressById(int id);
        Task<bool> DeleteStoreAddressByStoreId(int StoreId);
        #endregion

        #region StoreContact
        Task<int> SaveStoreContact(StoreContact storeContact);
        Task<IEnumerable<StoreContact>> GetStoreContactByStoreId(int StoreId);
        Task<bool> DeleteStoreContactById(int id);
        Task<bool> DeletStoreContactByStoreId(int StoreId);
        #endregion

        #region Store Assign 
        Task<int> SaveStoreAssign(StoreAssign storeAssign);
        Task<IEnumerable<StoreAssignListViewModel>> GetAllStoreAssign();
        Task<IEnumerable<StoreAssign>> GetAllStoreAssignByUser(string aspnetuser);
        Task<bool> DeletStoreAssignByAspnetuserId(string aspnetuserId);
        Task<IEnumerable<StoreAssign>> GetAllStoreAssignByUserId(string aspnetuser);
        Task<StoreAssign> GetStoreAssignById(int id);
        Task<bool> DeleteStoreAssignById(int id);
        #endregion
    }
}
