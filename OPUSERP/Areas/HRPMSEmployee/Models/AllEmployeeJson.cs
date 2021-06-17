using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class AllEmployeeJson
    {
        public int employeeId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string mobileNumberOffice { get; set; }
        public string emailAddress { get; set; }
        public string imageUrl { get; set; }
        public string companies { get; set; }
        public string action { get; set; }
    }
}
