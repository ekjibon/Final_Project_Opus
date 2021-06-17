using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Dashboard
{
    public class DepartmentWiseEmployeeCountViewModel
    {
     
        public string DeparmentName{get;set;}
        public int? countM { get; set; }
        public int? countF { get; set; }
    }
}
