using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData.Interfaces
{
    public interface IItemsService
    {
        #region Item
        Task<int> SaveItem(Item item);
        Task<bool> UpdateItemShortName(int? Id, string shortName);
        Task<IEnumerable<Item>> GetAllItem();
        Task<Item> GetItemById(int id);
        Task<bool> DeleteItemById(int id);
        Task<string> GetItemCode();
        Task<IEnumerable<Item>> GetAllItemForRequisition();
        Task<IEnumerable<Item>> GetAllFixedAssetItem();
        Task<int> GetRootIdItem(int currentID);
        Task<IEnumerable<Item>> GetItemByParrentId(int parrentId);
        Task<IEnumerable<Item>> GetAllItemForBoM(string itemCategory);
        Task<Item> GetItemByName(string Name);
        Task<Unit> GetUnitbyName(string name);
        #endregion

        #region ItemCategories
        Task<int> SaveItemCategory(ItemCategory itemCategory);
        Task<IEnumerable<ItemCategory>> GetAllParentItem();
        Task<IEnumerable<ItemCategory>> GetAllParentItembyparentcatid(int id);
        Task<IEnumerable<ItemCategory>> GetAllItemCategory();
        Task<int> GetRootId(int currentID);
        Task<IEnumerable<ItemCategory>> GetCategoryByParrentId(int ParentID);
        Task<ItemCategory> GetItemCategoryById(int id);
        Task<bool> DeleteItemCategoryById(int id);
        Task<ItemCategory> GetItemCategoryByName(string name);
        Task<IEnumerable<ItemCategory>> GetAllPParentItem();
        #endregion

        #region Unit
        Task<bool> SaveUnit(Unit unit);
        Task<IEnumerable<Unit>> GetAllUnit();
        Task<Unit> GetUnitById(int id);
        Task<bool> DeleteUnitById(int id);
        #endregion

        #region ItemType
        Task<bool> SaveItemType(ItemType unit);
        Task<IEnumerable<ItemType>> GetAllItemType();
        Task<ItemType> GetItemTypeById(int id);
        Task<bool> DeleteItemTypeById(int id);
        #endregion

        #region ItemSpecification
        Task<int> SaveItemSpecification(ItemSpecification itemSpecification);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecification();
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByProjectId(int projectId);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemId(int ItemId);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemName(string ItemId);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdandspec(int ItemId, string spec);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdandsku(int ItemId, string spec);
        Task<ItemSpecification> GetItemSpecificationById(int id);
        Task<bool> DeleteItemSpecificationById(int id);
        Task<IEnumerable<SpecificationDetail>> GetAllItemSpecificationDetailsByitemId(int ItemId);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationByitemIdForPriyo(int ItemId);
        Task<bool> DeleteItemSpecificationByItemId(int id);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationName();
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationNameFAM();
        Task<IEnumerable<ItemSpecification>> GetItemSpecificationByItemCategoryId(int catgId);
        #endregion

        #region Specification Category
        Task<int> SaveSpecificationCategory(SpecificationCategory specificationCategory);
        Task<IEnumerable<SpecificationCategory>> GetAllSpecificationCategory();
        Task<SpecificationCategory> GetSpecificationCategoryById(int id);
        Task<SpecificationCategory> GetSpecificationCategoryByname(string name);
        Task<bool> DeleteSpecificationCategoryById(int id);
        Task<IEnumerable<SpecificationCategory>> GetAllSpecificationCategorybycatid(int Id);
        Task<bool> DeleteSpecificationCategoryBycatId(int id);
        Task<IEnumerable<ItemSpecification>> GetAllItemSpecificationbyitemidnew(int id);
        #endregion

        #region Specification Detail
        Task<int> SaveSpecificationDetail(SpecificationDetail specificationDetail);
        Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetail();
        Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetailBySpecId(int SpecId);
        Task<IEnumerable<SpecificationDetailViewModel>> GetAllSpecificationDetailByitemId(int itemId);
        Task<SpecificationDetail> GetSpecificationDetailById(int id);
        Task<bool> DeleteSpecificationDetailById(int id);
        Task<bool> DeleteSpecificationDetailBySpecId(int SpecId);
        Task<bool> DeleteSpecificationDetailBySpecIdcatid(int SpecId, int speccategoryId);
        Task<IEnumerable<SpecificationDetail>> GetAllSpecificationDetailbyitemId(int id);

        Task<bool> DeleteSpecificationDetailByitemid(int SpecId);
        #endregion

        #region Buyer Item Mapping
        Task<bool> SaveBuyerItemMapping(BuyerItemMapping buyerItemMapping);
        Task<IEnumerable<BuyerItemMapping>> GetAllBuyerItemMapping();
        Task<BuyerItemMapping> GetBuyerItemMappingById(int id);
        Task<bool> DeleteBuyerItemMappingById(int id);
		#endregion

		Task<IEnumerable<Item>> GetAllItemInfo();

        #region Feature_Item
        Task<int> SaveFeatureItem(FeatureItem feature);
		Task<IEnumerable<ItemSpecification>> GetItemSpecificationsById(int id);
        Task<IEnumerable<FeatureItem>> AllFeatureItem();
        Task<FeatureItem> GetFeatureItemByID(int id);
        Task<int> DeleteFeatureItemByID(int id);
        Task<IEnumerable<ItemSpecificationDepartmentModel>> GetItemSpecificationsByDepartmentId(int departmentId);
        Task<IEnumerable<ItemSpecificationDepartmentModel>> GetItemByDepartmentWise();
        Task<FeatureItem> GetFeatureItemByUserAndSpecId(int userid, int specid);
        Task<ItemSpecification> itemSpecificationById(int id);
        #endregion

    }
}
