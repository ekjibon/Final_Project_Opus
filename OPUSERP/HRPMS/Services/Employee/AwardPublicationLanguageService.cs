using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class AwardPublicationLanguageService : IAwardPublicationLanguageService
    {
        private readonly ERPDbContext _context;

        public AwardPublicationLanguageService(ERPDbContext context)
        {
            _context = context;
        }

        //Award;
        public async Task<bool> SaveAward(EmployeeAward employeeAward)
        {
            if (employeeAward.Id != 0)
                _context.employeeAwards.Update(employeeAward);
            else
            _context.employeeAwards.Add(employeeAward);
            
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAwardById(int id)
        {
            _context.employeeAwards.Remove(await _context.employeeAwards.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeAward>> GetAward()
        {
            return await _context.employeeAwards.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeAward> GetAwardById(int id)
        {
            return await _context.employeeAwards.FindAsync(id);
        }

        public async Task<IEnumerable<EmployeeAward>> GetAwardsByEmpId(int empId)
        {
            return await _context.employeeAwards.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }


        //publications..
        public async Task<IEnumerable<Publication>> GetPublication()
        {
            return await _context.publications.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePublicationById(int id)
        {
            _context.publications.Remove(await _context.publications.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Publication> GetPublicationById(int id)
        {
            return await _context.publications.FindAsync(id);
        }

        public async Task<bool> SavePublication(Publication publication)
        {
            if (publication.Id != 0)
                _context.publications.Update(publication);
            else
                _context.publications.Add(publication);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publication>> GetPublicationsByEmpId(int empId)
        {
            return await _context.publications.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }


        //Language
        public async Task<bool> SaveLanguage(EmployeeLanguage employeeLanguage)
        {
            if (employeeLanguage.Id != 0)
                _context.employeeLanguages.Update(employeeLanguage);
            else
                _context.employeeLanguages.Add(employeeLanguage);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeLanguage>> GetLanguage()
        {
            return await _context.employeeLanguages.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeLanguage> GetLanguageById(int id)
        {
            return await _context.employeeLanguages.FindAsync(id);
        }

        public async Task<bool> DeleteLanguageById(int id)
        {
            _context.employeeLanguages.Remove(await _context.employeeLanguages.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeLanguage>> GetLanguageByEmpId(int empId)
        {
            return await _context.employeeLanguages.Where(x => x.employeeId == empId).Include(x=> x.language).AsNoTracking().ToListAsync();
        }
    }
}
