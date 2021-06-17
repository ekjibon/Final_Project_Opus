using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.MasterData
{
    public class EmployeeTypeService : ITypeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeType()
        {
            return await _context.employeeTypes.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetEmployeeTypeCheck(int id, string name)
        {
            var EmployeeType = await _context.employeeTypes.Where(x => x.empType == name && x.Id != id).FirstOrDefaultAsync();
            if (EmployeeType == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
    }
}
