using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Attendance
{
    [Table("EmpAttendance", Schema = "HR")]
    public class Emp_AttendenceTemp : Base
    {
        [MaxLength(50)]
        public string punchCardNo { get; set; }
        public DateTime? workDate { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? summaryId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? workingTime { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? latetime { get; set; }
        public string remarks { get; set; }
        [MaxLength(200)]
        public string attenUpdateBy { get; set; }
        [MaxLength(200)]
        public string attenUpdateAppBy { get; set; }
    }
}