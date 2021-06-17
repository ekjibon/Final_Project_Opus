using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models.Lang
{
    public class ItemCategoryLn
    {
        public string title { get; set; }
        public string itemSubCategoryId { get; set; }
        public string itemCategoryName { get; set; }
        public string parentCategoryName { get; set; }
        public string itemCategoryPrefix { get; set; }
        public string description { get; set; }
        public string accountLedgerId { get; set; }
        public string categoryTree { get; set; }
        public string action { get; set; }
    }
}
