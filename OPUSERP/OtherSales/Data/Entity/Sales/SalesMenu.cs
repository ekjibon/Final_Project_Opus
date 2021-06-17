using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesMenu", Schema = "OSales")]
    public class SalesMenu:Base
    {
        public int? salesMenuCategoryId { get; set; }
        public SalesMenuCategory salesMenuCategory { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        
        [MaxLength(300)]
        public string menuName { get; set; }
        [MaxLength(500)]
        public string fileName { get; set; }
        [MaxLength(10)]
        public string activeStatus { get; set; }
    }
}
