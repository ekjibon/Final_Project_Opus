using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.RetirementAndTermination
{
    [Table("PRLAttachment", Schema = "HR")]
    public class PRLAttachment:Base
    {
        public int prlApplicationid { get; set; }
        public PRLApplication pRLApplication { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
