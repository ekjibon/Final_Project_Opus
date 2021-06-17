using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("VatCategory", Schema = "CRM")]
    public class VatCategory : Base
    {
        [MaxLength(100)]
        public string vatCategoryName { get; set; }
        public decimal? vatRate { get; set; }
    }
}
