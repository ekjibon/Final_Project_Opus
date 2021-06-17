using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;

namespace OPUSERP.CRM.Services.MasterData
{
    public class CRMDesignationDepartmentService : ICRMDesignationDepartmentService
    {
        private readonly ERPDbContext _context;

        public CRMDesignationDepartmentService(ERPDbContext context)
        {
            _context = context;
        }

        //Designation
        public async Task<bool> DeleteDesignationById(int id)
        {
            _context.designations.Remove(_context.designations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CRMDesignation>> GetDesignations()
        {
            return await _context.CRMDesignations.AsNoTracking().ToListAsync();
        }

        public async Task<CRMDesignation> GetDesignationById(int id)
        {
            return await _context.CRMDesignations.FindAsync(id);
        }

        public async Task<CRMDesignation> GetDesignationIdByName(string name)
        {
            return await _context.CRMDesignations.Where(x => x.designationName == name).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveDesignation(CRMDesignation designation)
        {
            if (designation.Id != 0)
                _context.CRMDesignations.Update(designation);
            else
                _context.CRMDesignations.Add(designation);

            return 1 == await _context.SaveChangesAsync();
        }

        //Department
        public async Task<bool> DeleteDepartmentById(int id)
        {
            _context.CRMDepartments.Remove(_context.CRMDepartments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CRMDepartment>> GetDepartment()
        {
            return await _context.CRMDepartments.AsNoTracking().ToListAsync();
        }

        public async Task<CRMDepartment> GetDepartmentById(int id)
        {
            return await _context.CRMDepartments.FindAsync(id);
        }

        public async Task<bool> SaveDepartment(CRMDepartment department)
        {
            if(department.Id != 0)
                _context.CRMDepartments.Update(department);
            else
                _context.CRMDepartments.Add(department);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
