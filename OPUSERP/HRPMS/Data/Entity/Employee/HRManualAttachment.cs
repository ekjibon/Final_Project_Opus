using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("HRManualAttachment", Schema = "HR")]
    public class HRManualAttachment : Base
    {
        public string fileUrl { get; set; }

        public string fileTitle { get; set; }

        public string remarks { get; set; }

        public DateTime? date { get; set; }
    }
}
