using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Data.Entity.Auth
{
    public class LogUserPersonInformation:Base
    {
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? statusId { get; set; }
    }
}
