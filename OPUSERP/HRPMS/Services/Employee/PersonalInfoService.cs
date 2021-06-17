using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Auth.Models;
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
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ERPDbContext _context;

        public PersonalInfoService(ERPDbContext context)
        {
            _context = context;
        }

        //EmployeeInfo

        public async Task<IEnumerable<PeerSearchViewModel>> GetEmployeeInfoBySearch(string searchKey)
        {
            return await _context.peerSearchViewModels.FromSql($"GetQuickSearchEmpInfo {searchKey}").AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteEmployeeInfoById(int id)
        {
            _context.employeeInfos.Remove(_context.employeeInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.employeeInfos.Include(x => x.department).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetActiveAllEmployeeInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.employeeInfos.Include(x => x.department).Where(a => a.activityStatus == 1).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROAnalyst()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 13 && x.teamId != null).Select(x => x.aspnetuserId).ToList();
            return await _context.employeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCRMAnalyst()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 2 && x.teamId != null).Select(x => x.aspnetuserId).ToList();
            return await _context.employeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROAnalystByTeamId(int teamId)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.teamId == teamId).Select(x => x.aspnetuserId).ToList();
            return await _context.employeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROTeamLeader()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x=>x.moduleId==13 && x.teamId==null).Select(x => x.aspnetuserId).ToList();
            return await _context.employeeInfos.Include(x => x.department).Where(x=> leader.Contains(x.ApplicationUserId)).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoCROReview()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            List<string> leader = _context.Teams.Where(x => x.moduleId == 13).Select(x => x.aspnetuserId).ToList();
            return await _context.employeeInfos.Include(x => x.department).Where(x => leader.Contains(x.ApplicationUserId)).ToListAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeInfoById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.employeeInfos.Include(x=>x.department).Include(x=> x.religion).Include(x=> x.employeeType).Include(x => x.employeeStatus).Include(x => x.hrProgram).Include(x => x.hrUnit).Where(x=>x.Id == id).FirstAsync();
        }

        public async Task<int> SaveEmployeeInfo(EmployeeInfo employeeInfo)
        {
            if (employeeInfo.Id != 0)
                _context.employeeInfos.Update(employeeInfo);
            else
                _context.employeeInfos.Add(employeeInfo);
            await _context.SaveChangesAsync();
            return employeeInfo.Id;
        }

        public async Task<bool> UpdateEmployeeinfoById(int id)
        {
            EmployeeInfo employeeInfo = await _context.employeeInfos.FindAsync(id);
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
        public async Task<bool> ApproveEmployeeinfoById(int id)
        {
            EmployeeInfo employeeInfo = await _context.employeeInfos.FindAsync(id);
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

        public async Task<EmployeeInfo> GetEmployeeInfoByCode(string code)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<AspNetUsersViewModel> GetUserInfoByEmpCode(string code)
        {
            var result = (from U in _context.Users
                          join E in _context.employeeInfos on U.Id equals E.ApplicationUserId
                          where E.employeeCode == code
                          select new AspNetUsersViewModel
                          {
                              aspnetId = U.Id,
                              UserName = U.UserName,
                              Email = U.Email,
                              EmpName = E.nameEnglish
                          }).FirstOrDefaultAsync();
            return await result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoList()
        {
            return await _context.employeeInfos.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByApplicationId(string userId)
        {
            return await _context.employeeInfos.Where(x => x.ApplicationUserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId)
        {
            return await _context.employeeWithDesignations.FromSql($"sp_GetEmployeeDetailsList @p0,@p1",empId,string.Empty).ToListAsync();
        }

        public async Task<string> GetEmployeeNameCodeById(int Id)
        {
            EmployeeInfo data = await _context.employeeInfos.FindAsync(Id);
            return data.nameEnglish + "-" + data.employeeCode;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetAllEmployeeInfo(int empStatus, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org && empStatus == x.activityStatus);    

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetAllEmployeeInfoJson()
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x=>x.activityStatus == 1);

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeId = employeeInfo.Id,
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    companies = _context.Companies.FirstOrDefault().companyName,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetAllInactiveEmployeeInfoJson()
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.activityStatus == 0);

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeId = employeeInfo.Id,
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    companies = _context.Companies.FirstOrDefault().companyName,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoSingle(int empStatus, string org, string empode)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org && x.employeeCode == empode && empStatus==x.activityStatus);

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    imageUrl= await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        
        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach(string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if(SepToken.Length > 1)
                {
                    if(SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    }else if(SepToken[0] == "PRL")
                    {
                        DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
                        DateTime now = DateTime.Now;
                        queryData = queryData.Where(x => (x.LPRDate <= nowAndEx && x.LPRDate >= now));
                    }
                }
            }
            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetActiveEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org && x.activityStatus == 1);

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
                        queryData = queryData.Where(x => (x.LPRDate <= nowAndEx && x.LPRDate >= now));
                    }
                }
            }
            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetInactiveEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org && x.activityStatus == 0);

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
                        queryData = queryData.Where(x => (x.LPRDate <= nowAndEx && x.LPRDate >= now));
                    }
                }
            }
            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleSingle(string queryString, string org,string empode)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org&&x.employeeCode==empode);

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
                        queryData = queryData.Where(x => (x.LPRDate <= nowAndEx && x.LPRDate >= now));
                    }
                }
            }


            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    imageUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/HRPMSEmployee/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleApprove(string queryString, string org, string empode)
        {
            EmployeeInfo employee =await _context.employeeInfos.Where(x => x.employeeCode == empode).FirstOrDefaultAsync();
            List<int?> empId =await _context.approvalDetails.Where(x => x.approverId == employee.Id).Select(x=>x.approvalMaster.employeeInfoId).ToListAsync();
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org  && x.isApproved==0&&empId.Contains(x.Id));

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
                        queryData = queryData.Where(x => (x.LPRDate <= nowAndEx && x.LPRDate >= now));
                    }
                }
            }


            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.designation,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Approve'  href='/HRPMSEmployee/Info/ApproveInfo/{employeeInfo.Id}'><i class='fa fa-check'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<EmployeeInfo> GetFreeEmployeeByCode(string code)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code && (x.ApplicationUserId == null || x.ApplicationUserId == "" || x.ApplicationUserId =="0")).FirstAsync();
        }

        public async Task<bool> UpdateEmployee(int Id, string authId, string org)
        {
            EmployeeInfo data = await _context.employeeInfos.FindAsync(Id);

            if (data == null) return false;
            data.ApplicationUserId = authId;
            data.orgType = org;

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByOrg(string org)
        {
            return await _context.employeeInfos.Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code, string orgType)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code).Where(x=> x.orgType == orgType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string> GetAuthCodeByUserId(int empId)
        {
            return await _context.employeeInfos.Where(x => x.Id == empId).AsNoTracking().Select(x=> x.ApplicationUserId).FirstOrDefaultAsync();
        }

        public async Task<string> GetCompanyIdByEmpId(int empId)
        {
            return await _context.employeeInfos.Where(x => x.Id == empId).Select(x=>x.branch.company.companyName).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> IsThisEmpIDPresent(string employeeId)
        {
           return await _context.employeeInfos.Where(x => x.employeeCode == employeeId).AsNoTracking().Select(x=> x.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetDuplicateEmpCode(int empId, string empCode)
        {
            return await _context.employeeInfos.Where(x => x.Id != empId && x.employeeCode == empCode).AsNoTracking().ToListAsync();
        }

        //Dashboard 

        public async Task<IEnumerable<EmployeeInfo>> PRLInNextSixMonthByOrg(string org)
        {
            DateTime frm = DateTime.Now;
            DateTime to = frm.AddMonths(6);
            return await _context.employeeInfos.Where(x => x.orgType == org && (x.LPRDate <= to && x.LPRDate >= frm)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> LeaveInLastOneMonthByOrg(string org)
        {
            DateTime to = DateTime.Now;
            DateTime frm = to.AddMonths(-1);
            List<int> ids = await  _context.leaveLogs.Where(x => x.leaveFrom >= frm && x.leaveFrom <= to).AsNoTracking().Select(x => (int)x.employeeId).ToListAsync();
            return await _context.employeeInfos.Where(x => x.orgType == org && ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }
        public async Task<string> GetEmployeeIDByAuthID(string empId)
        {
            return await _context.employeeInfos.Where(x => x.ApplicationUserId == empId).Select(x => x.employeeCode.ToString()).FirstOrDefaultAsync();
        }

        //Visual Data
        public async Task<string> GetEmpCodeNameVisualData()
        {
            return await _context.VisualDatas.AsNoTracking().Select(x => x.empCodeName).FirstOrDefaultAsync();
        }
    }
}
