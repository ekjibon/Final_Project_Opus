using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer
{
    [Table("PromotionAttachment", Schema = "HR")]
    public class PromotionAttachment:Base
    {
        public int promotionId { get; set; }
        public PromotionEntry promotion { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
