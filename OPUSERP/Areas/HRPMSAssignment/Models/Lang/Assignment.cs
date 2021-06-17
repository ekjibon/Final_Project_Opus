using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAssignment.Models.Lang
{
    public class Assignment
    {

        public string employeeName { get; set; }

        //public string assignmentTypeName { get; set; }

        public int? EntryNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string designationName { get; set; }

        public string departmentName { get; set; }

        public string Remarks { get; set; }
    }
}
