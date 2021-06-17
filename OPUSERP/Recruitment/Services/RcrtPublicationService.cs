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
    public class RcrtPublicationService : IRcrtPublicationService
    {
        private readonly ERPDbContext _context;

        public RcrtPublicationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRcrtPublication(RcrtPublication rcrtPublication)
        {
            if (rcrtPublication.Id != 0)
                _context.RcrtPublications.Update(rcrtPublication);
            else
                _context.RcrtPublications.Add(rcrtPublication);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RcrtPublication>> GetPublicationsByCandidateId(int candidateId)
        {
            return await _context.RcrtPublications.Where(x => x.candidateId == candidateId).AsNoTracking().ToListAsync();
        }
    }
}
