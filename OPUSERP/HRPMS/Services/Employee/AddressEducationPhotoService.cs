using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class AddressEducationPhotoService : IEmployeeInfoService
    {
        private readonly ERPDbContext _context;

        public AddressEducationPhotoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Address 

        public async Task<bool> SaveAddress(Address address)
        {
            if (address.Id != 0)
                _context.addresses.Update(address);
            else
                _context.addresses.Add(address);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddress()
        {
            return await _context.addresses.AsNoTracking().ToListAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.addresses.FindAsync(id);
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            _context.addresses.Remove(_context.addresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByEmpId(int empId)
        {
            return await _context.addresses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<Address> GetAddressByTypeAndEmpId(int empId, string type)
        {
            return await _context.addresses.Where(x => x.employeeId == empId && x.type == type).Include(x=> x.division).Include(x=>x.district).Include(x=>x.thana).FirstOrDefaultAsync();
        }

        #endregion

        #region EducationalQualification

        public async Task<bool> DeleteEducationalQualificationById(int id)
        {
            _context.educationalQualifications.Remove(_context.educationalQualifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationalQualification>> GetAllEducationalQualification()
        {
            return await _context.educationalQualifications.AsNoTracking().ToListAsync();
        }

        public async Task<EducationalQualification> GetEducationalQualificationById(int id)
        {
            return await _context.educationalQualifications.FindAsync(id);
        }
      
        public async Task<bool> SaveEducationalQualification(EducationalQualification educationalQualification)
        {
            if (educationalQualification.Id != 0)
                _context.educationalQualifications.Update(educationalQualification);
            else
                _context.educationalQualifications.Add(educationalQualification);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationalQualification>> GetEducationalQualificationByEmpId(int empId)
        {
            return await _context.educationalQualifications.Where(x => x.employeeId == empId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EducationalQualification>> GetEducationalQualificationListByEmpId(int? empId)
        {
            return await _context.educationalQualifications.Where(x => x.employeeId == empId).Include(x => x.result).Include(x => x.degree).Include(x => x.reldegreesubject).Include(x => x.organization).Include(x => x.degree.levelofeducation).Include(x => x.reldegreesubject.subject).AsNoTracking().ToListAsync();
        }
        #endregion


        #region Photograph

        public async Task<bool> SaveImage(Photograph photograph)
        {
            if (photograph.Id != 0)
                _context.photographs.Update(photograph);
            else
                _context.photographs.Add(photograph);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Photograph>> GetPhotograph()
        {
            return await _context.photographs.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Photograph>> GetPhotographByEmpId(int empId)
        {
            return await _context.photographs.Where(x => x.employeeId == empId).AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.photographs.Remove(_context.photographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Photograph> GetPhotographByTypeAndEmpId(int empId, string type)
        {
            return await _context.photographs.Where(x => x.employeeId == empId && x.type == type).AsNoTracking().FirstOrDefaultAsync();
        }

       



        #endregion
    }
}
