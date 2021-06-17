using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("Children", Schema = "HR")]
    public class Children : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        [MaxLength(250)]
        public string childName { get; set; }
        [MaxLength(250)]
        public string childNameBN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }
        
        public string education { get; set; }
        [MaxLength(30)]
        public string gender { get; set; }
        [MaxLength(50)]
        public string bin { get; set; }
        [MaxLength(250)]
        public string occupation { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(250)]
        public string organization { get; set; }

        [MaxLength(100)]
        public string nid { get; set; }
        [MaxLength(30)]
        public string bloodGroup { get; set; }

        public int disability { get; set; }
        public string disablityType { get; set; }
    }
}
