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
    public class UpdateCandidateInfoService : IUpdateCandidateInfoService
    {
        private readonly ERPDbContext _context;

        public UpdateCandidateInfoService(ERPDbContext context)
        {
            _context = context;
        }
        #region Address 

        public async Task<bool> SaveAddress(RCRTAddress address)
        {
            if (address.Id != 0)
                _context.RCRTAddresses.Update(address);
            else
                _context.RCRTAddresses.Add(address);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTAddress>> GetAllAddress()
        {
            return await _context.RCRTAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<RCRTAddress> GetAddressById(int id)
        {
            return await _context.RCRTAddresses.FindAsync(id);
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.RCRTAddresses.Remove(_context.RCRTAddresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTAddress>> GetAddressByCanidateId(int candidateId)
        {
            return await _context.RCRTAddresses.Where(x => x.candidateId == candidateId).AsNoTracking().ToListAsync();
        }

        public async Task<RCRTAddress> GetAddressByTypeAndCandidateId(int candidateId, string type)
        {
            return await _context.RCRTAddresses.Where(x => x.candidateId == candidateId && x.type == type).Include(x => x.division).Include(x => x.district).Include(x => x.thana).FirstOrDefaultAsync();
        }

        #endregion

        #region RCRT Education

        public async Task<bool> SaveRCRTEducation(RCRTEducation rCRTEducation)
        {
            if (rCRTEducation.Id != 0)
                _context.RCRTEducations.Update(rCRTEducation);
            else
                _context.RCRTEducations.Add(rCRTEducation);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTEducation>> GetAllRCRTEducation()
        {
            return await _context.RCRTEducations.AsNoTracking().ToListAsync();
        }

        public async Task<RCRTEducation> GetRCRTEducationById(int id)
        {
            return await _context.RCRTEducations.FindAsync(id);
        }

        public async Task<bool> DeleteEducationalQualificationById(int id)
        {
            _context.RCRTEducations.Remove(_context.RCRTEducations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTEducation>> GetRCRTEducationByCandidateId(int candidateId)
        {
            return await _context.RCRTEducations.Where(x => x.candidateId == candidateId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RCRTEducation>> GetRCRTEducationListByCandidateId(int? candidateId)
        {
            return await _context.RCRTEducations.Where(x => x.candidateId == candidateId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        #endregion


        //#region Photograph

        public async Task<bool> SaveImage(CandidatePhoto photograph)
        {
            if (photograph.Id != 0)
                _context.CandidatePhotos.Update(photograph);
            else
                _context.CandidatePhotos.Add(photograph);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CandidatePhoto>> GetPhotograph()
        {
            return await _context.CandidatePhotos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CandidatePhoto>> GetPhotographByCandidateId(int candidateId)
        {
            return await _context.CandidatePhotos.Where(x => x.candidateId == candidateId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.CandidatePhotos.Remove(_context.CandidatePhotos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<CandidatePhoto> GetPhotographByTypeAndCandidateId(int candidateId, string type)
        {
            return await _context.CandidatePhotos.Where(x => x.candidateId == candidateId && x.type == type).AsNoTracking().FirstOrDefaultAsync();
        }

        //#endregion
    }
}
