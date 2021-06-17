using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.Master;

namespace OPUSERP.VMS.Services.MasterData.Interfaces
{
    public interface IVMSReportService
    {
        Task<int> SaveReportField(ReportField reportField);
        Task<IEnumerable<ReportField>> GetAllReportField();
        Task<IEnumerable<ServiceReportViewModel>> GetServiceReportData(int rptType,int vid,int fuelStId,int vendorId,string fromDate,string toDate,string userId );
        Task<IEnumerable<LogBookReportViewModel>> GetLogBookReportData(int vid, string fromDate, string toDate, string userId);
    }
}
