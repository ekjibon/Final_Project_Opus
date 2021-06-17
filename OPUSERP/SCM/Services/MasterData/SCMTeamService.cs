using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData
{
    public class SCMTeamService : ISCMTeamService
    {
        private readonly ERPDbContext _context;

        public SCMTeamService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTeamMaster(TeamMaster teamMaster)
        {
            if (teamMaster.Id != 0)
            {
                _context.TeamMasters.Update(teamMaster);
            }
            else
            {
                _context.TeamMasters.Add(teamMaster);
            }
            await _context.SaveChangesAsync();
            return teamMaster.Id;
        }

        public async Task<IEnumerable<TeamMaster>> GetAllTeamMaster()
        {
            var result = await (from t in _context.TeamMasters
                                join u in _context.Users on t.leaderId equals u.userId
                                join e in _context.employeeInfos on u.Id equals e.ApplicationUserId
                                select new TeamMaster
                                {
                                    Id=t.Id,
                                    teamCode = t.teamCode,
                                    teamName = t.teamName,
                                    leaderId = t.leaderId,
                                    isActive = t.isActive,
                                    shortOrder = t.shortOrder,
                                    teamLeader = e.nameEnglish
                                }).AsNoTracking().ToListAsync();
            return result;
        }
        

        public async Task<TeamMaster> GetTeamMasterById(int id)
        {
            return await _context.TeamMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTeamMasterById(int id)
        {
            _context.TeamMasters.Remove(_context.TeamMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #region TeamMember

        public async Task<int> SaveTeamMember(TeamMember teamMember)
        {
            if (teamMember.Id != 0)
            {
                _context.TeamMembers.Update(teamMember);
            }
            else
            {
                _context.TeamMembers.Add(teamMember);
            }
            await _context.SaveChangesAsync();
            return teamMember.Id;
        }

        public async Task<IEnumerable<TeamMember>> GetAllTeamMember()
        {
            return await _context.TeamMembers.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<System.Object>> GetAllTeamMemberByMasterId(int id)
        {
            var result = (from D in _context.TeamMembers
                          join M in _context.TeamMasters on D.teamMasterId equals M.Id
                          join MM in _context.Users on M.leaderId equals MM.userId
                          join EMM in _context.employeeInfos on MM.Id equals EMM.ApplicationUserId
                          join S in _context.Users on D.memberId equals S.userId
                          join E in _context.employeeInfos on S.Id equals E.ApplicationUserId
                          where D.teamMasterId == id
                          select new
                          {
                              D.Id,
                              D.teamMasterId,
                              teamName=M.teamName+" - "+(M.teamCode),
                              leaderName=EMM.nameEnglish,
                              D.memberId,
                              memberName=E.nameEnglish
                          }).AsNoTracking().ToListAsync();

            return await result;
        }


        public async Task<TeamMember> GetTeamMemberById(int id)
        {
            return await _context.TeamMembers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTeamMemberById(int id)
        {
            _context.TeamMembers.Remove(_context.TeamMembers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

    }
}
