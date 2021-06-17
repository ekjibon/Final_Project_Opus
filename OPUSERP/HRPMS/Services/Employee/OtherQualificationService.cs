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
    public class OtherQualificationService: IOtherQualificationService
    {
        private readonly ERPDbContext _context;

        public OtherQualificationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOtherQualificationById(int id)
        {
            _context.otherQualifications.Remove(_context.otherQualifications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OtherQualification>> GetOtherQualification()
        {
            return await _context.otherQualifications.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OtherQualification>> GetOtherQualificationByEmpId(int empId)
        {
            return await _context.otherQualifications.Where(x => x.employeeID == empId).Include(x => x.result).Include(x => x.otherQualificationHead).AsNoTracking().ToListAsync();
        }

        public async Task<OtherQualification> GetOtherQualificationById(int id)
        {
            return await _context.otherQualifications.FindAsync(id);
        }

        public async Task<bool> SaveOtherQualification(OtherQualification otherQualification)
        {
            if (otherQualification.Id != 0)
                _context.otherQualifications.Update(otherQualification);
            else
                _context.otherQualifications.Add(otherQualification);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
