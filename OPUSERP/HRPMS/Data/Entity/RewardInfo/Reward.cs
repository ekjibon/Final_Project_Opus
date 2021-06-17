using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.RewardInfo
{
    [Table("Reward", Schema = "HR")]
    public class Reward:Base
    {
        public int employeeId { get; set; }

        public EmployeeInfo employeeInfo { get; set; }

        public string letterNo { get; set; }

        public string rewardDate { get; set; }

        public string rewardName { get; set; }

        public string remarks { get; set; }
    }
}
