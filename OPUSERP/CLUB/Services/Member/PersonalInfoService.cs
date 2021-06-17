using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using OPUSERP.CLUB.Areas.Member.Models.NotMappedEntity;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;
using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSERP.Areas.Grettings.Models;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.CLUB.Services.Member
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ERPDbContext _context;

        public PersonalInfoService(ERPDbContext context)
        {
            _context = context;
        }

        //EmployeeInfo
        public async Task<bool> DeleteEmployeeInfoById(int id)
        {
            _context.memberInfos.Remove(_context.memberInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetEmployeeInfoCheck(string empCode, int id)
        {
            var Result = await _context.memberInfos.Where(x => x.employeeCode == empCode && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<MemberInfo>> GetEmployeeInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.memberInfos.ToListAsync();
        }

        public async Task<IEnumerable<MemberwithPhoto>> GetEmployeeInfoWithPhoto()
        {
            IEnumerable<MemberInfo> memberInfos =  await _context.memberInfos.AsNoTracking().ToListAsync();
            List<MemberwithPhoto> data = new List<MemberwithPhoto>();
            foreach (MemberInfo employeeInfo in memberInfos)
            {
                data.Add(new MemberwithPhoto
                {
                    employeeInfo = employeeInfo,
                    photoUrl = await _context.photographs.Where(x => x.employeeId == employeeInfo.Id).Select(x=>x.url).FirstOrDefaultAsync()
                });
            }
            return data;
        }

        public async Task<IEnumerable<MemberInfo>> GetEmployeeInfoByType()
        {
            return await _context.memberInfos.Where(x => x.memberTypeId == 1).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Grettings>> GetThisMonthGrettings()
        {
            DateTime dt = DateTime.Now;
            List<Grettings> data = new List<Grettings>();

            data = await _context.memberInfos.Where(x => x.dateOfBirth.Value.Month == dt.Month).Select(x =>
             new Grettings
             {
                 name = x.nameEnglish,
                 memberName = x.nameEnglish,
                 date = x.dateOfBirth,
                 email = x.emailAddressPersonal,
                 GrettingsMessage = "Birthday",
                 relation = "self"
             }).ToListAsync();

            data.AddRange(await _context.childrens.Where(x => x.dateOfBirth.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.childName,
                date = x.dateOfBirth,
                memberName = x.employee.nameEnglish,
                email = x.employee.emailAddressPersonal,
                GrettingsMessage = "Birthday",
                relation = "Children of"
            }).ToListAsync());


            data.AddRange(await _context.memberSpouses.Where(x => x.dateOfBirth.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.spouseName,
                date = x.dateOfBirth,
                memberName = x.employee.nameEnglish,
                email = x.employee.emailAddressPersonal,
                GrettingsMessage = "Birthday",
                relation = "Spouse of"
            }).ToListAsync());


            data.AddRange(await _context.memberSpouses.Where(x => x.dateOfMarriage.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.spouseName,
                date = x.dateOfMarriage,
                memberName = x.employee.nameEnglish,
                email = x.employee.emailAddressPersonal,
                GrettingsMessage = "Marriage Day",
                relation = "Spouse of"
            }).ToListAsync());

            return data;
        }



        public async Task<IEnumerable<Grettings>> GetEmployeeGrettings()
        {
            DateTime dt = DateTime.Now;
            List<Grettings> data = new List<Grettings>();

            data = await _context.memberInfos.Where(x => x.dateOfBirth.Value.Month == dt.Month && x.dateOfBirth.Value.Day == dt.Day).Select(x =>
             new Grettings
             {
                 name = x.nameEnglish,
                 memberName = x.nameEnglish,
                 GrettingsMessage = "Birthday",
                 relation = "self"
             }).ToListAsync();

            data.AddRange(await _context.childrens.Where(x => x.dateOfBirth.Value.Day == dt.Day && x.dateOfBirth.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.childName,
                memberName = x.employee.nameEnglish,
                GrettingsMessage = "Birthday",
                relation = "Children of"
            }).ToListAsync());


            data.AddRange(await _context.memberSpouses.Where(x => x.dateOfBirth.Value.Day == dt.Day && x.dateOfBirth.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.spouseName,
                memberName = x.employee.nameEnglish,
                GrettingsMessage = "Birthday",
                relation = "Spouse of"
            }).ToListAsync());


            data.AddRange(await _context.memberSpouses.Where(x => x.dateOfMarriage.Value.Day == dt.Day && x.dateOfBirth.Value.Month == dt.Month).Include(x => x.employee).Select(x => new Grettings
            {
                name = x.spouseName,
                memberName = x.employee.nameEnglish,
                GrettingsMessage = "Marriage Day",
                relation = "Spouse of"
            }).ToListAsync());

            return data;

        }

        public async Task<MemberInfo> GetEmployeeInfoById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.memberInfos.Include(x => x.religion).Include(x => x.memberType).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveEmployeeInfo(MemberInfo employeeInfo)
        {
            if (employeeInfo.Id != 0)
                _context.memberInfos.Update(employeeInfo);
            else
                _context.memberInfos.Add(employeeInfo);
            await _context.SaveChangesAsync();
            return employeeInfo.Id;
        }

        public async Task<MemberInfo> GetEmployeeInfoByCode(string code)
        {
            return await _context.memberInfos.Where(x => x.employeeCode == code).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberWithDesignationVM>> GetEmployeeInfoDetailsList(int empId)
        {
            return await _context.memberWithDesignations.FromSql($"sp_GetEmployeeDetailsList @p0,@p1", empId, string.Empty).ToListAsync();
        }

        public async Task<string> GetEmployeeNameCodeById(int Id)
        {
            MemberInfo data = await _context.memberInfos.FindAsync(Id);
            return data.nameEnglish + "-" + data.employeeCode;
        }

        public async Task<IEnumerable<AllMemberJson>> GetAllMemberInfoJson()
        {
            IQueryable<MemberInfo> queryData = _context.memberInfos;

            #region Result Process
            IEnumerable<MemberInfo> data = await queryData.ToListAsync();
            List<AllMemberJson> filteredData = new List<AllMemberJson>();

            foreach (MemberInfo employeeInfo in data)
            {
                filteredData.Add(new AllMemberJson
                {
                    memberId = employeeInfo.Id,
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    address = await _context.memberTransferLogs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.department).FirstOrDefaultAsync(),
                    organization = await _context.memberTransferLogs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.workStation).FirstOrDefaultAsync(),
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    //designation = employeeInfo.designation,
                    designation = await _context.memberTransferLogs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.designation).FirstOrDefaultAsync(),
                    companies = _context.Companies.FirstOrDefault().companyName,
                    membertype = await _context.memberTypes.Where(x => x.Id == employeeInfo.memberTypeId).Select(x => x.memberType).FirstOrDefaultAsync(),
                    imageUrl = await _context.memberPhotographs.Where(x => x.employeeId == employeeInfo.Id).Select(x => x.url).FirstOrDefaultAsync(),
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/MemberInfo/Photograph/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/MemberInfo/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/MemberInfo/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }


        //Here We GetQuery Result
        public async Task<IEnumerable<AllMemberJson>> GetEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<MemberInfo> queryData = _context.memberInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.memberTypeId == Int32.Parse(SepToken[1]));
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
            IEnumerable<MemberInfo> data = await queryData.ToListAsync();
            List<AllMemberJson> filteredData = new List<AllMemberJson>();

            foreach (MemberInfo employeeInfo in data)
            {
                filteredData.Add(new AllMemberJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.gpfAcNo,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' href='/MemberInfo/Info/Index/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' href='/MemberInfo/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/MemberInfo/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllMemberJson>> GetEmployeeInfoAsQueryAbleMore(string queryString, string org)
        {
            IQueryable<MemberInfo> queryData = _context.memberInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("|");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender") queryData = queryData.Where(x => x.gender == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "BloodGroup") queryData = queryData.Where(x => x.bloodGroup == SepToken[1]);
                    else if (SepToken[0] == "MemberType") queryData = queryData.Where(x => x.memberTypeId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                    else if (SepToken[0] == "Division")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.divisionId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "District")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Thana")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.thanaId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Degree")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Group")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Institution")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    //else if (SepToken[0] == "SpacialSkill")
                    //{
                    //    List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                    //    queryData = queryData.Where(x => Ids.Contains(x.Id));
                    //}

                    else if (SepToken[0] == "dateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));
                    
                    else if (SepToken[0] == "dateOfBirthSpouse")
                    {
                        List<int> Ids = await _context.memberSpouses.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfMarriage")
                    {
                        List<int> Ids = await _context.memberSpouses.Where(x => (x.dateOfMarriage >= DateTime.Parse(SepToken[1]) && x.dateOfMarriage <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfBirthChild")
                    {
                        List<int> Ids = await _context.memberChildrens.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                }
            }
            #endregion

            return await queryData.Select(x => new AllMemberJson
            {
                employeeCode = x.employeeCode,
                nameEnglish = x.nameEnglish,
                designation = x.designation,
                emailAddress = x.emailAddress,
                mobileNumberOffice = x.mobileNumberPersonal,
                action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' href='/MemberInfo/Info/Index/{x.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' href='/MemberInfo/InfoView/Index/{x.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/Employee/InfoView/pdspdf/{x.Id}'><i class='fa fa-print'></i></a>"
            }).ToListAsync();
        }

        public async Task<MemberInfo> GetFreeEmployeeByCode(string code)
        {
            //return await _context.memberInfos.Where(x => x.employeeCode == code && (x.ApplicationUserId == null || x.ApplicationUserId == "" || x.ApplicationUserId == "0")).FirstAsync();
            return await _context.memberInfos.Where(x => x.employeeCode == code ).FirstAsync();
        }

        public async Task<bool> UpdateEmployee(int Id, string authId, string org)
        {
            MemberInfo data = await _context.memberInfos.FindAsync(Id);

            if (data == null) return false;
            //data.ApplicationUserId = authId;
            data.orgType = org;

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberInfo>> GetEmployeeInfoByOrg(string org)
        {
            return await _context.memberInfos.Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<MemberInfo> GetEmployeeInfoByCodeAndOrg(string code, string orgType)
        {
            return await _context.memberInfos.Where(x => x.employeeCode == code).Where(x => x.orgType == orgType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string> GetAuthCodeByUserId(int empId)
        {
            //return await _context.memberInfos.Where(x => x.Id == empId).AsNoTracking().Select(x => x.ApplicationUserId).FirstOrDefaultAsync();
            return await _context.memberInfos.Where(x => x.Id == empId).AsNoTracking().Select(x=>x.employeeCode).FirstOrDefaultAsync();
        }

        public async Task<int> IsThisEmpIDPresent(string employeeId)
        {
            return await _context.memberInfos.Where(x => x.employeeCode == employeeId).AsNoTracking().Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<string> GetEmployeeIDByAuthID(string empId)
        {
            //return await _context.memberInfos.Where(x => x.ApplicationUserId == empId).Select(x => x.Id.ToString()).FirstOrDefaultAsync();
            return await _context.memberInfos.Select(x => x.Id.ToString()).FirstOrDefaultAsync();
        }
    }
}
