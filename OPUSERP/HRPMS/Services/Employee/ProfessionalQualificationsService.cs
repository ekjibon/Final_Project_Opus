using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class ProfessionalQualificationsService: IProfessionalQualificationsService
    {
        private readonly ERPDbContext _context;

        public ProfessionalQualificationsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProfessionalQualificationsById(int id)
        {
            _context.professionalQualifications.Remove(_context.professionalQualifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProfessionalQualifications>> GetProfessionalQualifications()
        {
            return await _context.professionalQualifications.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProfessionalQualifications>> GetProfessionalQualificationsByEmpId(int empId)
        {
            return await _context.professionalQualifications.Where(x => x.employeeID == empId).Include(x => x.result).Include(x => x.qualificationHead).AsNoTracking().ToListAsync();
        }

        public async Task<ProfessionalQualifications> GetProfessionalQualificationsById(int id)
        {
            return await _context.professionalQualifications.FindAsync(id);
        }

        public async Task<bool> SaveProfessionalQualifications(ProfessionalQualifications professionalQualifications)
        {
            if (professionalQualifications.Id != 0)
                _context.professionalQualifications.Update(professionalQualifications);
            else
                _context.professionalQualifications.Add(professionalQualifications);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
