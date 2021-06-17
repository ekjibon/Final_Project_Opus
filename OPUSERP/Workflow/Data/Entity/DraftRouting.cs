using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("DraftRouting", Schema = "WMS")]
    public class DraftRouting:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? draftDocId { get; set; }
        public DraftDoc draftDoc { get; set; }

        public int? order { get; set; }

        public string remarks { get; set; }
    }
}
