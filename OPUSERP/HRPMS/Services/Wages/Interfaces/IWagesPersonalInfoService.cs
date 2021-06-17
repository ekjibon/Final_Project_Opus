using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesPersonalInfoService
    {
        Task<int> SaveEmployeeInfo(WagesEmployeeInfo employeeInfo);
        Task<bool> UpdateEmployeeinfoById(int id);
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfo();
        Task<WagesEmployeeInfo> GetEmployeeInfoById(int id);
        Task<bool> DeleteEmployeeInfoById(int id);
        Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId);
        Task<WagesEmployeeInfo> GetEmployeeInfoByCode(string code);
        Task<WagesEmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code,string orgType);
        Task<WagesEmployeeInfo> GetFreeEmployeeByCode(string code);
        Task<string> GetEmployeeNameCodeById(int Id);
        Task<bool> UpdateEmployee(int Id, string authId, string org);
        Task<bool> ApproveEmployeeinfoById(int id);
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoByOrg(string org);
        Task<string> GetAuthCodeByUserId(int empId);

        Task<int> IsThisEmpIDPresent(string employeeId);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleSingle(string queryString, string org, string empode);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleApprove(string queryString, string org, string empode);
        // for DashBoard
        Task<IEnumerable<WagesEmployeeInfo>> PRLInNextSixMonthByOrg(string org);
        Task<IEnumerable<WagesEmployeeInfo>> LeaveInLastOneMonthByOrg(string org);
        Task<WagesEmployeeInfo> GetEmployeeInfoByApplicationId(string userId);
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROAnalyst();
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROTeamLeader();
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROAnalystByTeamId(int teamId);
        Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROReview();
        Task<bool> UpdateEmployeeinfoInactiveById(int id);
        Task<bool> UpdateEmployeeinfoActiveById(int id);


    }
}
