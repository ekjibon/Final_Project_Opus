using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPService.MasterData
{
    public class TeamService : ITeamService
    {
        private readonly ERPDbContext _context;

        public TeamService(ERPDbContext context)
        {
            _context = context;
        }

        // Sector
        public async Task<bool> SaveTeam(Team team)
        {
            if (team.Id != 0)
                _context.Teams.Update(team);
            else
                _context.Teams.Add(team);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Team>> GetAllTeam()
        {
            return await _context.Teams.Include(x => x.module).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<Team> GetTeambyCode(string teamcode)
        {
            return await _context.Teams.Include(x => x.module).Where(x => x.teamCode == teamcode).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Team>> GetAllTeamByModule(int moduleId)
        {
            return await _context.Teams.Where(x => x.moduleId == moduleId).Include(x => x.module).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Team>> GetTeamByParrentId(int? parrentId)
        {
            return await _context.Teams.Where(x => x.teamId == parrentId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<Team> GetTeamByaspnetuserId(string Id)
        {
            return await _context.Teams.Where(x => x.aspnetuserId == Id).OrderByDescending(a => a.Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<bool> DeleteTeamById(int id)
        {
            _context.Teams.Remove(_context.Teams.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<int> GetRootId(int currentID)
        {
            Team team;
            do
            {
                team = await _context.Teams.FindAsync(currentID);
                currentID = team.teamId ?? 0;
            }
            while (currentID != 0);
            //  int a = 10;
            return team.Id;
        }

        public async Task<IEnumerable<CRMTeamViewModel>> GetTeamInfoByTeamId(int? teamId)
        {
            List<CRMTeamViewModel> result = (from T in _context.Teams
                                             join U in _context.Users on T.aspnetuserId equals U.Id
                                             join E in _context.employeeInfos on U.EmpCode equals E.employeeCode
                                             //where T.teamId == teamId
                                             select new CRMTeamViewModel
                                             {
                                                 tId = T.Id,
                                                 teamId = T.teamId,
                                                 areaId = T.area.Id,
                                                 memberName = T.memberName,
                                                 teamCode = T.teamCode,
                                                 aspnetuserId = T.aspnetuserId,
                                                 areaName = T.area.areaName,
                                                 empName=E.nameEnglish,
                                                 moduleId=T.moduleId

                                             }).OrderByDescending(a => a.tId).ToList();
            return result;
        }

        public async Task<IEnumerable<Team>> GetTeamByTeamIdAndUserId(int? teamId, string aspnetuserId)
        {
            return await _context.Teams.Include(x => x.area).Where(x => x.teamId == teamId && x.aspnetuserId == aspnetuserId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveTeamNew(Team team)
        {
            if (team.Id != 0)
                _context.Teams.Update(team);
            else
                _context.Teams.Add(team);

            await _context.SaveChangesAsync();
            return team.Id;
        }
    }
}
