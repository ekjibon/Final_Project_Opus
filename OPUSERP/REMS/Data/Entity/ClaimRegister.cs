using OPUSERP.Data.Entity;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("ClaimRegister", Schema = "REMS")]
    public class ClaimRegister:Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }

        public DateTime? claimDate { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string claimNumber { get; set; }

        public string claimDescription { get; set; }

        public int? statusId { get; set; }
        public ClaimStatusInfo status { get; set; }

        public int? userId { get; set; }

        public int? isWarrenty { get; set; }
    }
}
