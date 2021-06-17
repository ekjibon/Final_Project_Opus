using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPService.MasterData.Interfaces
{
    public interface ITeamService
    {
        // Sector
        Task<bool> SaveTeam(Team team);
        Task<IEnumerable<Team>> GetAllTeamByModule(int moduleId);
        Task<Team> GetTeamById(int id);
        Task<int> GetRootId(int currentID);
        Task<bool> DeleteTeamById(int id);
        Task<Team> GetTeamByaspnetuserId(string Id);
        Task<IEnumerable<Team>> GetTeamByParrentId(int? ParentID);
        Task<IEnumerable<Team>> GetAllTeam();
        Task<IEnumerable<CRMTeamViewModel>> GetTeamInfoByTeamId(int? teamId);
        Task<IEnumerable<Team>> GetTeamByTeamIdAndUserId(int? teamId, string aspnetuserId);
        Task<int> SaveTeamNew(Team team);
        Task<Team> GetTeambyCode(string teamcode);
    }
}
