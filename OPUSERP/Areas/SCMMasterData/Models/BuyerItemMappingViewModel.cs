using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class BuyerItemMappingViewModel
    {
        public int mappingId { get; set; }
        public int? itemCategoryId { get; set; }
        public int? employeeInfoId { get; set; }

        public IEnumerable<ItemCategory> itemCategories { get; set; }
        public IEnumerable<BuyerItemMapping> buyerItemMappings { get; set; }
    }
}
