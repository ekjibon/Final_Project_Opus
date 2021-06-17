using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("EmployeeType", Schema = "HR")]
    public class EmployeeType : Base
    {
        [Required]
        public string empType { get; set; }
        public string empTypeBn { get; set; }

        public string shortName { get; set; }
    }
}
