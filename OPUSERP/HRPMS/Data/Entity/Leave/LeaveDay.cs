using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Leave
{
    [Table("LeaveDay", Schema = "HR")]
    public class LeaveDay : Base
    {
        [MaxLength(200)]
        public string leaveDayName { get; set; }
        [MaxLength(200)]
        public string leaveDayNameBn { get; set; }
        [MaxLength(500)]
        public string description { get; set; }
        [MaxLength(5)]
        public string dayStartTime { get; set; }
        [MaxLength(5)]
        public string dayEndTime { get; set; }
    }
}
