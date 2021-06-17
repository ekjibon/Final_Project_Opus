using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class AddressEducationPhotoService : IMemberInfoService
    {
        private readonly ERPDbContext _context;

        public AddressEducationPhotoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Address 

        public async Task<bool> SaveAddress(MemberAddress address)
        {
            if (address.Id != 0)
                _context.memberAddresses.Update(address);
            else
                _context.memberAddresses.Add(address);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberAddress>> GetAllAddress()
        {
            return await _context.memberAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<MemberAddress> GetAddressById(int id)
        {
            return await _context.memberAddresses.FindAsync(id);
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.addresses.Remove(_context.addresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberAddress>> GetAddressByEmpId(int empId)
        {
            return await _context.memberAddresses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<MemberAddress> GetAddressByTypeAndEmpId(int empId, string type)
        {
            return await _context.memberAddresses.Include(x=> x.division).Include(x=>x.district).Include(x=>x.thana).Where(x => x.employeeId == empId && x.type == type).FirstOrDefaultAsync();
        }

        #endregion

        #region EducationalQualification

        public async Task<bool> DeleteEducationalQualificationById(int id)
        {
            _context.educationalQualifications.Remove(_context.educationalQualifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberEducationalQualification>> GetAllEducationalQualification()
        {
            return await _context.memberEducationalQualifications.AsNoTracking().ToListAsync();
        }

        public async Task<MemberEducationalQualification> GetEducationalQualificationById(int id)
        {
            return await _context.memberEducationalQualifications.FindAsync(id);
        }
      
        public async Task<bool> SaveEducationalQualification(MemberEducationalQualification educationalQualification)
        {
            if (educationalQualification.Id != 0)
                _context.memberEducationalQualifications.Update(educationalQualification);
            else
                _context.memberEducationalQualifications.Add(educationalQualification);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberEducationalQualification>> GetEducationalQualificationByEmpId(int empId)
        {
            return await _context.memberEducationalQualifications.Where(x => x.employeeId == empId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MemberEducationalQualification>> GetEducationalQualificationListByEmpId(int? empId)
        {
            return await _context.memberEducationalQualifications.Where(x => x.employeeId == empId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        #endregion


        #region Photograph

        public async Task<bool> SaveImage(MemberPhotograph photograph)
        {
            if (photograph.Id != 0)
                _context.memberPhotographs.Update(photograph);
            else
                _context.memberPhotographs.Add(photograph);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberPhotograph>> GetPhotograph()
        {
            return await _context.memberPhotographs.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MemberPhotograph>> GetPhotographByEmpId(int empId)
        {
            return await _context.memberPhotographs.Where(x => x.employeeId == empId).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.memberPhotographs.Remove(_context.memberPhotographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<MemberPhotograph> GetPhotographByTypeAndEmpId(int empId, string type)
        {
            return await _context.memberPhotographs.Where(x => x.employeeId == empId && x.type == type).AsNoTracking().FirstOrDefaultAsync();
        }
       #endregion
    }
}
