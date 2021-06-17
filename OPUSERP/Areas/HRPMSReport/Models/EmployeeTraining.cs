using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class EmployeeTraining
    {
        public string course { get; set; }
        public string year { get; set; }
        public string startDateActual { get; set; }
        public string endDateActual { get; set; }
        public string budget { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public string action { get; set; }
    }
}
