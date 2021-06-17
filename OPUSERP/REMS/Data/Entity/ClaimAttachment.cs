using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("ClaimAttachment", Schema = "REMS")]
    public class ClaimAttachment:Base
    {
        public int? claimId { get; set; }
        public ClaimRegister claim { get; set; }
        [MaxLength(150)]
        public string subject { get; set; }
        [MaxLength(350)]
        public string fileName { get; set; }
        [MaxLength(250)]
        public string fileType { get; set; }
        [MaxLength(500)]
        public string filePath { get; set; }
    }
}
