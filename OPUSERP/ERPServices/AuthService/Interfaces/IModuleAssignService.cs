using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.Interfaces
{
    public interface IModuleAssignService
    {
        Task<int> SaveModuleAccessPage(ModuleAccessPage userAccessPage);
        Task<IEnumerable<ModuleAccessPage>> GetModuleAccessPages();
        Task<IEnumerable<ERPModule>> GetERPModules();
        Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid);
        Task<IEnumerable<ModuleAccessPageViewModel>> GetModuleAccessPagesactive(string UserTypeId);
        Task<IEnumerable<ERPModule>> GetERPModulesForTeam();




    }
}
