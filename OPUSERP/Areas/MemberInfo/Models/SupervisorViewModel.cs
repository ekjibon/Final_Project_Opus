using OPUSERP.Areas.MemberInfo.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class SupervisorViewModel
    {
        public string employeeID { get; set; }
        public string supervisorID { get; set; }
        public string supervisorName { get; set; }
        public string supDesignation { get; set; }
        public string commandOrder { get; set; }
        public string canFinalApprover { get; set; }

        public string employeeNameCode { get; set; }

        public SupervisorLn fLang { get; set; }
        //public IEnumerable<supervisors> supervisors { get; set; }
    }
}
