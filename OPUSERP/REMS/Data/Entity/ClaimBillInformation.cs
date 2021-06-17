using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("ClaimBillInformation", Schema = "REMS")]
    public class ClaimBillInformation:Base
    {
        public int? claimId { get; set; }
        public ClaimRegister claim { get; set; }

        [MaxLength(350)]
        public string paymentHead { get; set; }

        public decimal? amount { get; set; }
    }
}
