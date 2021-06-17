using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("DiscountRate", Schema = "POS")]
    public class DiscountRate : Base
    {
        public decimal? rate { get; set; }
    }
}
