using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class EmployeeTypeService : ITypeService
    {
        private readonly ERPDbContext _context;

        public EmployeeTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeType()
        {
            return await _context.employeeTypes.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeType> GetEmployeeTypeById(int id)
        {
            return await _context.employeeTypes.FindAsync(id);
        }

        public async Task<bool> SaveEmployeeType(EmployeeType employeeType)
        {
            if(employeeType.Id != 0)
                _context.employeeTypes.Update(employeeType);
            else
                _context.employeeTypes.Add(employeeType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeTypeById(int id)
        {
            _context.employeeTypes.Remove(_context.employeeTypes.Find(id));
            return 1 ==  await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<string>> GetTypNamesByIds(int[] ids)
        {
           return await _context.employeeTypes.Where(x => ids.Contains(x.Id)).Select(x => x.empType).ToListAsync();
        }
    }
}
