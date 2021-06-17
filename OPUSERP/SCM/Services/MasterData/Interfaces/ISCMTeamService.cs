using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData.Interfaces
{
    public interface ISCMTeamService
    {
        Task<int> SaveTeamMaster(TeamMaster teamMaster);
        Task<IEnumerable<TeamMaster>> GetAllTeamMaster();
        Task<TeamMaster> GetTeamMasterById(int id);
        Task<bool> DeleteTeamMasterById(int id);

        Task<int> SaveTeamMember(TeamMember teamMember);
        Task<IEnumerable<TeamMember>> GetAllTeamMember();
        Task<TeamMember> GetTeamMemberById(int id);
        Task<bool> DeleteTeamMemberById(int id);

        Task<IEnumerable<System.Object>> GetAllTeamMemberByMasterId(int id);
    }
}
