using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("HRPMSAttachment", Schema = "HR")]
    public class HRPMSAttachment:Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? atttachmentCategoryId { get; set; }
        public AtttachmentCategory atttachmentCategory { get; set; }

        public string fileUrl { get; set; }

        public string fileTitle { get; set; }

        public string remarks { get; set; }

        public DateTime? attachmentDate { get; set; }
    }
}
