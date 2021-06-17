using OPUSERP.OtherSales.Data.Entity.Sales;
using System.Collections.Generic;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesMenuCategoryViewModel
    {
        public int masterId { get; set; }
        public string categoryName { get; set; }
        public string activeStatus { get; set; }

        public IEnumerable<SalesMenuCategory> salesMenuCategories { get; set; }
        public IEnumerable<SalesMenu> salesMenus { get; set; }
    }
}
