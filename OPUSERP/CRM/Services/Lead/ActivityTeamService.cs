using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{

    public class ActivityTeamService : IActivityTeamService
    {
        private readonly ERPDbContext _context;

        public ActivityTeamService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveActivityTeam(ActivityTeam activityTeam)
        {
            if (activityTeam.Id != 0)
                _context.ActivityTeams.Update(activityTeam);
            else
                _context.ActivityTeams.Add(activityTeam);
            await _context.SaveChangesAsync();
            return activityTeam.Id;
        }

        public async Task<IEnumerable<ActivityTeam>> GetAllActivityTeamByActivityMasterId(int id)
        {
            List<ActivityTeam> Activityteams = new List<ActivityTeam>();
            List<int?> teamIds =  _context.ActivityTeams.Include(x=>x.activityMaster.leads).Where(x => x.activityMaster.Id == id).Select(x => x.teamId).ToList();

            List<Team> teams= _context.Teams.Where(x => teamIds.Contains(x.Id)).ToList();

          IEnumerable<ActivityTeam> activityTeamslist=  await _context.ActivityTeams.Where(x=>x.activityMaster.Id==id).Include(x=>x.activityMaster).ToListAsync();
            foreach(ActivityTeam data in activityTeamslist)
            {
                var team = teams.Where(x => x.Id == data.teamId).FirstOrDefault();
                Activityteams.Add(new ActivityTeam {
                    activityMasterId=data.activityMasterId,
                    teamId=data.teamId,
                    team=data.team
                    

                });

            }

            return Activityteams;


        }

        public async Task<ActivityTeam> GetActivityTeamById(int id)
        {
            return await _context.ActivityTeams.FindAsync(id);
        }

        public async Task<bool> DeleteActivityTeamById(int id)
        {
            _context.ActivityTeams.Remove(_context.ActivityTeams.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteActivityTeamByMasterId(int id)
        {
            _context.ActivityTeams.RemoveRange(_context.ActivityTeams.Where(x=>x.activityMasterId==id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
