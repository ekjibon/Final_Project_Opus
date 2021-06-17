using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Cold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Cold.Interfaces
{
    public interface IColdService
    {
        #region Cold Activity Masters

        Task<int> SaveColdActivityMasters(ColdActivityMasters coldActivityMasters);
        Task<IEnumerable<ColdActivityMasters>> GetAllColdActivityMasters();
        Task<ColdActivityMasters> GetColdActivityMastersById(int id);
        Task<bool> DeleteColdActivityMastersById(int id);
        Task<IEnumerable<ColdActivityMasterCViewModel>> GetColdActivityMasterCByLeadId();

        #endregion

        #region Cold Activity Details

        Task<int> SaveColdActivityDetails(ColdActivityDetails coldActivityDetails);
        Task<IEnumerable<ColdActivityDetails>> GetAllColdActivityDetailsByActivityMasterId(int id);
        Task<ColdActivityDetails> GetColdActivityDetailsById(int id);
        Task<bool> DeleteColdActivityDetailsById(int id);
        Task<bool> DeleteColdActivityDetailsByMasterId(int id);

        #endregion

        #region Cold Activity Masters

        Task<int> SaveColdActivityTeams(ColdActivityTeams coldActivityTeams);
        Task<IEnumerable<ColdActivityTeams>> GetAllColdActivityTeamsByActivityMasterId(int id);
        Task<ColdActivityTeams> GetColdActivityTeamsById(int id);
        Task<bool> DeleteColdActivityTeamsById(int id);
        Task<bool> DeleteColdActivityTeamsByMasterId(int id);

        #endregion
        #region Cold Activity Discussions

        Task<int> SaveColdActivityDiscussion(ColdActivityDiscussion coldActivityDiscussion);
        Task<IEnumerable<ColdActivityDiscussion>> GetAllColdActivityDiscussionsByActivityMasterId(int id);
        Task<ColdActivityDiscussion> GetColdActivityDiscussionById(int id);
        Task<bool> DeleteColdActivityDiscussionById(int id);
        Task<bool> DeleteColdActivityDiscussionByMasterId(int id);

        #endregion

    }
}
