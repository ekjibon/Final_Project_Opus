using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<int> SaveEmployeeInfo(EmployeeInfo employeeInfo);
        Task<bool> UpdateEmployeeinfoById(int id);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfo();
        Task<IEnumerable<EmployeeInfo>> GetActiveAllEmployeeInfo();
        Task<EmployeeInfo> GetEmployeeInfoById(int id);
        Task<bool> DeleteEmployeeInfoById(int id);
        Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId);
        Task<EmployeeInfo> GetEmployeeInfoByCode(string code);
        Task<EmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code,string orgType);
        Task<EmployeeInfo> GetFreeEmployeeByCode(string code);
        Task<string> GetEmployeeNameCodeById(int Id);
        Task<bool> UpdateEmployee(int Id, string authId, string org);
        Task<bool> ApproveEmployeeinfoById(int id);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByOrg(string org);
        Task<string> GetAuthCodeByUserId(int empId);
        Task<IEnumerable<EmployeeInfo>> GetDuplicateEmpCode(int empId, string empCode);
        Task<int> IsThisEmpIDPresent(string employeeId);
        Task<IEnumerable<AllEmployeeJson>> GetActiveEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllEmployeeJson>> GetInactiveEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleSingle(string queryString, string org, string empode);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleApprove(string queryString, string org, string empode);
        Task<IEnumerable<PeerSearchViewModel>> GetEmployeeInfoBySearch(string searchKey);
        //newly added
        Task<IEnumerable<AllEmployeeJson>> GetAllEmployeeInfo(int empStatus, string org);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoSingle(int queryString, string org, string empode);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoList();
        // for DashBoard
        Task<IEnumerable<EmployeeInfo>> PRLInNextSixMonthByOrg(string org);
        Task<IEnumerable<EmployeeInfo>> LeaveInLastOneMonthByOrg(string org);
        Task<EmployeeInfo> GetEmployeeInfoByApplicationId(string userId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROAnalyst();
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROTeamLeader();
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROAnalystByTeamId(int teamId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROReview();
        Task<string> GetEmployeeIDByAuthID(string empId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCRMAnalyst();
        Task<IEnumerable<AllEmployeeJson>> GetAllEmployeeInfoJson();
        Task<IEnumerable<AllEmployeeJson>> GetAllInactiveEmployeeInfoJson();
        Task<AspNetUsersViewModel> GetUserInfoByEmpCode(string code);
        Task<string> GetCompanyIdByEmpId(int empId);

        //Visual Data
        Task<string> GetEmpCodeNameVisualData();
    }
}
