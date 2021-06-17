using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class ReportingViewModel
    {
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Thana> thanas { get; set; }

        public IEnumerable<Religion> religions { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Degree> degrees { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<Language> languages { get; set; }
        public IEnumerable<LeaveType> leaveTypelist { get; set; }
        
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<PNS> pNs { get; set; }
        public IEnumerable<HRProject> hRProjects { get; set; }
        public IEnumerable<HRDoner> hRDoners { get; set; }
        public IEnumerable<HRActivity> hRActivities { get; set; }
        public IEnumerable<Department> departments { get; set; }

        public EmployeeInfoLn fLang { get; set; }
        public string visualEmpCodeName { get; set; }
    }
}
