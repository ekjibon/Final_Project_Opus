using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("ACRProcess", Schema = "HR")]
    public class ACRProcess:Base
    {
        public int acrSeduleId { get; set; }

        public ACRSchedule aCRSchedule { get; set; }

        public int? sequence { get; set; }

        public int? actorId { get; set; }

        public EmployeeInfo employeeInfo { get; set; }

        public string actionType { get; set; }

        public string remarks { get; set; }

        public string routingType { get; set; }//rout,reroud

        public string dueDate { get; set; }//targat date
    }
}
