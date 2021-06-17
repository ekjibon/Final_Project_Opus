using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IEmployeeProjectActivityService
    {
        #region Employee Project Activity
        Task<bool> SaveEmployeeProjectActivity(EmployeeProjectActivity employeeProjectActivity);
        Task<IEnumerable<EmployeeProjectActivity>> GetEmployeeProjectActivity();
        Task<EmployeeProjectActivity> GetEmployeeProjectActivityById(int id);
        Task<bool> DeleteEmployeeProjectActivityById(int id);
        Task<IEnumerable<EmployeeProjectActivity>> GetEmployeeProjectActivityByEmpId(int empId);
        #endregion

        #region  Project Assign
        Task<IEnumerable<EmployeeProjectAssign>> GetEmployeeProjectAssignByEmpId(int empId);
        Task<IEnumerable<Project>> GetAllRunningProject();
        Task<bool> SaveEmployeeProjectAssign(EmployeeProjectAssign employeeProjectAssign);
        Task<bool> DeleteEmployeeProjectAssignById(int id);

        #endregion

        #region Employee Other Info

        Task<bool> SaveEmployeeOtherInfo(EmployeeOtherInfo employeeOtherInfo);        
        Task<EmployeeOtherInfo> GetEmployeeOtherInfoById(int id);
        Task<bool> DeleteEmployeeOtherInfoById(int id);
        Task<IEnumerable<EmployeeOtherInfo>> GetEmployeeOtherInfoByEmpId(int empId);
        #endregion

        #region Employee Cost Centre

        Task<bool> SaveEmployeeCostCentre(EmployeeCostCentre employeeCostCentre);
        Task<bool> DeleteEmployeeCostCentreById(int id);
        Task<IEnumerable<EmployeeCostCentre>> GetEmployeeCostCentreByEmpId(int empId);

        #endregion

        #region  Employee Grade

        Task<bool> SaveEmployeeGrade(EmployeeGrade employeeGrade);
        Task<bool> DeleteEmployeeGradeById(int id);
        Task<IEnumerable<EmployeeGrade>> GetEmployeeGradeByEmpId(int empId);

        #endregion
    }
}
