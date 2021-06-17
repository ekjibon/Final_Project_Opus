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
    public class EmployeeContractInfoService: IEmployeeContratInfoService
    {
        private readonly ERPDbContext _context;

        public EmployeeContractInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletContractInfoById(int id)
        {
            _context.EmployeeContractInfos.Remove(_context.EmployeeContractInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeContractInfo>> GetContractInfoByEmpId(int empId)
        {
            return await _context.EmployeeContractInfos.Where(x=>x.employeeId==empId).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeContractInfo> GetContractInfoById(int id)
        {
            return await _context.EmployeeContractInfos.FindAsync(id);
        }

        public async Task<bool> SaveContractInfo(EmployeeContractInfo contractInfo)
        {
            if (contractInfo.Id != 0)
                _context.EmployeeContractInfos.Update(contractInfo);
            else
                _context.EmployeeContractInfos.Add(contractInfo);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
