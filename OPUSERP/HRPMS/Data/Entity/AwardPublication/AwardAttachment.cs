using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.AwardPublication
{
    [Table("AwardAttachment", Schema = "HR")]
    public class AwardAttachment:Base
    {
        public int awardId { get; set; }
        public AwardEntry award { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
