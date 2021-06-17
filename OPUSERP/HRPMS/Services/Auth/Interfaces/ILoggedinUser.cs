using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Auth.Interfaces
{
    public interface ILoggedinUser
    {
        Task<string>usersOrganization(string name);
        Task<List<string>> UsersRoles(string name);
        Task<int> UserEmpId(string name);
        int UserAuthId(string id);
    }
}
