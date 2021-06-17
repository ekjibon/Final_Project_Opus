using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.OtherSales.Models;
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
    public class OSalesInvoiceDetailService : IOSalesInvoiceDetailService
    {
        private readonly ERPDbContext _context;

        public OSalesInvoiceDetailService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleDetails
        public async Task<bool> SaveSalesInvoiceDetail(SalesInvoiceDetail salesInvoiceDetail)
        {
            if (salesInvoiceDetail.Id != 0)
                _context.OSalesInvoiceDetails.Update(salesInvoiceDetail);
            else
                _context.OSalesInvoiceDetails.Add(salesInvoiceDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetail()
        {
            return await _context.OSalesInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return await _context.OSalesInvoiceDetails.Include(x=>x.patientServiceDetails).Include(x=>x.rentInvoiceDetail).Include(x=>x.itemSpecication.Item.unit).Include(x => x.itemPriceFixing.itemSpecification.Item).AsNoTracking().Where(x => x.salesInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoicelistbydaterange(DateTime? fromDate, DateTime? toDate)
        {
            return await _context.OSalesInvoiceDetails.Include(x => x.itemSpecication.Item.unit).Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.salesInvoiceMaster.relSupplierCustomerResourse.resource).AsNoTracking().Where(x => x.salesInvoiceMaster.invoiceDate >= fromDate && x.salesInvoiceMaster.invoiceDate <= toDate).ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoicelistBydaterangeAndUser(DateTime? fromDate, DateTime? toDate, string userName)
        {
            if ((userName == "" || userName == null) && fromDate != null)
            {
                return await _context.OSalesInvoiceDetails.Include(x => x.itemSpecication.Item.unit).Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.salesInvoiceMaster.relSupplierCustomerResourse.resource).AsNoTracking().Where(x => x.salesInvoiceMaster.invoiceDate >= fromDate && x.salesInvoiceMaster.invoiceDate <= toDate).ToListAsync();
            }
            else if ((userName != "" || userName != null) && fromDate == null)
            {
                return await _context.OSalesInvoiceDetails.Include(x => x.itemSpecication.Item.unit).Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.salesInvoiceMaster.relSupplierCustomerResourse.resource).AsNoTracking().Where(x => x.salesInvoiceMaster.createdBy == userName).ToListAsync();
            }
            else
            {
                return await _context.OSalesInvoiceDetails.Include(x => x.itemSpecication.Item.unit).Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.salesInvoiceMaster.relSupplierCustomerResourse.resource).AsNoTracking().Where(x => x.salesInvoiceMaster.invoiceDate >= fromDate && x.salesInvoiceMaster.invoiceDate <= toDate && x.salesInvoiceMaster.createdBy == userName).ToListAsync();
            }
        }

        public async Task<IEnumerable<POSInvoiceListViewModel>> GetPosInvoiceList(string FDate, string TDate)
        {
            return await _context.pOSInvoiceListViewModels.FromSql($"SP_GET_PosInvoiceList {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<SalesInvoiceDetail> GetSalesInvoiceDetailById(int id)
        {
            return await _context.OSalesInvoiceDetails.FindAsync(id);
        }


        public async Task<bool> DeleteSalesInvoiceDetailById(int id)
        {
            _context.OSalesInvoiceDetails.Remove(_context.OSalesInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId)
        {
            _context.OSalesInvoiceDetails.RemoveRange(_context.OSalesInvoiceDetails.Where(x => x.salesInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region ReturnSaleDetail
        public async Task<bool> SaveSalesReturnInvoiceDetail(SalesReturnInvoiceDetail salesReturnInvoiceDetail)
        {
            if (salesReturnInvoiceDetail.Id != 0)
                _context.salesReturnInvoiceDetails.Update(salesReturnInvoiceDetail);
            else
                _context.salesReturnInvoiceDetails.Add(salesReturnInvoiceDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetail()
        {
            return await _context.salesReturnInvoiceDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<SalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailBySaleMaster(int MasterId)
        {
            return await _context.salesReturnInvoiceDetails.Where(x => x.salesInvoiceDetail.salesInvoiceMasterId == MasterId).AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteSalesReturnInvoiceDetailByMasterId(int masterId)
        {
            _context.salesReturnInvoiceDetails.RemoveRange(_context.salesReturnInvoiceDetails.Where(x => x.salesReturnInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailByMasterId(int masterId)
        {
            return await _context.salesReturnInvoiceDetails.Include(x => x.salesInvoiceDetail.itemSpecication.Item).Include(x => x.salesInvoiceDetail.salesInvoiceMaster.relSupplierCustomerResourse.Leads).AsNoTracking().Where(x => x.salesReturnInvoiceMasterId == masterId).ToListAsync();
        }
        public async Task<SalesReturnInvoiceDetail> GetSalesReturnInvoiceDetailById(int id)
        {
            return await _context.salesReturnInvoiceDetails.FindAsync(id);
        }
        #endregion
    }
}
