using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Cold.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Cold
{
    public class ColdService : IColdService
    {
        private readonly ERPDbContext _context;

        public ColdService(ERPDbContext context)
        {
            _context = context;
        }

        #region Cold Activity Masters

        public async Task<int> SaveColdActivityMasters(ColdActivityMasters coldActivityMasters)
        {
            if (coldActivityMasters.Id != 0)
                _context.ColdActivityMasters.Update(coldActivityMasters);
            else
                _context.ColdActivityMasters.Add(coldActivityMasters);
            await _context.SaveChangesAsync();
            return coldActivityMasters.Id;
        }

        public async Task<IEnumerable<ColdActivityMasters>> GetAllColdActivityMasters()
        {
            return await _context.ColdActivityMasters.Include(x => x.activityCategory).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<ColdActivityMasters> GetColdActivityMastersById(int id)
        {
            return await _context.ColdActivityMasters.Where(x => x.Id == id).Include(x => x.activityCategory).FirstOrDefaultAsync();
        }     
       
        public async Task<bool> DeleteColdActivityMastersById(int id)
        {
            _context.ColdActivityMasters.Remove(_context.ColdActivityMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Cold Activity Details

        public async Task<int> SaveColdActivityDetails(ColdActivityDetails coldActivityDetails)
        {
            if (coldActivityDetails.Id != 0)
                _context.ColdActivityDetails.Update(coldActivityDetails);
            else
                _context.ColdActivityDetails.Add(coldActivityDetails);
            await _context.SaveChangesAsync();
            return coldActivityDetails.Id;
        }

        public async Task<IEnumerable<ColdActivityDetails>> GetAllColdActivityDetailsByActivityMasterId(int id)
        {
            List<ColdActivityDetails> ActivityDetailss = new List<ColdActivityDetails>();
            List<string> contactIds = _context.ColdActivityDetails.Where(x => x.coldActivityMasters.Id == id).Select(x => x.contactName).ToList();

        

            IEnumerable<ColdActivityDetails> activityDetailslist = await _context.ColdActivityDetails.Where(x => x.coldActivityMasters.Id == id).Include(x => x.coldActivityMasters).ToListAsync();
            foreach (ColdActivityDetails data in activityDetailslist)
            {
             
                ActivityDetailss.Add(new ColdActivityDetails
                {
                    coldActivityMastersId = data.coldActivityMastersId,
                 
                    contactName = data.contactName
                });
            }
            return ActivityDetailss;
        }

        public async Task<ColdActivityDetails> GetColdActivityDetailsById(int id)
        {
            return await _context.ColdActivityDetails.FindAsync(id);
        }

        public async Task<bool> DeleteColdActivityDetailsById(int id)
        {
            _context.ColdActivityDetails.Remove(_context.ColdActivityDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteColdActivityDetailsByMasterId(int id)
        {
            _context.ColdActivityDetails.RemoveRange(_context.ColdActivityDetails.Where(x => x.coldActivityMastersId == id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region Cold Activity Masters

        public async Task<int> SaveColdActivityTeams(ColdActivityTeams coldActivityTeams)
        {
            if (coldActivityTeams.Id != 0)
                _context.ColdActivityTeams.Update(coldActivityTeams);
            else
                _context.ColdActivityTeams.Add(coldActivityTeams);
            await _context.SaveChangesAsync();
            return coldActivityTeams.Id;
        }

        public async Task<IEnumerable<ColdActivityTeams>> GetAllColdActivityTeamsByActivityMasterId(int id)
        {
            List<ColdActivityTeams> Activityteams = new List<ColdActivityTeams>();
            List<int> teamIds = _context.ColdActivityTeams.Where(x => x.coldActivityMasters.Id == id).Select(x => x.teamId).ToList();

            List<Team> teams = _context.Teams.Where(x => teamIds.Contains(x.Id)).ToList();

            IEnumerable<ColdActivityTeams> activityTeamslist = await _context.ColdActivityTeams.Where(x => x.coldActivityMasters.Id == id).Include(x => x.coldActivityMasters).ToListAsync();
            foreach (ColdActivityTeams data in activityTeamslist)
            {
                var team = teams.Where(x => x.Id == data.teamId).FirstOrDefault();
                Activityteams.Add(new ColdActivityTeams
                {
                    coldActivityMastersId = data.coldActivityMastersId,
                    teamId = data.teamId,
                    team = data.team
                });
            }
            return Activityteams;
        }

        public async Task<ColdActivityTeams> GetColdActivityTeamsById(int id)
        {
            return await _context.ColdActivityTeams.FindAsync(id);
        }

        public async Task<bool> DeleteColdActivityTeamsById(int id)
        {
            _context.ColdActivityTeams.Remove(_context.ColdActivityTeams.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteColdActivityTeamsByMasterId(int id)
        {
            _context.ColdActivityTeams.RemoveRange(_context.ColdActivityTeams.Where(x => x.coldActivityMastersId == id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
        #region Cold Activity Discussion

        public async Task<int> SaveColdActivityDiscussion(ColdActivityDiscussion coldActivityDiscussion)
        {
            if (coldActivityDiscussion.Id != 0)
                _context.ColdActivityDiscussions.Update(coldActivityDiscussion);
            else
                _context.ColdActivityDiscussions.Add(coldActivityDiscussion);
            await _context.SaveChangesAsync();
            return coldActivityDiscussion.Id;
        }
        public async Task<IEnumerable<ColdActivityMasterCViewModel>> GetColdActivityMasterCByLeadId()
        {
            List<ColdActivityMasterCViewModel> lstviewdata = new List<ColdActivityMasterCViewModel>();
            List<ColdActivityMasters> lstmaster = await _context.ColdActivityMasters.Include(x => x.activityCategory).OrderByDescending(x=>x.Id).ToListAsync();
            foreach (ColdActivityMasters data in lstmaster)
            {
                int sourceId = data.Id;
                int count = 1;
                do
                {
                    List<ColdActivityMasters> activityMaster = await _context.ColdActivityMasters.Where(x => x.coldActivityMastersId == sourceId).ToListAsync();
                    if (activityMaster.Count() != 0)
                    {
                        foreach (ColdActivityMasters item in activityMaster)
                        {
                            count++;
                            sourceId = item.Id;

                        }
                    }
                    else
                    {
                        sourceId = 0;
                    }

                }
                while (sourceId != 0);
                lstviewdata.Add(new ColdActivityMasterCViewModel
                {
                    activityMasters = data,
                    countchild = count
                });

            }
            return lstviewdata; //await _context.ActivityMasters.Where(x => x.leadsId == id).Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
        }
        public async Task<IEnumerable<ColdActivityDiscussion>> GetAllColdActivityDiscussionsByActivityMasterId(int id)
        {
            List<ColdActivityDiscussion> coldActivityDiscussions = new List<ColdActivityDiscussion>();
            List<string> contactIds = _context.ColdActivityDiscussions.Where(x => x.coldActivityMasters.Id == id).Select(x => x.discussion).ToList();



            IEnumerable<ColdActivityDiscussion> activityDetailslist = await _context.ColdActivityDiscussions.Where(x => x.coldActivityMasters.Id == id).Include(x => x.coldActivityMasters).ToListAsync();
            foreach (ColdActivityDiscussion data in activityDetailslist)
            {

                coldActivityDiscussions.Add(new ColdActivityDiscussion
                {
                    coldActivityMastersId = data.coldActivityMastersId,

                    discussion = data.discussion
                });
            }
            return coldActivityDiscussions;
        }

        public async Task<ColdActivityDiscussion> GetColdActivityDiscussionById(int id)
        {
            return await _context.ColdActivityDiscussions.FindAsync(id);
        }

        public async Task<bool> DeleteColdActivityDiscussionById(int id)
        {
            _context.ColdActivityDiscussions.Remove(_context.ColdActivityDiscussions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteColdActivityDiscussionByMasterId(int id)
        {
            _context.ColdActivityDiscussions.RemoveRange(_context.ColdActivityDiscussions.Where(x => x.coldActivityMastersId == id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion
    }
}
