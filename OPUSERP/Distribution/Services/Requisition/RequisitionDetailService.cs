using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.Distribution.Services.Requisition.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.Requisition
{
    public class RequisitionDetailService : IRequisitionDetailService
    {
        private readonly ERPDbContext _context;

        public RequisitionDetailService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleDetails
        public async Task<bool> SaveRequisitionDetail(RequisitionDetail requisitionDetail)
        {
            if (requisitionDetail.Id != 0)
                _context.distributionRequisitionDetails.Update(requisitionDetail);
            else
                _context.distributionRequisitionDetails.Add(requisitionDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetail()
        {
            return await _context.distributionRequisitionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetailsByMasterId(int masterId)
        {
            return await _context.distributionRequisitionDetails.Include(x=>x.packageDetail.packageMaster).Include(x => x.disItemPriceFixing.itemSpecification.Item).AsNoTracking().Where(x => x.requisitionMasterId == masterId).ToListAsync();
        }

        public async Task<RequisitionDetail> GetRequisitionDetailById(int id)
        {
            return await _context.distributionRequisitionDetails.FindAsync(id);
        }


        public async Task<bool> DeleteRequisitionDetailById(int id)
        {
            _context.distributionRequisitionDetails.Remove(_context.distributionRequisitionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRequisitionDetailByMasterId(int masterId)
        {
            _context.distributionRequisitionDetails.RemoveRange(_context.distributionRequisitionDetails.Where(x => x.requisitionMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
       
    }
}
