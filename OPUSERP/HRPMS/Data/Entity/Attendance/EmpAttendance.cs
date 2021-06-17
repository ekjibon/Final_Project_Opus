using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Attendance
{
    [Table("EmpAttendance", Schema = "HR")]
    public class EmpAttendance : Base
    {
        [MaxLength(50)]
        public string punchCardNo { get; set; }
        [MaxLength(20)]
        public string workDate { get; set; }
        [MaxLength(20)]
        public string startTime { get; set; }
        [MaxLength(20)]
        public string endTime { get; set; }
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