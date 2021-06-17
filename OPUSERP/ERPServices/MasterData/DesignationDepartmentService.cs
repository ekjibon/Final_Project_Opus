using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData
{
    public class DesignationDepartmentService : IDesignationDepartmentService
    {
        private readonly ERPDbContext _context;

        public DesignationDepartmentService(ERPDbContext context)
        {
            _context = context;
        }

        //Designation
        public async Task<bool> DeleteDesignationById(int id)
        {
            _context.designations.Remove(_context.designations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Designation>> GetDesignations()
        {
            return await _context.designations.AsNoTracking().ToListAsync();
        }

        public async Task<Designation> GetDesignationById(int id)
        {
            return await _context.designations.FindAsync(id);
        }

        public async Task<bool> SaveDesignation(Designation designation)
        {
            if (designation.Id != 0)
                _context.designations.Update(designation);
            else
                _context.designations.Add(designation);

            return 1 == await _context.SaveChangesAsync();
        }

        //Department
        public async Task<bool> DeleteDepartmentById(int id)
        {
            _context.departments.Remove(_context.departments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await _context.departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _context.departments.FindAsync(id);
        }

        public async Task<bool> SaveDepartment(Department department)
        {
            if(department.Id != 0)
                _context.departments.Update(department);
            else
                _context.departments.Add(department);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
