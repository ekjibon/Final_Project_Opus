using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales
{
    public class OSalesCollectionDetailsService : IOSalesCollectionDetailsService
    {
        private readonly ERPDbContext _context;

        public OSalesCollectionDetailsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveSalesCollectionDetail(SalesCollectionDetail salesCollectionDetail)
        {
            if (salesCollectionDetail.Id != 0)
                _context.OSalesCollectionDetails.Update(salesCollectionDetail);
            else
                _context.OSalesCollectionDetails.Add(salesCollectionDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetail()
        {
            return await _context.OSalesCollectionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetailByCollectionId(int id)
        {
            return await _context.OSalesCollectionDetails.Where(x => x.salesCollectionId == id).Include(x=>x.salesCollectionBillInfo.salesInvoiceMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BillReturnPaymentDetail>> GetAllBillReturnPaymentDetailByMasterId(int id)
        {
            return await _context.BillReturnPaymentDetails.Where(x => x.billReturnPaymentMasterId == id).Include(x => x.salesReturnInvoiceMaster).AsNoTracking().ToListAsync();
        }

        public async Task<SalesCollectionDetail> GetSalesCollectionDetailById(int id)
        {
            return await _context.OSalesCollectionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteSalesCollectionDetailById(int id)
        {
            _context.OSalesCollectionDetails.Remove(_context.OSalesCollectionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
