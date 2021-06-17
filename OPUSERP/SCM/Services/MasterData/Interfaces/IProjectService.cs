using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData.Interfaces
{
    public interface IProjectService
    {
        Task<int> SaveProject(Project project);
        void UpdateProjectStatusById(int projectId, string status);
        Task<IEnumerable<Project>> GetProjectList();
        Task<IEnumerable<Project>> GetProjectListByUser(string userName);
        Task<Project> GetProjectById(int id);
        Task<bool> DeleteProjectById(int id);
        Task<IEnumerable<Project>> GetProjectBySBUId(int id);
    }
}
