using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("ReviewRoute", Schema = "WMS")]
    public class ReviewRoute:Base
    {
        public int? docRouteId { get; set; }
        public DocRoute docRoute { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? actionId { get; set; }
        public ActionInfo action { get; set; }

        public string status { get; set; }
    }
}
