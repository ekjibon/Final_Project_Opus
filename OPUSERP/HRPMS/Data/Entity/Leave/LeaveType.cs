using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Leave
{
    [Table("LeaveType", Schema = "HR")]
    public class LeaveType : Base
    {
        [Required]
        public string nameEn { get; set; }
        public string nameBn { get; set; }
        public string description { get; set; }
    }
}
