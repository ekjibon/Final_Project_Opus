using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer
{
    [Table("TransferAttachment", Schema = "HR")]
    public class TransferAttachment:Base
    {
        public int taransferId { get; set; }
        public TransferEntry transfer { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
