using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.Report
{
    public class ReportsService : IReports
    {
        private readonly ERPDbContext _context;

        public ReportsService(ERPDbContext context)
        {
            _context = context;
        }    
        
      
        public  IEnumerable<IndividualAttendanceReportViewModel> GetJobCardReport(string empCode,string fromDate ,string toDate)
        {
            //return _context.JobCardReportViewModels.FromSql("IndividualAttendanceReport @p0,@p1,@p2",empCode, fromDate, toDate).ToList();  
            //return _context.JobCardReportViewModels.FromSql(@"call IndividualAttendanceReport ({0},{1},{2})", empCode, fromDate, toDate).ToList();

            return _context.individualAttendanceReportViewModels.FromSql(@"SP_GET_Attendance @p0,@p1,@p2", empCode, fromDate, toDate).ToList();
        }
         
        //Here We GetQuery Result
        public async Task<IEnumerable<EmployeeReport>> GetEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("|");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender") queryData = queryData.Where(x => x.gender == SepToken[1]);
                    else if (SepToken[0] == "HomeDistrict") queryData = queryData.Where(x => x.homeDistrict == SepToken[1]);
                    else if (SepToken[0] == "Disability") queryData = queryData.Where(x => x.disability == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "BloodGroup") queryData = queryData.Where(x => x.bloodGroup == SepToken[1]);
                    else if (SepToken[0] == "EmployeePosition") queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                    else if (SepToken[0] == "NatureRecrutement") queryData = queryData.Where(x => x.natureOfRequitment == SepToken[1]);
                    else if (SepToken[0] == "joiningDesignation") queryData = queryData.Where(x => x.joiningDesignation == SepToken[1]);
                    else if (SepToken[0] == "CurrentDesignation") queryData = queryData.Where(x => x.designation == SepToken[1]);
                    else if (SepToken[0] == "SBU") queryData = queryData.Where(x => x.branchId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "PNS") queryData = queryData.Where(x => x.pNSId == Int32.Parse(SepToken[1]));
                    
                    else if (SepToken[0] == "Division") {
                        List<int> Ids = await _context.addresses.Where(x => x.divisionId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "District") {
                        List<int> Ids = await _context.addresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Thana") {
                        List<int> Ids = await _context.addresses.Where(x => x.thanaId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Degree") {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Group") {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "University") {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "SpouseHomeDistrict") {
                        List<int> Ids = await _context.addresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Language") {
                        List<int> Ids = await _context.employeeLanguages.Where(x => x.languageId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "AdverseComment")
                    {
                        List<int> Ids = await _context.acrInfos.Where(x => x.advanceComment == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "TravelCountry")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => x.countryId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "IBASS")
                    {
                        List<int> Ids = await _context.bankInfos.Where(x => x.ibus == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "LicenseCategory")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => x.category == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "LeaveType")
                    {
                        List<int> Ids = await _context.leaveLogs.Where(x => x.Id == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Project")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRProjectId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Doner")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRDonerId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Activity")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRActivityId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "DateOfBirth") queryData = queryData.Where(x =>  (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofJoining") queryData = queryData.Where(x => (x.joiningDateGovtService >= DateTime.Parse(SepToken[1]) && x.joiningDateGovtService <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "LPRDate") queryData = queryData.Where(x => (x.LPRDate >= DateTime.Parse(SepToken[1]) && x.LPRDate <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "PRLStartDate") queryData = queryData.Where(x => (x.PRLStartDate >= DateTime.Parse(SepToken[1]) && x.PRLStartDate <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "PRLEndDate") queryData = queryData.Where(x => (x.PRLEndDate >= DateTime.Parse(SepToken[1]) && x.PRLEndDate <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofRegularity") queryData = queryData.Where(x => (x.dateofregularity >= DateTime.Parse(SepToken[1]) && x.dateofregularity <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofConfirmation") queryData = queryData.Where(x => (x.dateOfPermanent >= DateTime.Parse(SepToken[1]) && x.dateOfPermanent <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "ACRStartDate")
                    {
                        //List<int> Ids = await _context.acrInfos.Where(x => (DateTime.Parse(x.startDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.startDate) <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        List<int> Ids = await _context.acrInfos.Where(x => (x.startDate >= DateTime.Parse(SepToken[1]) && x.startDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ACREndDate")
                    {
                        //List<int> Ids = await _context.acrInfos.Where(x => (DateTime.Parse(x.endDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.endDate) <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        List<int> Ids = await _context.acrInfos.Where(x => (x.endDate >= DateTime.Parse(SepToken[1]) && x.endDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "PassportIssueDate")
                    {
                        List<int> Ids = await _context.passportDetails.Where(x => (x.dateOfIssue >= DateTime.Parse(SepToken[1]) && x.dateOfIssue <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "PassportExpiryDate")
                    {
                        List<int> Ids = await _context.passportDetails.Where(x => (x.dateOfExpair >= DateTime.Parse(SepToken[1]) && x.dateOfExpair <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TravelStartDate")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => (x.startDate >= DateTime.Parse(SepToken[1]) && x.startDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TravelEndDate")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => (x.endDate >= DateTime.Parse(SepToken[1]) && x.endDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LicenseIssueDate")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => (x.dateOfIssue >= DateTime.Parse(SepToken[1]) && x.dateOfIssue <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LicenseExpiryDate")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => (x.dateOfExpair >= DateTime.Parse(SepToken[1]) && x.dateOfExpair <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ServiceFromDate")
                    {
                        List<int> Ids = await _context.transferLogs.Where(x => (x.from >= DateTime.Parse(SepToken[1]) && x.from <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ServiceToDate")
                    {
                        List<int> Ids = await _context.transferLogs.Where(x => (x.to >= DateTime.Parse(SepToken[1]) && x.to <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "BookBorrowDate")
                    {
                        List<int> Ids = await _context.borrowerInfos.Where(x => (x.dateOfBorrow >= DateTime.Parse(SepToken[1]) && x.dateOfBorrow <= DateTime.Parse(SepToken[2]))).Select(x => x.borrowerId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "BookReturnDate")
                    {
                        List<int> Ids = await _context.borrowerInfos.Where(x => (x.dateOfReturn >= DateTime.Parse(SepToken[1]) && x.dateOfReturn <= DateTime.Parse(SepToken[2]))).Select(x => x.borrowerId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LeaveFromDate")
                    {
                        List<int?> Ids = await _context.leaveLogs.Where(x => (x.leaveFrom >= DateTime.Parse(SepToken[1]) && x.leaveFrom <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LeaveToDate")
                    {
                        List<int?> Ids = await _context.leaveLogs.Where(x => (x.leaveTo >= DateTime.Parse(SepToken[1]) && x.leaveTo <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingFromDate")
                    {
                        List<int?> Ids = await _context.enrolledTrainees.Include(x => x.trainingInfoNewId).Where(x => (x.trainingInfoNew.startDateActual >= DateTime.Parse(SepToken[1]) && x.trainingInfoNew.startDateActual <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingToDate")
                    {
                        List<int?> Ids = await _context.enrolledTrainees.Include(x => x.trainingInfoNewId).Where(x => (x.trainingInfoNew.endDateActual >= DateTime.Parse(SepToken[1]) && x.trainingInfoNew.endDateActual <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ContractStartDate")
                    {
                        List<int?> Ids = await _context.EmployeeContractInfos.Where(x => (x.contractStartDate >= DateTime.Parse(SepToken[1]) && x.contractStartDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ContractEndDate")
                    {
                        List<int?> Ids = await _context.EmployeeContractInfos.Where(x => (x.contractEndDate >= DateTime.Parse(SepToken[1]) && x.contractEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                }
            }
            #endregion

            return await queryData.Select(x=> new EmployeeReport
            {
                id = x.employeeCode,
                name = x.nameEnglish,
                currentDesignation = x.designation,
                email = x.emailAddress,
                mobile = x.mobileNumberPersonal
            }).ToListAsync();
        }


        //Here We GetQuery Result
        public async Task<IEnumerable<EmployeeReport>> GetWagesAsQueryAble(string queryString, string org)
        {
            IQueryable<WagesEmployeeInfo> queryData = _context.wagesEmployeeInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("|");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender") queryData = queryData.Where(x => x.gender == SepToken[1]);
                    else if (SepToken[0] == "HomeDistrict") queryData = queryData.Where(x => x.homeDistrict == SepToken[1]);
                    else if (SepToken[0] == "ActivityStatus") queryData = queryData.Where(x => x.activityStatus == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "Disability") queryData = queryData.Where(x => x.disability == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "BloodGroup") queryData = queryData.Where(x => x.bloodGroup == SepToken[1]);
                    else if (SepToken[0] == "EmployeePosition") queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                    else if (SepToken[0] == "NatureRecrutement") queryData = queryData.Where(x => x.natureOfRequitment == SepToken[1]);
                    else if (SepToken[0] == "joiningDesignation") queryData = queryData.Where(x => x.joiningDesignation == SepToken[1]);
                    else if (SepToken[0] == "CurrentDesignation") queryData = queryData.Where(x => x.designation == SepToken[1]);
                    else if (SepToken[0] == "SBU") queryData = queryData.Where(x => x.branchId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "PNS") queryData = queryData.Where(x => x.pNSId == Int32.Parse(SepToken[1]));

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
                    else if (SepToken[0] == "University")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "SpouseHomeDistrict")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Language")
                    {
                        List<int> Ids = await _context.employeeLanguages.Where(x => x.languageId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "AdverseComment")
                    {
                        List<int> Ids = await _context.acrInfos.Where(x => x.advanceComment == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "TravelCountry")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => x.countryId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "IBASS")
                    {
                        List<int> Ids = await _context.bankInfos.Where(x => x.ibus == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "LicenseCategory")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => x.category == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "LeaveType")
                    {
                        List<int> Ids = await _context.leaveLogs.Where(x => x.Id == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Project")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRProjectId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Doner")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRDonerId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Activity")
                    {
                        List<int> Ids = await _context.employeeProjectActivities.Where(x => x.hRActivityId == Int32.Parse(SepToken[1])).Select(x => (int)x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "DateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofJoining") queryData = queryData.Where(x => (x.joiningDateGovtService >= DateTime.Parse(SepToken[1]) && x.joiningDateGovtService <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "LPRDate") queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.LPRDate) <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "PRLStartDate") queryData = queryData.Where(x => (DateTime.Parse(x.PRLStartDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.PRLStartDate) <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "PRLEndDate") queryData = queryData.Where(x => (DateTime.Parse(x.PRLEndDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.PRLEndDate) <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofRegularity") queryData = queryData.Where(x => (x.dateofregularity >= DateTime.Parse(SepToken[1]) && x.dateofregularity <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "DateofConfirmation") queryData = queryData.Where(x => (x.dateOfPermanent >= DateTime.Parse(SepToken[1]) && x.dateOfPermanent <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "ACRStartDate")
                    {
                        //List<int> Ids = await _context.acrInfos.Where(x => (DateTime.Parse(x.startDate) >= DateTime.Parse(SepToken[1]) && DateTime.Parse(x.startDate) <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        List<int> Ids = await _context.acrInfos.Where(x => (x.startDate >= DateTime.Parse(SepToken[1]) && x.startDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ACREndDate")
                    {
                        List<int> Ids = await _context.acrInfos.Where(x => (x.endDate >= DateTime.Parse(SepToken[1]) && x.endDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "PassportIssueDate")
                    {
                        List<int> Ids = await _context.passportDetails.Where(x => (x.dateOfIssue >= DateTime.Parse(SepToken[1]) && x.dateOfIssue <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "PassportExpiryDate")
                    {
                        List<int> Ids = await _context.passportDetails.Where(x => (x.dateOfExpair >= DateTime.Parse(SepToken[1]) && x.dateOfExpair <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TravelStartDate")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => (x.startDate >= DateTime.Parse(SepToken[1]) && x.startDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TravelEndDate")
                    {
                        List<int> Ids = await _context.traveInfos.Where(x => (x.endDate >= DateTime.Parse(SepToken[1]) && x.endDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LicenseIssueDate")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => (x.dateOfIssue >= DateTime.Parse(SepToken[1]) && x.dateOfIssue <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LicenseExpiryDate")
                    {
                        List<int> Ids = await _context.drivingLicenses.Where(x => (x.dateOfExpair >= DateTime.Parse(SepToken[1]) && x.dateOfExpair <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ServiceFromDate")
                    {
                        List<int> Ids = await _context.transferLogs.Where(x => (x.from >= DateTime.Parse(SepToken[1]) && x.from <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ServiceToDate")
                    {
                        List<int> Ids = await _context.transferLogs.Where(x => (x.to >= DateTime.Parse(SepToken[1]) && x.to <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "BookBorrowDate")
                    {
                        List<int> Ids = await _context.borrowerInfos.Where(x => (x.dateOfBorrow >= DateTime.Parse(SepToken[1]) && x.dateOfBorrow <= DateTime.Parse(SepToken[2]))).Select(x => x.borrowerId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "BookReturnDate")
                    {
                        List<int> Ids = await _context.borrowerInfos.Where(x => (x.dateOfReturn >= DateTime.Parse(SepToken[1]) && x.dateOfReturn <= DateTime.Parse(SepToken[2]))).Select(x => x.borrowerId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LeaveFromDate")
                    {
                        List<int?> Ids = await _context.leaveLogs.Where(x => (x.leaveFrom >= DateTime.Parse(SepToken[1]) && x.leaveFrom <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "LeaveToDate")
                    {
                        List<int?> Ids = await _context.leaveLogs.Where(x => (x.leaveTo >= DateTime.Parse(SepToken[1]) && x.leaveTo <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingFromDate")
                    {
                        List<int?> Ids = await _context.enrolledTrainees.Include(x => x.trainingInfoNewId).Where(x => (x.trainingInfoNew.startDateActual >= DateTime.Parse(SepToken[1]) && x.trainingInfoNew.startDateActual <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingToDate")
                    {
                        List<int?> Ids = await _context.enrolledTrainees.Include(x => x.trainingInfoNewId).Where(x => (x.trainingInfoNew.endDateActual >= DateTime.Parse(SepToken[1]) && x.trainingInfoNew.endDateActual <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ContractStartDate")
                    {
                        List<int?> Ids = await _context.EmployeeContractInfos.Where(x => (x.contractStartDate >= DateTime.Parse(SepToken[1]) && x.contractStartDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ContractEndDate")
                    {
                        List<int?> Ids = await _context.EmployeeContractInfos.Where(x => (x.contractEndDate >= DateTime.Parse(SepToken[1]) && x.contractEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                }
            }
            #endregion

            return await queryData.Select(x => new EmployeeReport
            {
                id = x.employeeCode,
                name = x.nameEnglish,
                currentDesignation = x.designation,
                email = x.emailAddress,
                mobile = x.mobileNumberPersonal
            }).ToListAsync();
        }

        public async Task<IEnumerable<HrReportViewModel>> GetHrDataByDesig(string desigId, int? deptId, string bloodGroup, int? sbuId)
        {
            return await _context.hrReportViewModels.FromSql($"SP_RPT_HR {desigId},{deptId},{bloodGroup},{sbuId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HrEducationReportViewModel>> GetHrDataByEducation(int? subjectId, int? universityId, int? loeId)
        {
            return await _context.hrEducationReportViewModels.FromSql($"SP_RPT_HR_Education {subjectId},{universityId},{loeId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HrTrainingReportViewModel>> GetHrDataByTrCourse(int? courseId)
        {
            return await _context.hrTrainingReportViewModels.FromSql($"SP_RPT_HR_Training {courseId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HrBelongingReportViewModel>> GetHrDataByBelongingItem(int? belongingId)
        {
            return await _context.hrBelongingReportViewModels.FromSql($"SP_RPT_HR_Belonging {belongingId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<HrSummaryReportViewModel>> GetHrSummaryData(string callName)
        {
            return await _context.hrSummaryReportViewModels.FromSql($"SP_RPT_HRSUMMARY {callName}").AsNoTracking().ToListAsync();
        }
    }
}
