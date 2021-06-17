using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("TaxCategory", Schema = "CRM")]
    public class TaxCategory : Base
    {
        [MaxLength(100)]
        public string taxCategoryName { get; set; }
        public decimal? taxRate { get; set; }
    }
}
