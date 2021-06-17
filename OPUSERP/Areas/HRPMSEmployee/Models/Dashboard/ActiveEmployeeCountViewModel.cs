using OPUSERP.HRPMS.Data.Entity.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Dashboard
{
    public class ActiveEmployeeCountViewModel
    {
        public int? totalActiveEmployee { get; set; }
        public int? totalMalePercent { get; set; }
        public int? totalFeMalePercent { get; set; }
        public List<string> departments { get; set; }
        public List<string> employeTypes { get; set; }
        public List<int> depmalecount { get; set; }
        public List<int> depfemalecount { get; set; }
        public List<int> years { get; set; }
        public List<int> noofemployeesbyyears { get; set; }
        public List<int> departmentswisepercent { get; set; }
        public List<int> employeeTypePercent { get; set; }
        public int? employeepromotionrate { get; set; }
        public int? turnOverrate { get; set; }
        public int? absentRate { get; set; }
        public List<string> days { get; set; }
        public List<string> months { get; set; }
        public List<int> absentlistseven { get; set; }
        public List<int> absentlistmonth { get; set; }
        public List<int> latelistseven { get; set; }
        public List<int> travelcount { get; set; }
        public List<int> monthlyleavecount { get; set; }
        public List<string> leavecatogies { get; set; }
        public List<int> categoryleavecount { get; set; }
        public IEnumerable<EmpAttendance> empAttendances { get; set; }
        public IEnumerable<ActiveEmployeeDetailView> activeEmployeeDetailViews { get; set; }
       // public IEnumerable<DepartmentWiseEmployeeCountViewModel> departmentWiseEmployeeCountViewModels { get; set; }
       
    }
}
