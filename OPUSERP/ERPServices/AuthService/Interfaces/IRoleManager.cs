using Microsoft.AspNetCore.Identity;
using OPUSERP.Areas.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.AuthService.Interfaces
{
    public interface IRoleManager
    {
        Task<List<ApplicationRoleViewModel>> GetAllRolesAsync();
        Task<IdentityResult> CreateAsync(ApplicationRoleViewModel model);
        Task<IdentityResult> DeleteAsync(ApplicationRoleViewModel model);
        Task<IEnumerable<string>> GetRoleIds(string userName);
    }
}
