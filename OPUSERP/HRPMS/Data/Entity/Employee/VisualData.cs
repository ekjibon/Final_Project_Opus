using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("VisualData", Schema = "HR")]
    public class VisualData : Base
    {
        [MaxLength(200)]
        public string empCodeName { get; set; }

    }
}