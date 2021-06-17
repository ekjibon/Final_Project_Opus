using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.MasterData
{
    public class VMSReportService: IVMSReportService
    {
        private readonly ERPDbContext _context;

        public VMSReportService(ERPDbContext context)
        {
            _context = context;
        }
        
        #region Report Field
        public async Task<int> SaveReportField(ReportField reportField)
        {
            if (reportField.Id != 0)
            {
                reportField.updatedBy = reportField.createdBy;
                reportField.updatedAt = DateTime.Now;
                _context.ReportFields.Update(reportField);
            }
            else
            {
                reportField.createdAt = DateTime.Now;
                _context.ReportFields.Add(reportField);
            }

            await _context.SaveChangesAsync();
            return reportField.Id;
        }

        public async Task<IEnumerable<ReportField>> GetAllReportField()
        {
            return await _context.ReportFields.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ServiceReportViewModel>> GetServiceReportData(int rptType, int vid, int fuelStId, int vendorId, string fromDate, string toDate, string userId)
        {
            return await _context.serviceReportViews.FromSql($"SP_GetServiceReportData {rptType},{vid},{fuelStId},{vendorId},{fromDate},{toDate},{userId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<LogBookReportViewModel>> GetLogBookReportData( int vid,  string fromDate, string toDate, string userId)
        {
            return await _context.logBookReportViews.FromSql($"SP_GetLogBookReportData {vid},{fromDate},{toDate},{userId}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
