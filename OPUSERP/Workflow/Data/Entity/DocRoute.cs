using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("DocRoute", Schema = "WMS")]
    public class DocRoute:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? employeeReplaceId { get; set; }
        public EmployeeInfo employeeReplace { get; set; }

        public int? docId { get; set; }
        public Doc doc { get; set; }

        public int? actionId { get; set; }
        public ActionInfo action { get; set; }

        public int? order { get; set; }

        public int? isActive { get; set; }

        public string status { get; set; }

        public string remarks { get; set; }
    }
}
