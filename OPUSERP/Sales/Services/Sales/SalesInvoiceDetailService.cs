

using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Sales.Data.Entity.Sales;
using OPUSERP.Sales.Services.Sales.Interface;
using OPUSERP.Sales.Services.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Sales
{
    public class SalesInvoiceDetailService: ISalesInvoiceDetailService
    {
        private readonly ERPDbContext _context;

        public SalesInvoiceDetailService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleDetails
        public async Task<int> SaveSalesInvoiceDetail(SalesInvoiceDetail salesInvoiceDetail)
        {
            if (salesInvoiceDetail.Id != 0)
                _context.SalesInvoiceDetails.Update(salesInvoiceDetail);
            else
                _context.SalesInvoiceDetails.Add(salesInvoiceDetail);
            await _context.SaveChangesAsync();
            return salesInvoiceDetail.Id;
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetail()
        {
            return await _context.SalesInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return await _context.SalesInvoiceDetails.Include(x=>x.agreementDetails.ratingYear).AsNoTracking().Where(x=>x.salesInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<SalesInvoiceDetail> GetSalesInvoiceDetailById(int id)
        {
            return await _context.SalesInvoiceDetails.FindAsync(id);
        }
       

        public async Task<bool> DeleteSalesInvoiceDetailById(int id)
        {
            _context.SalesInvoiceDetails.Remove(_context.SalesInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId)
        {
            _context.SalesInvoiceDetails.RemoveRange(_context.SalesInvoiceDetails.Where(x=>x.salesInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region RepSaleDetails
        public async Task<bool> SaveRepSalesInvoiceDetail(RepSalesInvoiceDetail salesInvoiceDetail)
        {
            if (salesInvoiceDetail.Id != 0)
                _context.RepSalesInvoiceDetails.Update(salesInvoiceDetail);
            else
                _context.RepSalesInvoiceDetails.Add(salesInvoiceDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepSalesInvoiceDetail>> GetAllRepSalesInvoiceDetail()
        {
            return await _context.RepSalesInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RepSalesInvoiceDetail>> GetAllRepSalesInvoiceDetailByMasterId(int masterId)
        {
            return await _context.RepSalesInvoiceDetails.Include(x => x.agreementDetails.ratingYear).AsNoTracking().Where(x => x.repSalesInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<RepSalesInvoiceDetail> GetRepSalesInvoiceDetailById(int id)
        {
            return await _context.RepSalesInvoiceDetails.FindAsync(id);
        }


        public async Task<bool> DeleteRepSalesInvoiceDetailById(int id)
        {
            _context.RepSalesInvoiceDetails.Remove(_context.RepSalesInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRepSalesInvoiceDetailByMasterId(int masterId)
        {
            _context.RepSalesInvoiceDetails.RemoveRange(_context.RepSalesInvoiceDetails.Where(x => x.repSalesInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

    }
}
