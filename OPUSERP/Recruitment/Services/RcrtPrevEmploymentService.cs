using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class RcrtPrevEmploymentService : IRcrtPrevEmploymentService
    {
        private readonly ERPDbContext _context;

        public RcrtPrevEmploymentService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveRcrtPreviousEmployment(RcrtPreviousEmployment rcrtPreviousEmployment)
        {
            if (rcrtPreviousEmployment.Id != 0)
                _context.RcrtPreviousEmployments.Update(rcrtPreviousEmployment);
            else
                _context.RcrtPreviousEmployments.Add(rcrtPreviousEmployment);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RcrtPreviousEmployment>> GetPreviousEmploymentByCandidateId(int candidateId)
        {
            return await _context.RcrtPreviousEmployments.AsNoTracking().Where(x => x.candidateId == candidateId).Include(x => x.organizationType).ToListAsync();
        }
    }
}
