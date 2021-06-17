using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("RepairTransactionLog", Schema = "REMS")]
    public class RepairTransactionLog:Base
    {
        public int? claimId { get; set; }
        public ClaimRegister claim { get; set; }

        public int? statusInfoId { get; set; }
        public ClaimStatusInfo statusInfo { get; set; }

        public string description { get; set; }
    }
}
