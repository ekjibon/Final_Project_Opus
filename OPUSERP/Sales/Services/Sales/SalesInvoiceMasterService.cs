

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Sales.Models;
using OPUSERP.Areas.Sales.Models.NotMapped;
using OPUSERP.Data;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Data.Entity.Sales;
using OPUSERP.Sales.Services.Sales.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Sales
{
    public class SalesInvoiceMasterService: ISalesInvoiceMasterService
    {
        private readonly ERPDbContext _context;

        public SalesInvoiceMasterService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleInvoice
        public async Task<int> SaveSalesInvoiceMaster(SalesInvoiceMaster salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.SalesInvoiceMasters.Update(salesInvoiceMaster);
            else
                _context.SalesInvoiceMasters.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }

        public async Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMaster()
        {
            return await _context.SalesInvoiceMasters.AsNoTracking().Include(x=>x.leads).OrderByDescending(x=>x.Id).ToListAsync();
        }

       

        public async Task<SalesInvoiceMaster> GetSalesInvoiceMasterById(int id)
        {
            return await _context.SalesInvoiceMasters.Include(x=>x.leads).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }
      

        public async Task<bool> UpdateSalesInvoiceMasterById(int id)
        {
            SalesInvoiceMaster salesInvoiceMaster = await _context.SalesInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1==0;
            }
        }
        public async Task<bool> UpdateSalesInvoiceMasterStockCloseById(int id)
        {
            SalesInvoiceMaster salesInvoiceMaster = await _context.SalesInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

       

        public async Task<bool> DeleteSalesInvoiceMasterById(int id)
        {
            _context.SalesInvoiceMasters.Remove(_context.SalesInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentInvoiceWithDue>> GetDueSalesInvoice(int customerId)
        {
            IEnumerable<SalesInvoiceMaster> salesInvoiceMasters = await _context.SalesInvoiceMasters.Where(x => x.isClose == 0 && x.leadsId == customerId).AsNoTracking().ToListAsync();

            List<PaymentInvoiceWithDue> data = new List<PaymentInvoiceWithDue>();
            foreach (SalesInvoiceMaster salesInvoiceMaster in salesInvoiceMasters)
            {
                var totalAmount = salesInvoiceMaster.NetTotal;
                var collectionDue = _context.SalesCollectionDetails.Where(x => x.salesInvoiceMasterId == salesInvoiceMaster.Id).Sum(s => s.Amount);
                SalesInvoiceMaster datastore = _context.SalesInvoiceMasters.Where(x => x.Id == salesInvoiceMaster.Id && x.isClose == 0).FirstOrDefault();
                var Due = totalAmount - collectionDue;
                if (Due > 0)
                {
                    data.Add(new PaymentInvoiceWithDue
                    {
                        salesInvoiceMaster = salesInvoiceMaster,
                        due = Due
                    });
                }
            }
            return data;
        }

        public async Task<CustomerCollectionReportVM> GetSalesInvoiceRecipt(int customerId, string fromDate, string toDate)
        {

            decimal? openingDue = _context.SalesInvoiceMasters.Where(x => x.leadsId == customerId  && x.invoiceDate < Convert.ToDateTime(fromDate)).Sum(x => x.NetTotal);
            List<SalesInvoiceMaster> invoiceMasters = new List<SalesInvoiceMaster>();
            IEnumerable<SalesInvoiceMaster> salesInvoiceMasters = await _context.SalesInvoiceMasters.Where(x => x.leadsId == customerId  && x.invoiceDate >= Convert.ToDateTime(fromDate) && x.invoiceDate <= Convert.ToDateTime(toDate)).AsNoTracking().ToListAsync();
            foreach (SalesInvoiceMaster SIMR in salesInvoiceMasters)
            {

                invoiceMasters.Add(new SalesInvoiceMaster
                {
                    invoiceNumber = SIMR.invoiceNumber,
                    invoiceDate = SIMR.invoiceDate,
                    NetTotal = SIMR.NetTotal
                });

            }
            List<CollectionReportDetailsModel> collectionReportDetailsModels = new List<CollectionReportDetailsModel>();

            decimal? openingPayment = _context.SalesCollections.Where(x => x.collectionDate < Convert.ToDateTime(fromDate)  && x.leadsId == customerId).Sum(x => x.collectionAmount);

            IEnumerable<SalesCollectionDetail> salesCollectionDetails = await _context.SalesCollectionDetails.Include(x => x.salesCollection).Where(x => x.salesCollection.leadsId == customerId  && x.salesCollection.collectionDate >= Convert.ToDateTime(fromDate) && x.salesCollection.collectionDate <= Convert.ToDateTime(toDate)).Include(x => x.salesInvoiceMaster).AsNoTracking().ToListAsync();
            foreach (SalesCollectionDetail SIMR in salesCollectionDetails)
            {

                collectionReportDetailsModels.Add(new CollectionReportDetailsModel
                {
                    invoiceNumber = SIMR.salesInvoiceMaster.invoiceNumber,
                    collectionNumber = SIMR.salesCollection.collectionNumber,
                    collectionDate = SIMR.salesCollection.collectionDate,
                    collectionAmount = SIMR.Amount
                });

            }

          

            CustomerCollectionReportVM reportVM = new CustomerCollectionReportVM
            {
                openingDue = openingDue,
                openingPayment = openingPayment,
                salesInvoiceMaster = invoiceMasters,

                collectionReportDetailsModels = collectionReportDetailsModels
            };




            return reportVM;
        }

        #endregion
        #region RepSaleInvoice
        public async Task<int> SaveRepSalesInvoiceMaster(RepSalesInvoiceMaster salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.RepSalesInvoiceMasters.Update(salesInvoiceMaster);
            else
                _context.RepSalesInvoiceMasters.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }

        public async Task<IEnumerable<RepSalesInvoiceMaster>> GetAllRepSalesInvoiceMaster()
        {
            return await _context.RepSalesInvoiceMasters.AsNoTracking().Include(x => x.leads).OrderByDescending(x => x.Id).ToListAsync();
        }



        public async Task<RepSalesInvoiceMaster> GetRepSalesInvoiceMasterById(int id)
        {
            return await _context.RepSalesInvoiceMasters.Include(x => x.leads).Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<bool> UpdateRepSalesInvoiceMasterById(int id)
        {
            RepSalesInvoiceMaster salesInvoiceMaster = await _context.RepSalesInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }
        public async Task<bool> UpdateRepSalesInvoiceMasterStockCloseById(int id)
        {
            RepSalesInvoiceMaster salesInvoiceMaster = await _context.RepSalesInvoiceMasters.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }



        public async Task<bool> DeleteRepSalesInvoiceMasterById(int id)
        {
            _context.RepSalesInvoiceMasters.Remove(_context.RepSalesInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
        #endregion

    }
}
