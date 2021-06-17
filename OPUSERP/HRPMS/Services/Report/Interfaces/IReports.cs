using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Report
{
    public interface IReports 
    {
        IEnumerable<IndividualAttendanceReportViewModel> GetJobCardReport(string empCode, string fromDate, string toDate);
        Task<IEnumerable<EmployeeReport>> GetEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<EmployeeReport>> GetWagesAsQueryAble(string queryString, string org);
        Task<IEnumerable<HrReportViewModel>> GetHrDataByDesig(string desigId, int? deptId, string bloodGroup, int? sbuId);
        Task<IEnumerable<HrEducationReportViewModel>> GetHrDataByEducation(int? subjectId, int? universityId, int? loeId);
        Task<IEnumerable<HrTrainingReportViewModel>> GetHrDataByTrCourse(int? courseId);
        Task<IEnumerable<HrBelongingReportViewModel>> GetHrDataByBelongingItem(int? belongingId);
        Task<IEnumerable<HrSummaryReportViewModel>> GetHrSummaryData(string callName);
    }
}
