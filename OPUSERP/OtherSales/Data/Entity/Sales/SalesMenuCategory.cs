using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesMenuCategory", Schema = "OSales")]
    public class SalesMenuCategory:Base
    {
        [MaxLength(300)]
        public string categoryName { get; set; }        
        [MaxLength(10)]
        public string activeStatus { get; set; }
    }
}
