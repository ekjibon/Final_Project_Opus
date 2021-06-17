using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.SCMMasterData.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ItemViewModel
    {
        public int itemId { get; set; }
        public int? parentId { get; set; }
        public int? isParent { get; set; }

        public int? unitId { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string itemDescription { get; set; }
        public int? accountLedgerId { get; set; }
        public int? reOrderLevel { get; set; }

        public int itemSpecificationId { get; set; }
        public string itemspecification { get; set; }
        public int? NumType { get; set; }
        public string[] itemSpecificationName { get; set; }
        public int?[] specificationCategoryId { get; set; }
        public string[] specificationCategoryname { get; set; }
        public string[] skuList { get; set; }
        public string[] CategoryValue { get; set; }
        public int? itemTypeId { get; set; }
        public string itemShortName { get; set; }
        public string fileName { get; set; }
        public IFormFile itemPhoto { get; set; }
        public ItemLn fLang { get; set; }
        public IEnumerable<Item> items { get; set; }
        public IEnumerable<Unit> units { get; set; }
        public IEnumerable<SpecificationCategory> specificationCategories { get; set; }
        public IEnumerable<ItemType> itemTypes { get; set; }
        public IEnumerable<SpecificationDetail> specificationDetails { get; set; }
        public IEnumerable<ItemCategory> itemCategories { get; set; }
        public IEnumerable<ItemCategory> itemCategoriesall { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<ItemSpecification> itemSpecifications { get; set; }
        //public string[] speclist { get; set; }
        // public ItemCodeViewModel itemCodeViewModel { get; set; }
    }
}
