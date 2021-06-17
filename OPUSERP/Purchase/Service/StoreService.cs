using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service
{
    public class StoreService: IStoreService
    {
        private readonly ERPDbContext _context;

        public StoreService(ERPDbContext context)
        {
            _context = context;
        }

        #region Store Type
        public async Task<int> SaveStoreType(StoreType storeType)
        {
            try
            {
                if (storeType.Id != 0)
                {
                    storeType.updatedBy = storeType.createdBy;
                    storeType.updatedAt = DateTime.Now;
                    _context.StoreTypes.Update(storeType);
                }
                else
                {
                    storeType.createdAt = DateTime.Now;
                    _context.StoreTypes.Add(storeType);
                }

                await _context.SaveChangesAsync();
                return storeType.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StoreType>> GetAllStoreType()
        {
            return await _context.StoreTypes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteStoreTypeById(int id)
        {
            _context.StoreTypes.Remove(_context.StoreTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region Store 
        public async Task<int> SaveStore(Store store)
        {
            try
            {
                if (store.Id != 0)
                {
                    store.updatedBy = store.createdBy;
                    store.updatedAt = DateTime.Now;
                    _context.Stores.Update(store);
                }
                else
                {
                    store.createdAt = DateTime.Now;
                    _context.Stores.Add(store);
                }

                await _context.SaveChangesAsync();
                return store.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Store>> GetAllStore()
        {
            return await _context.Stores.Include(x => x.storeType).AsNoTracking().ToListAsync();
        }

        public async Task<Store> GetStoreById(int id)
        {
            return await _context.Stores.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Store> GetStoreByStoreName(string StoreName)
        {
            return await _context.Stores.Where(x => x.storeName == StoreName).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteStoreById(int id)
        {
            _context.Stores.Remove(_context.Stores.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region Store Address
        public async Task<int> SaveStoreAddress(StoreAddress storeAddress)
        {
            try
            {
                if (storeAddress.Id != 0)
                {
                    storeAddress.updatedBy = storeAddress.createdBy;
                    storeAddress.updatedAt = DateTime.Now;
                    _context.StoreAddresses.Update(storeAddress);
                }
                else
                {
                    storeAddress.createdAt = DateTime.Now;
                    _context.StoreAddresses.Add(storeAddress);
                }

                await _context.SaveChangesAsync();
                return storeAddress.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<StoreAddress> GetStoreAddressByStoreId(int StoreId)
        {
            return await _context.StoreAddresses.Where(x => x.storeId == StoreId).Include(x => x.division).Include(x => x.district).Include(x => x.thana).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StoreAddress>> GetStoreAddressListByStoreId(int StoreId)
        {
            return await _context.StoreAddresses.Include(x => x.store).Where(x => x.storeId == StoreId)
                .Include(x => x.division)
                .Include(x => x.district)
                .Include(x => x.thana)
                .Include(x => x.addressCategory).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteStoreAddressById(int id)
        {
            _context.StoreAddresses.Remove(_context.StoreAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteStoreAddressByStoreId(int StoreId)
        {
            IEnumerable<StoreAddress> storeAddressInfos = await _context.StoreAddresses.Where(x => x.storeId == StoreId).AsNoTracking().ToListAsync();
            foreach (StoreAddress data in storeAddressInfos)
            {
                _context.StoreAddresses.Remove(_context.StoreAddresses.Find(data.Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region StoreContact
        public async Task<int> SaveStoreContact(StoreContact storeContact)
        {
            try
            {
                if (storeContact.Id != 0)
                {
                    storeContact.updatedBy = storeContact.createdBy;
                    storeContact.updatedAt = DateTime.Now;
                    _context.StoreContacts.Update(storeContact);
                }
                else
                {
                    storeContact.createdAt = DateTime.Now;
                    _context.StoreContacts.Add(storeContact);
                }

                await _context.SaveChangesAsync();
                return storeContact.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StoreContact>> GetStoreContactByStoreId(int StoreId)
        {
            return await _context.StoreContacts.Include(x => x.store).Where(x => x.storeId == StoreId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteStoreContactById(int id)
        {
            _context.StoreContacts.Remove(_context.StoreContacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletStoreContactByStoreId(int StoreId)
        {
            IEnumerable<StoreContact> storeContactInfos = await _context.StoreContacts.Where(x => x.storeId == StoreId).AsNoTracking().ToListAsync();
            foreach (StoreContact data in storeContactInfos)
            {
                _context.StoreContacts.Remove(_context.StoreContacts.Find(data.Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Store Assign 
        public async Task<int> SaveStoreAssign(StoreAssign storeAssign)
        {
            try
            {
                if (storeAssign.Id != 0)
                {
                    storeAssign.updatedBy = storeAssign.createdBy;
                    storeAssign.updatedAt = DateTime.Now;
                    _context.StoreAssigns.Update(storeAssign);
                }
                else
                {
                    storeAssign.createdAt = DateTime.Now;
                    _context.StoreAssigns.Add(storeAssign);
                }

                await _context.SaveChangesAsync();
                return storeAssign.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StoreAssignListViewModel>> GetAllStoreAssign()
        {
            List<StoreAssignListViewModel> listdata = new List<StoreAssignListViewModel>();
            IEnumerable<StoreAssign> Store = _context.StoreAssigns.Include(x => x.aspnetuser).Include(x => x.store).ToList();
            foreach (StoreAssign st in Store)
            {
                var countdata = listdata.Where(x => x.aspnetuser == st.aspnetuser.UserName).ToList();
                if (countdata.Count() == 0)
                {
                    var storecount = Store.Where(x => x.aspnetuserId == st.aspnetuserId).ToList();
                    listdata.Add(new StoreAssignListViewModel
                    {
                        aspnetuser = st.aspnetuser.UserName,
                        count = storecount.Count()

                    });
                }
            }


            return listdata;
        }

        public async Task<IEnumerable<StoreAssign>> GetAllStoreAssignByUser(string aspnetuser)
        {

            return await _context.StoreAssigns.Include(x => x.store).Include(x => x.aspnetuser).Where(x => x.aspnetuser.UserName == aspnetuser).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<StoreAssign>> GetAllStoreAssignByUserId(string aspnetuser)
        {

            return await _context.StoreAssigns.Include(x => x.store).Include(x => x.aspnetuser).Where(x => x.aspnetuser.Id == aspnetuser).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletStoreAssignByAspnetuserId(string aspnetuserId)
        {
            IEnumerable<StoreAssign> storeContactInfos = await _context.StoreAssigns.Where(x => x.aspnetuserId == aspnetuserId).AsNoTracking().ToListAsync();
            foreach (StoreAssign data in storeContactInfos)
            {
                _context.StoreAssigns.Remove(_context.StoreAssigns.Find(data.Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<StoreAssign> GetStoreAssignById(int id)
        {
            return await _context.StoreAssigns.Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteStoreAssignById(int id)
        {
            _context.StoreAssigns.Remove(_context.StoreAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion
    }
}
