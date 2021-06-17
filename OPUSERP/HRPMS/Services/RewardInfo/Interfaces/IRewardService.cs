using OPUSERP.HRPMS.Data.Entity.RewardInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.RewardInfo.Interfaces
{
    public interface IRewardService
    {
        Task<int> SaveReward(Reward reward);
        Task<IEnumerable<Reward>> GetRewardByEmpId(int empId);
        Task<bool> DeleteRewardById(int id);
        Task<Reward> GetRewardById(int id);
    }
}
