using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrPersonalWorkDescription", Schema = "HR")]
    public class AcrPersonalWorkDescription:Base
    {
        public int acrInitiateId { get; set; }
        public AcrInitiate acrInitiate { get; set; }

        public string description { get; set; }
    }
}
