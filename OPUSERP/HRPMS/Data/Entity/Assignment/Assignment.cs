using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Data.Entity.Assignment
{
    [Table("Assignment", Schema = "HR")]
    public class Assignment:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? assignmentTypeId { get; set; }//1=Assignment,2=Transfer

        //public string assignmentTypeName { get; set; }

        public int? EntryNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

        public string Remarks { get; set; }
    }
}
