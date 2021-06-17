using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeLanguage", Schema = "HR")]
    public class EmployeeLanguage : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [MaxLength(50)]
        public string reading { get; set; }

        [MaxLength(50)]
        public string writing { get; set; }

        [MaxLength(50)]
        public string speaking { get; set; }

        public int? languageId { get; set; }
        public Language language { get; set; }

        [MaxLength(100)]
        public string proficiency { get; set; }
    }
}
