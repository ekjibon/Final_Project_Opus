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
    public class RcrtQualificationsService : IRcrtQualificationsService
    {
        private readonly ERPDbContext _context;

        public RcrtQualificationsService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveRcrtOtherQualification(RCRTOtherQualification rCRTOtherQualification)
        {
            if (rCRTOtherQualification.Id != 0)
                _context.RCRTOtherQualifications.Update(rCRTOtherQualification);
            else
                _context.RCRTOtherQualifications.Add(rCRTOtherQualification);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTOtherQualification>> GetOtherQualificationByCandidateId(int candidateId)
        {
            return await _context.RCRTOtherQualifications.AsNoTracking().Where(x => x.candidateId == candidateId).Include(x => x.result).Include(x => x.otherQualificationHead).ToListAsync();
        }

        public async Task<bool> SaveRcrtProfessionalQualification(RCRTProfessionalQualification rCRTProfessionalQualification)
        {
            if (rCRTProfessionalQualification.Id != 0)
                _context.RCRTProfessionalQualifications.Update(rCRTProfessionalQualification);
            else
                _context.RCRTProfessionalQualifications.Add(rCRTProfessionalQualification);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTProfessionalQualification>> GetProfessionalQualificationByCandidateId(int candidateId)
        {
            return await _context.RCRTProfessionalQualifications.AsNoTracking().Where(x => x.candidateId == candidateId).Include(x => x.result).Include(x => x.qualificationHead).ToListAsync();
        }
    }
}
