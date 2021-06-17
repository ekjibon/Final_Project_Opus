using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeProjectActivityViewModel
    {
        public int? id { get; set; }
        public int? employeeId { get; set; }
        public int? hRDonerId { get; set; }
        public int? hRActivityId { get; set; }
        public int? hRProjectId { get; set; }
        public int? projectId { get; set; }
        public int? isActive { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<HRDoner> hRDoners { get; set; }
        public IEnumerable<HRProject> hRProjects { get; set; }
        public IEnumerable<Project> projects { get; set; }
        public IEnumerable<HRActivity> hRActivities { get; set; }
        public IEnumerable<EmployeeProjectActivity> employeeProjectActivities { get; set; }
        public IEnumerable<EmployeeProjectAssign> employeeProjectAssigns { get; set; }
    }
}
