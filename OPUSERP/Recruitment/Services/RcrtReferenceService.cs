using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class RcrtReferenceService : IRcrtReferenceService
    {
        private readonly ERPDbContext _context;

        public RcrtReferenceService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveRcrtReference(RcrtReference rcrtReference)
        {
            if (rcrtReference.Id != 0)
                _context.RcrtReferences.Update(rcrtReference);
            else
                _context.RcrtReferences.Add(rcrtReference);

            await _context.SaveChangesAsync();
            return rcrtReference.Id;
        }
    }
}
