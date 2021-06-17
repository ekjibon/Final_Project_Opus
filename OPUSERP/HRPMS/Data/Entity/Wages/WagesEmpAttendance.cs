using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("WagesEmpAttendance", Schema = "HR")]
    public class WagesEmpAttendance : Base
    {
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

        public string attenUpdateBy { get; set; }

        public string attenUpdateAppBy { get; set; }



    }
}
