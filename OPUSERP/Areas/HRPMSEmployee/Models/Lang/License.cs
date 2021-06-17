using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Lang
{
    public class License
    {
        public string title { get; set; }
        public string licenseNumber { get; set; }

        public string licenseCategory { get; set; }

        public string place { get; set; }
        
        public string dateOfIssue { get; set; }
        
        public string dateOfExpair { get; set; }

        public string action { get; set; }
    }
}
