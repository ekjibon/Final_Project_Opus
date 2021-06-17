using OPUSERP.Areas.POS.Models;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
   public interface IItemPriceFixingService
    {
        Task<bool> SaveItemPriceFixing(ItemPriceFixing itemPriceFixing);
        Task<IEnumerable<ItemPriceFixing>> GetAllItemPriceFixing();
        Task<ItemPriceFixing> GetItemPriceFixingById(int id);
        Task<bool> DeleteItemPriceFixingById(int id);
        Task<bool> UpdateLastItemPriceFixing(int itemSpecificationId);
        Task<IEnumerable<ItemPriceFixing>> GetActiveItemPriceFixing();
        Task<IEnumerable<ItemPriceFixing>> GetActiveItemPriceFixingWithBarCode();
        Task<ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeById(int id);
        Task<ItemPriceFixing> GetActiveItemPriceFixingWithBarCodeNo(string barCodeNo);
        Task<IEnumerable<Item>> GetAllActiveItemFromItemPrice();
        Task<IEnumerable<ItemPriceFixingViewModelspec>> GetAllPriceFixedItemSpacificationByItemId(int itemId);
        Task<IEnumerable<OfferMaster>> GetAllPriceFixedItemSpacificationByItemOffId(int itemId);
    }
}
