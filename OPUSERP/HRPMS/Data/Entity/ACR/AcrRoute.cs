using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrRoute", Schema = "HR")]
    public class AcrRoute : Base
    {
        public int? supervisorId { get; set; }
        public EmployeeInfo supervisor { get; set; }

        public int acrInitiateId { get; set; }
        public AcrInitiate acrInitiate { get; set; }

        public int? sequence { get; set; }

        public int? isActive { get; set; }
    }
}
