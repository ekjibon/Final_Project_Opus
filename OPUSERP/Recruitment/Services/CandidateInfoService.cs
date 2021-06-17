using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class CandidateInfoService : ICandidateInfoService
    {
        private readonly ERPDbContext _context;

        public CandidateInfoService(ERPDbContext context)
        {
            _context = context;
        }

        //Candidate Info

        public async Task<int> SaveCandidateInfo(CandidateInfo candidateInfo)
        {
            if (candidateInfo.Id != 0)
                _context.CandidateInfos.Update(candidateInfo);
            else
                _context.CandidateInfos.Add(candidateInfo);
            await _context.SaveChangesAsync();
            return candidateInfo.Id;
        }

        public async Task<bool> UpdateCandidateInfo(int Id, int Type, string updateBy)
        {
            CandidateInfo data = await _context.CandidateInfos.FindAsync(Id);
            if (data != null)
            {
                data.status = Type;
                data.updatedBy = updateBy;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<CandidateInfo> GetCandidateInfoById(int id)
        {
            return await _context.CandidateInfos.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<CandidateInfo>> GetCandidateInfo()
        {
            return await _context.CandidateInfos.Include(x => x.jobReq).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletCandidateInfoById(int id)
        {
            _context.CandidateInfos.Remove(_context.CandidateInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllCandidateJson>> GetCandidateInfoAsQueryAble()
        {
            //IQueryable<CandidateInfo> queryData = _context.CandidateInfos;

            #region Filtering...

            //string[] Tokens = queryString.Split("&");
            //foreach (string token in Tokens)
            //{
            //    string[] SepToken = token.Split("=");
            //    if (SepToken.Length > 1)
            //    {
            //        if (SepToken[0] == "EmpType")
            //        {
            //            queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
            //        }
            //        else if (SepToken[0] == "PRL")
            //        {
            //            DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
            //            DateTime now = DateTime.Now;
            //            queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) <= nowAndEx && DateTime.Parse(x.LPRDate) >= now));
            //        }
            //    }
            //}
            #endregion

            #region Result Process
            //List<CandidateInfo> data = new List<CandidateInfo>();
            IEnumerable<CandidateInfo> data = await _context.CandidateInfos.ToListAsync();
            List<AllCandidateJson> filteredData = new List<AllCandidateJson>();

            foreach (CandidateInfo employeeInfo in data)
            {
                filteredData.Add(new AllCandidateJson
                {
                    candidateCode = employeeInfo.candidateCode,
                    candidateName = employeeInfo.candidateName,
                    email = employeeInfo.email,
                    mobile = employeeInfo.mobile,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/Recruitment/CVCollection/UpdateCandidateInfo/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='View' target='_blank' href='/HRPMSEmployee/Info/GridView/{employeeInfo.Id}'><i class='fa fa-search'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/HRPMSEmployee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/HRPMSEmployee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }



        public async Task<IEnumerable<CVFilterViewModelReport>> GetCVInfoAsQueryAble(string queryString)
        {
            IQueryable<CandidateInfo> queryData = _context.CandidateInfos;

            int i = 1;
            List<CVFilterViewModelReport> cVFilterViewModelReports = new List<CVFilterViewModelReport>();

            if (queryString == "all")
            {
                foreach (var data in queryData)
                {
                    cVFilterViewModelReports.Add(new CVFilterViewModelReport
                    {
                        id = i++,
                        name = data.candidateName,
                        email = data.email,
                        mobile = data.mobile,
                        Action = $"<a class='btn btn-success' data-toggle='modal' data-target='#cvDetailsViewModal' onclick='AddValue({data.Id})' href='javascript:void(0)'><i class='fa fa-eye'></i></a>"
                    });
                }
                return cVFilterViewModelReports;
            }

            #region Filtering...

            string[] Tokens = queryString.Split("|");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender") queryData = queryData.Where(x => x.gender == SepToken[1]);
                    else if (SepToken[0] == "HomeDistrict") queryData = queryData.Where(x => x.homeDistrict == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "BloodGroup") queryData = queryData.Where(x => x.bloodGroup == SepToken[1]);
                    else if (SepToken[0] == "CurrentDesignation") queryData = queryData.Where(x => x.designation == SepToken[1]);

                    else if (SepToken[0] == "Division")
                    {
                        List<int> Ids = await _context.RCRTAddresses.Where(x => x.divisionId == Int32.Parse(SepToken[1])).Select(x => x.candidateId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "District")
                    {
                        List<int> Ids = await _context.RCRTAddresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.candidateId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Thana")
                    {
                        List<int> Ids = await _context.RCRTAddresses.Where(x => x.thanaId == Int32.Parse(SepToken[1])).Select(x => x.candidateId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Degree")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Group")
                    {
                        List<int> Ids = await _context.RCRTEducations.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.candidateId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "University")
                    {
                        List<int> Ids = await _context.RCRTEducations.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.candidateId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "DateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));

                    
                }
            }
            #endregion
            foreach(var data in queryData)
            {
                cVFilterViewModelReports.Add(new CVFilterViewModelReport {
                    id = i++,
                    name = data.candidateName,
                    email = data.email,
                    mobile = data.mobile,
                    Action = $"<a class='btn btn-success' data-toggle='modal' data-target='#cvDetailsViewModal' onclick='AddValue({data.Id})' href='javascript:void(0)'><i class='fa fa-eye'></i></a>"
                });
            }

            return cVFilterViewModelReports;
        }


    }
}
