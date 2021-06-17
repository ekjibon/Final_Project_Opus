using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.RewardInfo;
using OPUSERP.HRPMS.Services.RewardInfo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.RewardInfo
{
    public class RewardService : IRewardService
    {
        private readonly ERPDbContext _context;

        public RewardService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteRewardById(int id)
        {
            _context.rewards.Remove(_context.rewards.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reward>> GetRewardByEmpId(int empId)
        {
            return await _context.rewards.Where(x => x.employeeId == empId).OrderBy(x => x.rewardDate).AsNoTracking().ToListAsync();
        }

        public async Task<Reward> GetRewardById(int id)
        {
            return await _context.rewards.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> SaveReward(Reward reward)
        {
            if (reward.Id != 0)
                _context.rewards.Update(reward);
            else
                _context.rewards.Add(reward);

            await _context.SaveChangesAsync();
            return reward.Id;
        }
    }
}
