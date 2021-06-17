using OPUSERP.Areas.SCMMasterData.Models.Lang;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ItemCategoryViewModel
    {
        public int itemcategoryId { get; set; }

        public int? parentId { get; set; }
        public int? isParent { get; set; }

        public string categoryName { get; set; }
        public string categoryDescription { get; set; }
        public string categoryPrefix { get; set; }

        public string[] descriptions { get; set; }
        public ItemCategoryLn fLang { get; set; }
        public IEnumerable<ItemCategory> itemCategories { get; set; }
    }
}
