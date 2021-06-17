using OPUSERP.HRPMS.Data.Entity.Assignment;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.HRPMSAssignment.Models
{
    [NotMapped]
    public class AssignmentViewModel
    {
        public int? ID { get; set; }
        public string employeeId { get; set; }

        public string employeeName { get; set; }

        public int? assignmentTypeId { get; set; }

        //public string assignmentTypeName { get; set; }

        public int? EntryNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? designationId { get; set; }

        public string designationName { get; set; }

        public int? departmentId { get; set; }

        public string departmentName { get; set; }

        public string Remarks { get; set; }

        public Lang.Assignment fLang { get; set; }
        public IEnumerable<HRPMS.Data.Entity.Assignment.Assignment> assignments { get; set; } 
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Department> departments { get; set; }
    }
}
