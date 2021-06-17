using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Shagotom.Services.MasterData.Interfaces;

namespace OPUSERP.Shagotom.Services.MasterData
{
    public class EmployeeInfoServiceForShagotom : IEmployeeInfoServiceForShagotom
    {
        private readonly ERPDbContext _context;


        public EmployeeInfoServiceForShagotom(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeeInfo()
        {
            return await _context.employeeInfos.ToListAsync();
        }

        public async Task<VisitorEntryViewModel> GetEmployeeInfoById(int id)
        {

            Photograph photograph = await _context.photographs.Where(x => x.employeeId == id).FirstOrDefaultAsync();
            EmployeeInfo e = await _context.employeeInfos.Where(x => x.Id == id).Include(x=>x.department).FirstOrDefaultAsync();

            VisitorEntryViewModel emp = new VisitorEntryViewModel();

            emp.Id = e.Id;
            emp.employeeCode = e.employeeCode;
            emp.emailAddress = e.emailAddress;
            emp.nameEnglish = e.nameEnglish;
            emp.mobileNumberPersonal = e.mobileNumberPersonal;
            emp.department = e.department.deptName;
            emp.designation = e.designation;

            if (photograph != null)
            {
                emp.imgUrl = photograph.url;
            }
            
            return emp;
            
        }

    }
}
