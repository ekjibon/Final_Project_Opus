using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IRcrtMembershipService
    {
        Task<bool> SaveRcrtMembershipInfo(RcrtEmpMembership rcrtEmpMembership);
    }
}
