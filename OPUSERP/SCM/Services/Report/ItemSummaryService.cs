using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMReport.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Services.Report.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Report
{
    public class ItemSummaryService : IItemSummaryService
    {
        private readonly ERPDbContext _context;

        public ItemSummaryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemSummaryViewModel>> GetItemSummary(DateTime? fromDate, DateTime? toDate)
        {
            List<ItemSummaryViewModel> itemSummaryViewModels = new List<ItemSummaryViewModel>();

            itemSummaryViewModels = await _context.RequisitionDetails.
                 Join(_context.RequisitionMasters, rd => rd.requisitionMasterId, rm => rm.Id,
                (rd, rm) => new { rd, rm }).
                Join(_context.ItemSpecifications, rdm => rdm.rd.itemSpecificationId, i => i.Id,
                (rdm, i) => new { rdm, i })
                .Join(_context.Items, ri => ri.i.itemId, it => it.Id,
                (ri, it) => new { ri, it })
                .Join(_context.Units, rii => rii.it.unitId, u => u.Id, (rii, u) => new { rii, u })
                .Join(_context.StockDetails, f => f.rii.ri.i.Id, s => s.itemSpecificationId, (f, s) => new { f, s })
                .Join(_context.StockMasters, sd => sd.s.stockMasterId, sm => sm.Id, (sd, sm) => new { sd, sm })
                .Select(x => new ItemSummaryViewModel
                {
                    itemName = x.sd.f.rii.it.itemName,
                    unitName=x.sd.f.u.unitName,
                    openingBalance=10500,
                    stockStatusId=x.sm.stockStatusId,
                    PurchaseReturn= 85,
                    IssueReturn=30,
                    Damages=40,
                    Obsolete=8,
                    ClosingBalance=56656
                }).Take(30).ToListAsync();
            
            return itemSummaryViewModels;
            
        }
        public async Task<IEnumerable<ItemSummaryViewModel>> ItemwiseIssueDetails(DateTime? fromDate, DateTime? toDate)
        {
            List<ItemSummaryViewModel> itemSummaryViewModels = new List<ItemSummaryViewModel>();
            itemSummaryViewModels = await _context.RequisitionDetails.
                 Join(_context.RequisitionMasters, rd => rd.requisitionMasterId, rm => rm.Id,
                (rd, rm) => new { rd, rm }).
                Join(_context.ItemSpecifications, rdm => rdm.rd.itemSpecificationId, i => i.Id,
                (rdm, i) => new { rdm, i })
                .Join(_context.Items, ri => ri.i.itemId, it => it.Id,
                (ri, it) => new { ri, it })
                .Join(_context.Units, rii => rii.it.unitId, u => u.Id, (rii, u) => new { rii, u })
                .Join(_context.StockDetails, f => f.rii.ri.i.Id, s => s.itemSpecificationId, (f, s) => new { f, s })
                .Join(_context.StockMasters, sd => sd.s.stockMasterId, sm => sm.Id, (sd, sm) => new { sd, sm })
                .Select(x => new ItemSummaryViewModel
                {
                    Qty = x.sd.s.qty,
                    unitName = x.sd.f.u.unitName,
                    ReceivedBy="Police",
                    Department="Police",
                    IsuueDate=DateTime.Now
                   
                }).Take(30).ToListAsync();
            return itemSummaryViewModels;
        }
    }
}
