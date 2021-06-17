using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Disciplinary
{
    [Table("Disciplinary", Schema = "HR")]
    public class Disciplinary:Base
    {
        public int employeeId { get; set; }
    }
}
