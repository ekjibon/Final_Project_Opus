using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class RcrtMembershipService : IRcrtMembershipService
    {
        private readonly ERPDbContext _context;

        public RcrtMembershipService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRcrtMembershipInfo(RcrtEmpMembership rcrtEmpMembership)
        {
            if (rcrtEmpMembership.Id != 0)
                _context.RcrtEmpMemberships.Update(rcrtEmpMembership);
            else
                _context.RcrtEmpMemberships.Add(rcrtEmpMembership);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
