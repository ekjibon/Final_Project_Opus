using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class PassportInfoService: IPassportInfoService
    {
        private readonly ERPDbContext _context;

        public PassportInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePassportInfo(PassportDetails passportDetail)
        {
            if (passportDetail.Id != 0)
                _context.passportDetails.Update(passportDetail);
            else
                _context.passportDetails.Add(passportDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassportDetails>> GetPassportInfo()
        {
            return await _context.passportDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PassportDetails> GetPassportInfoById(int id)
        {
            return await _context.passportDetails.FindAsync(id);
        }

        public async Task<bool> DeletePassportInfoById(int id)
        {
            _context.passportDetails.Remove(_context.passportDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassportDetails>> GetPassportInfoByEmpId(int empId)
        {
            return await _context.passportDetails.Include(x=>x.employee).Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
