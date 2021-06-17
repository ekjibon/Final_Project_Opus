using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.Areas.FAMSAssetRegister.Models.Dashboard;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Dashboard.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.Dashboard.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.Dashboard
{
    public class FAMSDashboardService : IFAMSDashboardService
    {
        private readonly ERPDbContext _context;

        public FAMSDashboardService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<FAMSDashboardcountViewModel> GetFAMSDashboardcountViewModel()
        {
            List<FAMSDashboardcountViewModel> models = new List<FAMSDashboardcountViewModel>();
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                int monthno = DateTime.Now.Month;
                DateTime premonth = DateTime.Now.AddMonths(monthno * -1);
                List<DateTime> months = new List<DateTime>();
                List<string> monthshow = new List<string>();
                int yearss = DateTime.Now.Year;
                DateTime firstDay = new DateTime(yearss, 1, 1);
                DateTime lastDay = new DateTime(yearss, 12, 31);

                int year = DateTime.Now.Year - 7;
                List<int> years = new List<int>();
                List<decimal> yearlyvalue = new List<decimal>();
                List<string> categoryname = new List<string>();
                List<decimal> categorywisepercent = new List<decimal>();
                List<AssetScheduleReportViewModel> assetScheduleReportViewModels= _context.AssetScheduleReportViewModels.FromSql($"RPTAssetScheduleSummary {Convert.ToDateTime(firstDay).ToString("yyyyMMdd")},{Convert.ToDateTime(lastDay).ToString("yyyyMMdd")}").AsNoTracking().ToList();


                //for (int i = 0; i <= monthno; i++)
                //{
                //    monthshow.Add(Convert.ToDateTime(premonth).ToString("MMMM"));
                   
                //    premonth = premonth.AddMonths(1);
                //}
                for (int i = 0; i <= 7; i++)
                {
                    years.Add(year);
                    var data = _context.AssetScheduleReportViewModels.FromSql($"RPTAssetScheduleSummary {year.ToString() + "0101"},{year.ToString() + "1231"}").AsNoTracking().ToList();
                    yearlyvalue.Add(data.Sum(x=>(decimal)x.netBookValue));
                    year = year + 1;

                }

                foreach (AssetScheduleReportViewModel d in assetScheduleReportViewModels)
                {
                    categoryname.Add(d.categoryName);
                    decimal amount = (decimal)d.netBookValue * 100 / assetScheduleReportViewModels.Sum(x=> (decimal)x.netBookValue);
                    categorywisepercent.Add(amount);
                }

                models.Add(new FAMSDashboardcountViewModel
                {
                    assetOpening = assetScheduleReportViewModels.Sum(x=>(decimal)x.openingBalance),
                    assetAddition = assetScheduleReportViewModels.Sum(x => (decimal)x.addition),
                    assetDisposal= assetScheduleReportViewModels.Sum(x => (decimal)x.disposals),
                    assetTransfer= assetScheduleReportViewModels.Sum(x => (decimal)x.transfers),
                    assetClosing= assetScheduleReportViewModels.Sum(x => (decimal)x.balance),
                    assetNet= assetScheduleReportViewModels.Sum(x => (decimal)x.netBookValue),
                    depriciationOpening= assetScheduleReportViewModels.Sum(x => (decimal)x.oBDepreciation),
                    depriciationcharged=assetScheduleReportViewModels.Sum(x => (decimal)x.accumuDepreciation),
                    depriciationadjustment= assetScheduleReportViewModels.Sum(x => (decimal)x.adjustment),
                    depriciationClosing= assetScheduleReportViewModels.Sum(x => (decimal)x.wDvalue),
                    categoryname=categoryname,
                    categorywisepercent=categorywisepercent,
                    years=years,
                    yearlyvalues=yearlyvalue



                });
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
           
            return models.FirstOrDefault();
        }
    }
}
