using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.RewardInfo;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSReward.Models
{
    public class RewardViewModel
    {
        public int? rewardId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string letterNo { get; set; }

        public string rewardDate { get; set; }

        public string rewardName { get; set; }

        public string remarks { get; set; }

        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<HRPMS.Data.Entity.RewardInfo.Reward> rewards { get; set; }
    }
}
