using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.jwt.Interfaces
{
    public interface IJwtFactoryService
    {
        Task<String> GenerateToken(string userName, string id, IList<string> roles);
    }
}
