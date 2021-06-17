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
    public class ReportService: IReportService
    {
        private readonly ERPDbContext _context;

        public ReportService(ERPDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<NumberViewModel>> GetNumber(int? projectId, int? supplierId, DateTime? fromDate, DateTime? toDate, string type)
        {
            List<NumberViewModel> numberViewModel = new List<NumberViewModel>();
            if (type == "PO")
            {
                //numberViewModel = await _context.PurchaseOrderMasters
                //    .Where(x => x.cSMaster.requisition.projectId == projectId && x.supplierId == supplierId && x.poDate <= toDate && x.poDate >= fromDate)
                //    .Select(x => new NumberViewModel { Id = x.Id, Number = x.poNo }).ToListAsync();
                numberViewModel = await _context.PurchaseOrderMasters
                   .Where(x => x.cSMaster.requisition.projectId == projectId  && x.poDate <= toDate && x.poDate >= fromDate)
                   .Select(x => new NumberViewModel { Id = x.Id, Number = x.poNo }).ToListAsync();
            }
            else if (type == "IOU")
            {
                numberViewModel = await _context.IOUMasters
                    .Where(x => x.requisition.projectId == projectId && x.iouDate <= toDate && x.iouDate >= fromDate)
                    .Select(x => new NumberViewModel { Id = x.Id, Number = x.iouNo }).ToListAsync();
            }
            else if (type == "PR")
            {
                numberViewModel = await _context.RequisitionMasters
                    .Where(x => x.projectId == projectId && x.reqDate <= toDate && x.reqDate >= fromDate)
                    .Select(x => new NumberViewModel { Id = x.Id, Number = x.reqNo }).ToListAsync();
            }
            else if (type == "GRPO")
            {
                //numberViewModel = await _context.StockMasters
                //    .Where(x => x.purchase.cSMaster.requisition.projectId == projectId && x.purchase.supplierId == supplierId && x.receiveDate <= toDate && x.receiveDate >= fromDate && x.receiveType == "PO")
                //    .Select(x => new NumberViewModel { Id = x.Id, Number = x.receiveNo }).ToListAsync();
                numberViewModel = await _context.StockMasters
                    .Where(x => x.purchase.cSMaster.requisition.projectId == projectId  && x.receiveDate <= toDate && x.receiveDate >= fromDate && x.receiveType == "PO")
                    .Select(x => new NumberViewModel { Id = x.Id, Number = x.receiveNo }).ToListAsync();
            }
            else if (type == "GRIOU")
            {
                numberViewModel = await _context.StockMasters
                    .Where(x => x.IOU.requisition.projectId == projectId && x.receiveDate <= toDate && x.receiveDate >= fromDate && x.receiveType == "IOU")
                    .Select(x => new NumberViewModel { Id = x.Id, Number = x.receiveNo }).ToListAsync();
            }
            return numberViewModel;
        }

    }
}
