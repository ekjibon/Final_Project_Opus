using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.AuthService.Interfaces
{
    public interface IJwtFactoryService
    {
        Task<String> GenerateToken(string userName, string id, IList<string> roles);
    }
}
