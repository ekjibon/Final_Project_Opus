using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IEmployeeOrganogramService
    {
        Task<IEnumerable<MemberAvailablePosts>> employeeAvailablePosts(int orgId);
    }
}
