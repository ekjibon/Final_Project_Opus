using OPUSERP.OtherSales.Data.Entity.Sales;
using System.Collections.Generic;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesMenuViewModel
    {
        public int masterId { get; set; }
        public int? salesMenuCategoryId { get; set; }
        public int? itemSpecificationId { get; set; }
        public string menuName { get; set; }
        public string fileName { get; set; }
        public string activeStatus { get; set; }

        public IEnumerable<SalesMenu> salesMenus { get; set; }
        public IEnumerable<SalesMenuCategory> salesMenuCategories { get; set; }
    }
}
