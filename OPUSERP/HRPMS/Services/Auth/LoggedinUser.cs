using Microsoft.AspNetCore.Identity;
using OPUSERP.Data;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Services.Auth.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Auth
{
    public class LoggedinUser : ILoggedinUser
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ERPDbContext _context;

        public LoggedinUser(UserManager<ApplicationUser> userManage, RoleManager<ApplicationRole> roleManager, ERPDbContext context)
        {
            _userManage = userManage;
            _roleManager = roleManager;
            _context = context;
        }

        public int UserAuthId(string id)
        {
            return _context.employeeInfos.Where(x => x.ApplicationUserId == id).Select(x => x.Id).FirstOrDefault();
        }

        public async Task<int> UserEmpId(string name)
        {
            ApplicationUser data = await _userManage.FindByNameAsync(name);
            return _context.employeeInfos.Where(x => x.ApplicationUserId == data.Id).Select(x => x.Id).FirstOrDefault();
        }

        public async Task<string> usersOrganization(string name)
        {
            ApplicationUser data =  await _userManage.FindByNameAsync(name);
            return data.org;
        }

        public async Task<List<string>> UsersRoles(string name)
        {
            return (List<string>) await _userManage.GetRolesAsync( await _userManage.FindByNameAsync(name));  
        }
    }
}
