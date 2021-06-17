using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPService.AuthService.Interfaces
{
    public interface IUserInfoes
    {
        Task<EmployeeInfoViewModel> GetEmployeeInfobyUser(string userName);
        Task<IEnumerable<UserType>> GetUserTypeList();
        Task<IEnumerable<ApplicationUser>> GetUserInfoListByUser(string userName);
        Task<AspNetUsersViewModel> GetUserInfoByUser(string userName);
        Task<ApplicationUser> GetUserBasicInfoes(string userName);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfo();
        Task<int> SaveDailyProgressReport(DailyProgressReport dailyProgress);
        Task<int> GetMaxUserId();
        Task<IEnumerable<AspNetUsersViewModel>> GetUsersByEmployeeInfo();
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoByModule(int moduleId);
        Task<AspNetUsersViewModel> GetUserInfoByUserId(int? UserId);
        Task<IEnumerable<ERPModule>> GetAllERPModule();
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoList();
        Task<IEnumerable<AspNetUsersViewModel>> GetAllActiveEmployeeInfo();
        Task<IEnumerable<AspNetUsersViewModel>> GetAllActiveEmployeeInfoForOwnerChange();
        Task<AspNetUsersViewModel> GetSbuIdByEmployeeEmail(string emailId);
        Task<EmployeeInfo> GetemployeebyempCode(string empcode);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoListForProxyAdmin(string userRoleId, string userName);
        Task<UpdateAspnetUser> UpdateAspNetUserByUserIdAndStatus(string userId, int status);
        Task<UpdateAspnetUser> DeleteAspNetUserByUserId(string userId, string userName);
        Task<IEnumerable<string>> GetRoleListByUserId(string Id);
        Task<bool> DeleteUserRoleListByUserId(string Id);
        Task<IEnumerable<UserBackup>> GetUserBackupList();
        Task<IEnumerable<UserBackUpViewModel>> GetUserBackupListWithEmp();
        Task<bool> DeleteRoleById(string Id);
        Task<bool> DeleteMenuByRoleId(string Id);
        Task<EmployeeInfo> Getemployeebyname(string empcode);
        Task<IEnumerable<AspNetUsersApproverViewModel>> GetUsersApproverByEmployeeInfo();
        Task<AspNetUsersViewModel> GetUserInfoWinthEmpByUser(string userName);
    }
}
