using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("WagesPhotograph", Schema = "HR")]
    public class WagesPhotograph : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        [Required]
        public string url { get; set; }

        public string remarks { get; set; }

        [Required]
        public string type { get; set; }

    }
}
