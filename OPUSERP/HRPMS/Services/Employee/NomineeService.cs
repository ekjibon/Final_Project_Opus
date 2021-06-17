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
    public class NomineeService : INomineeService
    {
        private readonly ERPDbContext _context;

        public NomineeService(ERPDbContext context)
        {
            _context = context;
        }

        #region Nominee

        public async Task<bool> DeleteNomineeById(int id)
        {
            _context.nominees.Remove(_context.nominees.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Nominee>> GetNominee()
        {
            return await _context.nominees.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Nominee>> GetNomineeByEmpId(int empId)
        {
            return await _context.nominees.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<Nominee> GetNomineeById(int id)
        {
            return await _context.nominees.FindAsync(id);
        }

        public async Task<int> SaveNominee(Nominee nominee)
        {
            if (nominee.Id != 0)
                _context.nominees.Update(nominee);
            else
                _context.nominees.Add(nominee);

            await _context.SaveChangesAsync();
            return nominee.Id;
        }

        #endregion

        #region Employee Insurance

        public async Task<bool> DeleteEmployeeInsuranceById(int id)
        {
            _context.employeeInsurances.Remove(_context.employeeInsurances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }       

        public async Task<IEnumerable<EmployeeInsurance>> GetEmployeeInsuranceByEmpId(int empId)
        {
            return await _context.employeeInsurances.Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }      

        public async Task<int> SaveEmployeeInsurance(EmployeeInsurance employeeInsurance)
        {
            if (employeeInsurance.Id != 0)
                _context.employeeInsurances.Update(employeeInsurance);
            else
                _context.employeeInsurances.Add(employeeInsurance);

            await _context.SaveChangesAsync();
            return employeeInsurance.Id;
        }

        #endregion

    }
}
