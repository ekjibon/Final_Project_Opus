using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData.Interfaces
{
    public interface IDisItemPriceFixingService
    {
        Task<int> SaveDisItemPriceFixing(DisItemPriceFixing itemPriceFixing);
        Task<IEnumerable<DisItemPriceFixing>> GetAllItemPriceFixing();
        Task<DisItemPriceFixing> GetItemPriceFixingById(int id);
        Task<bool> DeleteItemPriceFixingById(int id);
        Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId);
        Task<IEnumerable<DisItemPriceFixing>> GetActiveItemPriceFixing();
        Task<IEnumerable<DisItemPriceFixing>> GetActiveItemPriceFixingWithBarCode();
        Task<DisItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id);
        Task<DisItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo);
        Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice();
       // Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId);
       // Task<IEnumerable<OfferMaster>> GetAllPriceFixedItemSpacificationByItemOffId(int itemId);
        Task<IEnumerable<DisItemPriceFixing>> GetAllItemSpacificationByItemName(string ItemName);
        Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId);
        Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemtypeId(int itemId, int distypeid);
        #region PriceDetails
        Task<bool> SavePriceDetail(DisItemPriceFixingDetails disItemPriceFixingDetails);
        Task<IEnumerable<DisItemPriceFixingDetails>> GetAllpriceDetail();
        Task<IEnumerable<DisItemPriceFixingDetails>> GetAllPriceDetailsByMasterId(int masterId);
        Task<DisItemPriceFixingDetails> GetPriceDetailById(int id);
        Task<bool> DeletePriceDetailById(int id);
        Task<bool> DeletePriceDetailByMasterId(int masterId);
        Task<IEnumerable<ItemPriceFixingDetailViewModel>> ItemPriceFixingDetailViewModels(int masterId);
        #endregion
        #region packageMaster
        Task<int> SavePackageMaster(PackageMaster packageMaster);
        Task<IEnumerable<PackageMaster>> GetAllpackageMasters();
        Task<IEnumerable<PackageMaster>> GetAllPriceDetailsBydistributortypeId(int masterId);
        Task<IEnumerable<PackageMaster>> GetAllPriceDetailsBydistributortypedate(int masterId, DateTime date);
        Task<PackageMaster> GetpackageMasterById(int id);
        Task<bool> DeletepackageMasterById(int id);
        #endregion
        #region packagedetail
        Task<bool> SavePackageDetail(PackageDetail packageDetail);
        Task<IEnumerable<PackageDetail>> GetAllpackageDetail();
        Task<IEnumerable<PackageDetail>> GetAllPackageDetailByMasterId(int masterId);
        Task<PackageDetail> GetPackageDetailById(int id);
        Task<bool> DeletePackageDetailByMasterId(int masterId);
        Task<bool> DeletePackageDetailById(int id);
        Task<IEnumerable<ItemDisPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItempackageId(int itemId,int typeId);
        #endregion
    }
}
