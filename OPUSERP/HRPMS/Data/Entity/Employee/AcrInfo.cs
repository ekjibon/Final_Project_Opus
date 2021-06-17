using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("AcrInfo", Schema = "HR")]
    public class AcrInfo :  Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        [MaxLength(20)]
        public string supervisorCode { get; set; }
        [MaxLength(250)]
        public string supervisorName { get; set; }
        [MaxLength(100)]
        public string supervisorDesg { get; set; }
        [MaxLength(20)]
        public string signatoryCode { get; set; }
        [MaxLength(250)]
        public string signatoryName { get; set; }
        [MaxLength(100)]
        public string signatoryDesg { get; set; }
        [MaxLength(10)]
        public string year { get; set; }
        public string advanceComment { get; set; }
        public int score { get; set; } //ConfidentialField
        public string remarks { get; set; }
        [MaxLength(200)]
        public string finalStatus { get; set; }
        public DateTime? effectiveDate { get; set; }
        [MaxLength(300)]
        public string fileUrl { get; set; }
    }
}
