using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.HRPMS.Services.Employee
{
    public class WagesPersonalInfoService : IWagesPersonalInfoService
    {
        private readonly ERPDbContext _context;

        public WagesPersonalInfoService(ERPDbContext context)
        {
            _context = context;
        }

        //EmployeeInfo
        public async Task<bool> DeleteEmployeeInfoById(int id)
        {
            _context.wagesEmployeeInfos.Remove(_context.wagesEmployeeInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.wagesEmployeeInfos.Include(x => x.department).ToListAsync();
        }
        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROAnalyst()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 13 && x.teamId != null).Select(x => x.aspnetuserId).ToList();
            return await _context.wagesEmployeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROAnalystByTeamId(int teamId)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.teamId == teamId).Select(x => x.aspnetuserId).ToList();
            return await _context.wagesEmployeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROTeamLeader()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 13 && x.teamId == null).Select(x => x.aspnetuserId).ToList();
            return await _context.wagesEmployeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoCROReview()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 13).Select(x => x.aspnetuserId).ToList();
            return await _context.wagesEmployeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }

        public async Task<WagesEmployeeInfo> GetEmployeeInfoById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.wagesEmployeeInfos.Include(x => x.department).Include(x => x.religion).Include(x => x.employeeType).Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> SaveEmployeeInfo(WagesEmployeeInfo employeeInfo)
        {
            if (employeeInfo.Id != 0)
                _context.wagesEmployeeInfos.Update(employeeInfo);
            else
                _context.wagesEmployeeInfos.Add(employeeInfo);
            await _context.SaveChangesAsync();
            return employeeInfo.Id;
        }

        public async Task<bool> UpdateEmployeeinfoById(int id)
        {
            WagesEmployeeInfo employeeInfo = await _context.wagesEmployeeInfos.FindAsync(id);
            if (employeeInfo != null)
            {
                employeeInfo.isApproved = 0;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<bool> UpdateEmployeeinfoInactiveById(int id)
        {
            WagesEmployeeInfo employeeInfo = await _context.wagesEmployeeInfos.FindAsync(id);
            if (employeeInfo != null)
            {
                employeeInfo.activityStatus = 0;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<bool> UpdateEmployeeinfoActiveById(int id)
        {
            WagesEmployeeInfo employeeInfo = await _context.wagesEmployeeInfos.FindAsync(id);
            if (employeeInfo != null)
            {
                employeeInfo.activityStatus = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<bool> ApproveEmployeeinfoById(int id)
        {
            WagesEmployeeInfo employeeInfo = await _context.wagesEmployeeInfos.FindAsync(id);
            if (employeeInfo != null)
            {
                employeeInfo.isApproved = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<WagesEmployeeInfo> GetEmployeeInfoByCode(string code)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.employeeCode == code).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<WagesEmployeeInfo> GetEmployeeInfoByApplicationId(string userId)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.ApplicationUserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId)
        {
            return await _context.employeeWithDesignations.FromSql($"sp_GetEmployeeDetailsList @p0,@p1", empId, string.Empty).ToListAsync();
        }

        public async Task<string> GetEmployeeNameCodeById(int Id)
        {
            WagesEmployeeInfo data = await _context.wagesEmployeeInfos.FindAsync(Id);
            return data.nameEnglish + "-" + data.employeeCode;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<WagesEmployeeInfo> queryData = _context.wagesEmployeeInfos.Where(x => x.orgType == org && x.activityStatus == 1);

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    }
                    else if (SepToken[0] == "PRL")
                    {
                        DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
                        DateTime now = DateTime.Now;
                        queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) <= nowAndEx && DateTime.Parse(x.LPRDate) >= now));
                    }
                }
            }
            #endregion

            #region Result Process
            IEnumerable<WagesEmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (WagesEmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/WagesEditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/WagesIndex/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/Wagespdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleSingle(string queryString, string org, string empode)
        {
            IQueryable<WagesEmployeeInfo> queryData = _context.wagesEmployeeInfos.Where(x => x.orgType == org && x.employeeCode == empode);

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    }
                    else if (SepToken[0] == "PRL")
                    {
                        DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
                        DateTime now = DateTime.Now;
                        queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) <= nowAndEx && DateTime.Parse(x.LPRDate) >= now));
                    }
                }
            }


            #endregion

            #region Result Process
            IEnumerable<WagesEmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (WagesEmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/WagesEditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/WagesIndex/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/Wagespdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleApprove(string queryString, string org, string empode)
        {
            WagesEmployeeInfo employee = await _context.wagesEmployeeInfos.Where(x => x.employeeCode == empode).FirstOrDefaultAsync();
            List<int?> empId = await _context.approvalDetails.Where(x => x.approverId == employee.Id).Select(x => x.approvalMaster.employeeInfoId).ToListAsync();
            IQueryable<WagesEmployeeInfo> queryData = _context.wagesEmployeeInfos.Where(x => x.orgType == org && x.isApproved == 0 && empId.Contains(x.Id));

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    }
                    else if (SepToken[0] == "PRL")
                    {
                        DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
                        DateTime now = DateTime.Now;
                        queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) <= nowAndEx && DateTime.Parse(x.LPRDate) >= now));
                    }
                }
            }


            #endregion

            #region Result Process
            IEnumerable<WagesEmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (WagesEmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Approve'  href='/HRPMSEmployee/Info/ApproveInfo/{employeeInfo.Id}'><i class='fa fa-check'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/WagesIndex/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/Wagespdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<WagesEmployeeInfo> GetFreeEmployeeByCode(string code)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.employeeCode == code && (x.ApplicationUserId == null || x.ApplicationUserId == "" || x.ApplicationUserId == "0")).FirstAsync();
        }

        public async Task<bool> UpdateEmployee(int Id, string authId, string org)
        {
            WagesEmployeeInfo data = await _context.wagesEmployeeInfos.FindAsync(Id);

            if (data == null) return false;
            data.ApplicationUserId = authId;
            data.orgType = org;

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesEmployeeInfo>> GetEmployeeInfoByOrg(string org)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.orgType == org && x.activityStatus == 1).AsNoTracking().ToListAsync();
        }

        public async Task<WagesEmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code, string orgType)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.employeeCode == code).Where(x => x.orgType == orgType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string> GetAuthCodeByUserId(int empId)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.Id == empId).AsNoTracking().Select(x => x.ApplicationUserId).FirstOrDefaultAsync();
        }

        public async Task<int> IsThisEmpIDPresent(string employeeId)
        {
            return await _context.wagesEmployeeInfos.Where(x => x.employeeCode == employeeId).AsNoTracking().Select(x => x.Id).FirstOrDefaultAsync();
        }

        //Dashboard 

        public async Task<IEnumerable<WagesEmployeeInfo>> PRLInNextSixMonthByOrg(string org)
        {
            DateTime frm = DateTime.Now;
            DateTime to = frm.AddMonths(6);
            return await _context.wagesEmployeeInfos.Where(x => x.orgType == org && (DateTime.Parse(x.LPRDate) <= to && DateTime.Parse(x.LPRDate) >= frm)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesEmployeeInfo>> LeaveInLastOneMonthByOrg(string org)
        {
            DateTime to = DateTime.Now;
            DateTime frm = to.AddMonths(-1);
            List<int> ids = await _context.leaveLogs.Where(x => x.leaveFrom >= frm && x.leaveFrom <= to).AsNoTracking().Select(x => (int)x.employeeId).ToListAsync();
            return await _context.wagesEmployeeInfos.Where(x => x.orgType == org && ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }
    }
}
