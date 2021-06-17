using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("Spouse", Schema = "HR")]
    public class Spouse: Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        [MaxLength(250)]
        public string spouseName { get; set; }
        [MaxLength(150)]
        public string email { get; set; }
        [MaxLength(250)]
        public string spouseNameBN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        public string occupation { get; set; }
        [MaxLength(20)]
        public string gender { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(450)]
        public string organization { get; set; }
        [MaxLength(100)]
        public string bin { get; set; }
        [MaxLength(100)]
        public string nid { get; set; }
        [MaxLength(30)]
        public string bloodGroup { get; set; }
        [MaxLength(250)]
        public string contact { get; set; }
        [MaxLength(450)]
        public string highestEducation { get; set; }
        [MaxLength(100)]
        public string homeDistrict { get; set; }

    }
}
