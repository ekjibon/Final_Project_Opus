using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("MobileBankingType", Schema = "POS")]
    public class MobileBankingType : Base
    {
        [MaxLength(100)]
        public string MobileBankingTypeName { get; set; }
    }
}
