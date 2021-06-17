using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData.Interfaces
{
    public interface ISalesTeamDeploymentService
    {
        Task<bool> DeleteSalesTeamDeploymentById(int id);
        Task<IEnumerable<SalesTeamDeployment>> GetAllSalesTeamDeployment();
        Task<SalesTeamDeployment> GetSalesTeamDeploymentById(int id);
        Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByareaId(int id);
        Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByempId(int id);
        Task<bool> SaveSalesTeamDeployment(SalesTeamDeployment salesTeamDeployment);
        Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByparrentId(int id);
        Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByparrentrelId(int id, int relId);
    }
}
