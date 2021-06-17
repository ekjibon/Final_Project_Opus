using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation
{
    [Table("DisiciplinaryAttachment", Schema = "HR")]
    public class DisiciplinaryAttachment:Base
    {
        public int disiplinaryId { get; set; }
        public DisciplinaryAction disciplinary { get; set; }

        public string type { get; set; }

        public string fileTitle { get; set; }

        public string uploadFile { get; set; }
    }
}
