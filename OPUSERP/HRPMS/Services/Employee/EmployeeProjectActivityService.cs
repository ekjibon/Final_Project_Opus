using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class EmployeeProjectActivityService: IEmployeeProjectActivityService
    {
        private readonly ERPDbContext _context;

        public EmployeeProjectActivityService(ERPDbContext context)
        {
            _context = context;
        }

        #region Employee Project Activity
        public async Task<bool> SaveEmployeeProjectActivity(EmployeeProjectActivity employeeProjectActivity)
        {
            if (employeeProjectActivity.Id != 0)
                _context.employeeProjectActivities.Update(employeeProjectActivity);
            else
                _context.employeeProjectActivities.Add(employeeProjectActivity);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeProjectActivity>> GetEmployeeProjectActivity()
        {
            return await _context.employeeProjectActivities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProjectActivity>> GetEmployeeProjectActivityByEmpId(int empId)
        {
            return await _context.employeeProjectActivities.Include(x=>x.employee).Include(x=>x.hRProject).Include(x=>x.hRDoner).Include(x=>x.hRActivity).Where(x=>x.employeeId==empId).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeProjectActivity> GetEmployeeProjectActivityById(int id)
        {

            return await _context.employeeProjectActivities.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeeProjectActivityById(int id)
        {
            _context.employeeProjectActivities.Remove(_context.employeeProjectActivities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region  Project Assign
        public async Task<bool> SaveEmployeeProjectAssign(EmployeeProjectAssign employeeProjectAssign)
        {
            if (employeeProjectAssign.Id != 0)
                _context.employeeProjectAssigns.Update(employeeProjectAssign);
            else
                _context.employeeProjectAssigns.Add(employeeProjectAssign);
            return 1 == await _context.SaveChangesAsync();
        }

        ////public async Task<IEnumerable<EmployeeProjectAssign>> GetEmployeeProjectAssign()
        //////{
        //////    return await _context.employeeProjectAssigns.AsNoTracking().ToListAsync();
        //////}

        public async Task<IEnumerable<EmployeeProjectAssign>> GetEmployeeProjectAssignByEmpId(int empId)
        {
            return await _context.employeeProjectAssigns.Include(x => x.employee).Include(x => x.project).Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllRunningProject()
        {
            return await _context.Projects.Where(x=>x.projectStatus== "Running").AsNoTracking().ToListAsync();
        }

        //public async Task<EmployeeProjectAssign> GetEmployeeProjectAssignById(int id)
        //{

        //    return await _context.employeeProjectAssigns.FindAsync(id);
        //}

        public async Task<bool> DeleteEmployeeProjectAssignById(int id)
        {
            _context.employeeProjectAssigns.Remove(_context.employeeProjectAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Employee Other Info
        public async Task<bool> SaveEmployeeOtherInfo(EmployeeOtherInfo employeeOtherInfo)
        {
            if (employeeOtherInfo.Id != 0)
                _context.employeeOtherInfos.Update(employeeOtherInfo);
            else
                _context.employeeOtherInfos.Add(employeeOtherInfo);
            return 1 == await _context.SaveChangesAsync();
        }       

        public async Task<IEnumerable<EmployeeOtherInfo>> GetEmployeeOtherInfoByEmpId(int empId)
        {
            return await _context.employeeOtherInfos.Include(x => x.employeeInfo).Include(x => x.hRFacility).Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeOtherInfo> GetEmployeeOtherInfoById(int id)
        {
            return await _context.employeeOtherInfos.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeeOtherInfoById(int id)
        {
            _context.employeeOtherInfos.Remove(_context.employeeOtherInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Employee Cost Centre
        public async Task<bool> SaveEmployeeCostCentre(EmployeeCostCentre employeeCostCentre)
        {
            if (employeeCostCentre.Id != 0)
                _context.employeeCostCentres.Update(employeeCostCentre);
            else
                _context.employeeCostCentres.Add(employeeCostCentre);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeCostCentre>> GetEmployeeCostCentreByEmpId(int empId)
        {
            return await _context.employeeCostCentres.Include(x => x.employeeInfo).Include(x => x.costCentre).Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteEmployeeCostCentreById(int id)
        {
            _context.employeeCostCentres.Remove(_context.employeeCostCentres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Employee Grade
        public async Task<bool> SaveEmployeeGrade(EmployeeGrade employeeGrade)
        {
            if (employeeGrade.Id != 0)
                _context.employeeGrades.Update(employeeGrade);
            else
                _context.employeeGrades.Add(employeeGrade);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeGrade>> GetEmployeeGradeByEmpId(int empId)
        {
            return await _context.employeeGrades.Include(x => x.employeeInfo).Include(x => x.salaryGrade).Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteEmployeeGradeById(int id)
        {
            _context.employeeGrades.Remove(_context.employeeGrades.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
