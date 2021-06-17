using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("Currency", Schema = "SCM")]
    public class Currency:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string currencyName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string currencyShortName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string currencySign { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? currencyRate { get; set; }

        public int? shortOrder { get; set; }
    }
}
