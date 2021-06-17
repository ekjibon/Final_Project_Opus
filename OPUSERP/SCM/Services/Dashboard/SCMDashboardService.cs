using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMDashboard.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.Dashboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Dashboard
{
    public class SCMDashboardService : ISCMDashboardService
    {
        private readonly ERPDbContext _context;

        public SCMDashboardService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<SCMDashboardcountViewModel> GetSCMDataCountViewModel()
        {
            List<SCMDashboardcountViewModel> models = new List<SCMDashboardcountViewModel>();
            try
            {
                DateTime date = DateTime.Now;
                DateTime firstDay = new DateTime(date.Year, 1, 1);
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                int monthno = DateTime.Now.Month;
                DateTime premonth = DateTime.Now.AddMonths(monthno * -1);
                int year = DateTime.Now.Year - 7;
                List<int> years = new List<int>();
                
                List<DateTime> months = new List<DateTime>();
                List<string> monthshow = new List<string>();
                List<string> departmentdata = new List<string>();
                List<int> deptotalrequisition = new List<int>();
                List<int> deptotalrequisitionpending = new List<int>();
                List<int> deptotalwoinprogress = new List<int>();
                List<int> deptotalwogenerated = new List<int>();
                List<decimal?> deptotalamount = new List<decimal?>();
                List<decimal?> deptotalamountbyyear = new List<decimal?>();

                List<int> depworkorderpercent = new List<int>();
                List<decimal?> depworkordervolume = new List<decimal?>();
                List<string> itemnames = new List<string>();
                List<int> topfiveitempercentwo = new List<int>();
                List<int> yearlywocount = new List<int>();
                List<string> suppliers = new List<string>();
                List<decimal> supplierswisevolume = new List<decimal>();
                List<decimal> supplierwisepercent = new List<decimal>();
                List<string> itemnamespurchase = new List<string>();
                List<decimal> itemwisepurchasepercent = new List<decimal>();
                List<decimal> itemwisepurchaseAmount = new List<decimal>();
                List<string> itemnamesrequisition = new List<string>();
                List<int> itemwiserequisitionpercent = new List<int>();
                List<int> statuswisecount = new List<int>();
                List<string> statusInfo = new List<string>();
                // List<Department> departments = _context.departments.ToList();
                List<RequisitionDetail> requisitionDetails = _context.RequisitionDetails.Include(x => x.requisitionMaster).ToList();
                List<RequisitionMaster> requisitionMasters = _context.RequisitionMasters.ToList();
                departmentdata = requisitionDetails.Select(x => x.requisitionMaster.reqDept).Distinct().ToList();
                List<PurchaseOrderMaster> purchaseOrderMasters = _context.PurchaseOrderMasters.Include(x=>x.cSMaster.requisition).Include(x=>x.supplier).ToList();
                List<PurchaseOrderDetails> purchaseOrderDetails = _context.PurchaseOrderDetails.Include(x=>x.purchase.cSMaster.requisition).Include(x => x.purchase.supplier).ToList();
                List <ItemWIseOrderViewModel> itemWIseOrderViewModels= _context.itemWIseOrderViewModels.FromSql($"getitemwisewo").AsNoTracking().ToList();
                List<SupplierWIseOrderViewModel> supplierWIseOrderViewModels = _context.supplierWIseOrderViewModels.FromSql($"getsupplierwisewo").AsNoTracking().ToList();
                List<ItemWIsePurchasedViewModel> itemWIsePurchasedViewModels = _context.itemWIsePurchasedViewModels.FromSql($"getitemwisepuchase").AsNoTracking().ToList();
                List<ItemWIseRequisitionViewModel> itemWIseRequisitionViewModels = _context.itemWIseRequisitionViewModels.FromSql($"getitemwisereq").AsNoTracking().ToList();
                foreach (string dep in departmentdata)
                {
                    List<int> statusids = new List<int>();

                    int totalreq= requisitionMasters.Where(x=>x.reqDept==dep && x.statusInfoId == 1).Count();
                    deptotalrequisition.Add(totalreq);
                    int totalpend = requisitionMasters.Where(x => x.reqDept == dep&&x.statusInfoId==2).Count();
                    deptotalrequisitionpending.Add(totalpend);
                   
                    int totalwoinprogress = requisitionMasters.Where(x => x.reqDept == dep && x.statusInfoId>=3&&x.statusInfoId<=11).Count();
                    deptotalwoinprogress.Add(totalwoinprogress);
                    int totalwogenerated = requisitionMasters.Where(x => x.reqDept == dep && x.statusInfoId >=14).Count();
                    deptotalwogenerated.Add(totalwogenerated);
                    decimal? amount = requisitionDetails.Where(x => x.requisitionMaster.reqDept == dep).Sum(x=>x.reqRate*x.reqQty);
                    deptotalamount.Add(amount);
                    decimal? amountyear = requisitionDetails.Where(x=> x.requisitionMaster.reqDept == dep&&x.requisitionMaster.reqDate>= firstDay).Sum(x => x.reqRate * x.reqQty);
                    deptotalamountbyyear.Add(amountyear);

                    int worderpercent = purchaseOrderMasters.Where(x => x.cSMaster.requisition.reqDept == dep).Count() * 100 / purchaseOrderMasters.Count();
                    depworkorderpercent.Add(worderpercent);
                    decimal? worderamount = purchaseOrderDetails.Where(x => x.purchase.cSMaster.requisition.reqDept == dep).Sum(x=>x.poQty*x.poRate);
                    depworkordervolume.Add(worderamount);

                }
                int deptotalrequisitionmp = 0;
                int deptotalrequisitionpendingmp = 0;
                int deptotalwoinprogressmp = 0;
                int deptotalwogeneratedmp = 0;
                if (requisitionMasters.Where(x => x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count()>0)
                {
                    deptotalrequisitionmp = requisitionMasters.Where(x => x.statusInfoId == 1 && x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count() * 100 / requisitionMasters.Where(x => x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count();
                    deptotalrequisitionpendingmp = requisitionMasters.Where(x => x.statusInfoId== 2 && x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count() * 100 / requisitionMasters.Where(x => x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count();
                    deptotalwoinprogressmp = requisitionMasters.Where(x => x.statusInfoId >= 3 && x.statusInfoId <= 11 && x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count() * 100 / requisitionMasters.Where(x => x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count();
                    deptotalwogeneratedmp = requisitionMasters.Where(x =>  x.statusInfoId >= 14 && x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count() * 100 / requisitionMasters.Where(x => x.reqDate >= firstDayOfMonth && x.reqDate <= lastDayOfMonth).Count();

                }
                foreach (ItemWIseOrderViewModel item in itemWIseOrderViewModels)
                {
                    itemnames.Add(item.itemName);
                    int itemper = item.quantity * 100 / purchaseOrderMasters.Count();
                    topfiveitempercentwo.Add(itemper);

                }
                foreach (SupplierWIseOrderViewModel item in supplierWIseOrderViewModels)
                {
                    suppliers.Add(item.supplierName);
                    decimal itemper = item.amount * 100 / purchaseOrderDetails.Sum(x=>(decimal)x.poQty* (decimal)x.poRate);
                    supplierwisepercent.Add(itemper);
                    supplierswisevolume.Add(item.amount);

                }
                foreach (ItemWIsePurchasedViewModel item in itemWIsePurchasedViewModels)
                {
                    itemnamespurchase.Add(item.itemName);
                    decimal itemper = item.amount * 100 / purchaseOrderDetails.Sum(x => (decimal)x.poQty * (decimal)x.poRate);
                    itemwisepurchasepercent.Add(itemper);
                    itemwisepurchaseAmount.Add(item.amount);

                }
                foreach (ItemWIseRequisitionViewModel item in itemWIseRequisitionViewModels)
                {
                    itemnamesrequisition.Add(item.itemName);
                    int itemper = item.quantity * 100 / requisitionMasters.Count();
                    itemwiserequisitionpercent.Add(itemper);
                   

                }
                for (int i = 0; i <= 7; i++)
                {
                    years.Add(year);
                    int wocount = purchaseOrderMasters.Where(x => Convert.ToDateTime(x.poDate).Year == year).Count();
                    yearlywocount.Add(wocount);
                    year = year + 1;

                }
                for (int i = 0; i <= monthno; i++)
                {
                   
                    premonth = premonth.AddMonths(1);
                }
                statusInfo.Add("Requisition Submit");
                statusInfo.Add("Pending");
                statusInfo.Add("WO in Progress");
                statusInfo.Add("WO Generated");
                statuswisecount.Add(deptotalrequisitionmp);
                statuswisecount.Add(deptotalrequisitionpendingmp);
                statuswisecount.Add(deptotalwoinprogressmp);
                statuswisecount.Add(deptotalwogeneratedmp);
                models.Add(new SCMDashboardcountViewModel
                {
                   
                    departments=departmentdata,
                    deptotalrequisition=deptotalrequisition,
                    deptotalrequisitionpending=deptotalrequisitionpending,
                    deptotalwogenerated=deptotalwogenerated,
                    deptotalwoinprogress=deptotalwoinprogress,
                    //deptotalrequisitionmonthlypercent=deptotalrequisitionmp,
                    //deptotalrequisitionpendingmonthlypercent=deptotalrequisitionpendingmp,
                    //deptotalwoinprogressmonthlypercent=deptotalwoinprogressmp,
                    //deptotalwogeneratedmonthlypercent=deptotalwogeneratedmp,
                    statsinfo=statusInfo,
                    statuswisecount=statuswisecount,
                    deptotalamount=deptotalamount,
                    deptotalamountbyyear=deptotalamountbyyear,
                    depworkorderpercent=depworkorderpercent,
                    depworkordervolume=depworkordervolume,
                    topfiveItem=itemnames,
                    topfiveitempercentwo=topfiveitempercentwo,
                    years=years,
                    yearlywocount=yearlywocount,
                    suppliers=suppliers,
                    supplierswiseAmount=supplierswisevolume,
                    supplierswisepercent=supplierwisepercent,
                    itemspurchase=itemnamespurchase,
                    itemwisepurchaseAmount=itemwisepurchaseAmount,
                    itemwisepurchasepercent=itemwisepurchasepercent,
                    itemsrequisition=itemnamesrequisition,
                    itemwiserequisitionpercent=itemwiserequisitionpercent


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
