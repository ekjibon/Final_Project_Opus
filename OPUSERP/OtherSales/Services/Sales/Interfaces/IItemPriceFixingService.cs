using OPUSERP.Areas.POS.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface IItemPriceFixingService
    {
        Task<bool> SaveItemPriceFixing(OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing itemPriceFixing);
        Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetAllItemPriceFixing();
        Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetItemPriceFixingById(int id);
        Task<bool> DeleteItemPriceFixingById(int id);
        Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId);
        Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetActiveItemPriceFixing();
        Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetActiveItemPriceFixingWithBarCode();
        Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id);
        Task<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo);
        Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice();
        Task<IEnumerable<Item>> GetAllActiveRentalItem();
        Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId);
        Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacificationByItemIdForPriyo(int itemId);
        Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacificationByItemId(int itemId);
        Task<IEnumerable<OfferMaster>> GetAllPriceFixedItemSpacificationByItemOffId(int itemId);
        Task<IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing>> GetAllItemSpacificationByItemName(string ItemName);
        Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllItemSpacification();

        Task<PurchaseOrdersDetail> GetItemPriceFixingBySpecId(int id);
    }
}
