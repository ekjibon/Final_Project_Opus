using OPUSERP.Data.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeBasicModel
    {
        public string empName { get; set; }
        public string empCode { get; set; }
        public string designationName { get; set; }
        public string sectionName { get; set; }
        public string deptName { get; set; }
        public string mobileNo { get; set; }
        public string imagePath { get; set; }
        public string sectionType { get; set; }
        public int? sectionId { get; set; }
        public IEnumerable<LogUserPersonInformation> logUserPersonInformation { get; set; }
    }
}
