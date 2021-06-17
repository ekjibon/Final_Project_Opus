using OPUSERP.Data.Entity.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IERPModuleService
    {
        Task<int> SaveERPModule(ERPModule eRPModule);
        Task<IEnumerable<ERPModule>> GetAllERPModule();
        Task<ERPModule> GetERPModuleById(int id);
        Task<ERPModule> GetERPModuleByModuleName(string moduleName);
        Task<bool> DeleteERPModuleById(int id);
    }
}
