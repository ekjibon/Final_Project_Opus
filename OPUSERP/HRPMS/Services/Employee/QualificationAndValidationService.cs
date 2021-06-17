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
    public class QualificationAndValidationService: IQualificationAndValidationService
    {
        private readonly ERPDbContext _context;

        public QualificationAndValidationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteQualificationAndValidationById(int id)
        {
            _context.qualificationAndValidations.Remove(_context.qualificationAndValidations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QualificationAndValidation>> GetQualificationAndValidation()
        {
            return await _context.qualificationAndValidations.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<QualificationAndValidation>> GetQualificationAndValidationByEmpId(int empId)
        {
            return await _context.qualificationAndValidations.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<QualificationAndValidation> GetQualificationAndValidationById(int id)
        {
            return await _context.qualificationAndValidations.FindAsync(id);
        }

        public async Task<bool> SaveQualificationAndValidation(QualificationAndValidation qualificationAndValidation)
        {
            if (qualificationAndValidation.Id != 0)
                _context.qualificationAndValidations.Update(qualificationAndValidation);
            else
                _context.qualificationAndValidations.Add(qualificationAndValidation);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
