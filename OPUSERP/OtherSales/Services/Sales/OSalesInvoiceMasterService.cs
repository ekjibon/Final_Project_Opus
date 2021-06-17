using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Areas.POS.Models;
using OPUSERP.CRM.Data.SPModel;
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
    public class OSalesInvoiceMasterService: IOSalesInvoiceMasterService
    {
        private readonly ERPDbContext _context;

        public OSalesInvoiceMasterService(ERPDbContext context)
        {
            _context = context;
        }
        #region SaleInvoice
        public async Task<int> SaveSalesInvoiceMaster(SalesInvoiceMaster salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.OSalesInvoiceMasters.Update(salesInvoiceMaster);
            else
                _context.OSalesInvoiceMasters.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }

        public async Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMaster()
        {
            return await _context.OSalesInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).Include(x=>x.relSupplierCustomerResourse.resource).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMasterByrelSuplierCustomerId(int Id)
        {
            return await _context.OSalesInvoiceMasters.Where(x=>x.relSupplierCustomerResourseId==Id).AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).Include(x=>x.relSupplierCustomerResourse.resource).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<LeadAutoNumber> GetAutoSalesInvoiceNoBySp(string invoiceDate)
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql($"SP_AutoSalesInvoiceNo {invoiceDate}");
            return await data.FirstOrDefaultAsync();
        }

        public async Task<LeadAutoNumber> GetAutoRentSaleInvoiceNoBySp(string invoiceDate)
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql($"SP_AutoRentSalesInvoiceNo {invoiceDate}");
            return await data.FirstOrDefaultAsync();
        }
        public async Task<LeadAutoNumber> GetAutoRentServiceInvoiceNoBySp(string invoiceDate)
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql($"SP_AutoRentServiceInvoiceNo {invoiceDate}");
            return await data.FirstOrDefaultAsync();
        }

        public async Task<SalesInvoiceMaster> GetSalesInvoiceMasterById(int id)
        {
            return await _context.OSalesInvoiceMasters.Include(x => x.relSupplierCustomerResourse.Leads).Include(e=>e.relSupplierCustomerResourse).Include(x=>x.relSupplierCustomerResourse.resource).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }
        //public async Task<IEnumerable<PosSalesDetailModel>> GetSalesInvoiceMasterByDate(DateTime from,DateTime toDate)
        //{
           
        //    IEnumerable<PosInvoiceMaster> posInvoiceMasters = await _context.posInvoiceMasters.Include(x => x.posCustomer).Where(x=>x.invoiceDate>=from.Date&&x.invoiceDate<=toDate.Date).ToListAsync();
          

        

         
        //    List<PosSalesDetailModel> salesDetailModels = new List<PosSalesDetailModel>();
          
        //    foreach (PosInvoiceMaster salmaster in posInvoiceMasters)
        //    {
        //        salesDetailModels.Add(new PosSalesDetailModel
        //        {
        //            salesId=salmaster.Id,
        //            invoiceNumber = salmaster.invoiceNumber,
        //            invoiceDate = salmaster.invoiceDate?.ToString("dd-MMM-yyyy"),
        //            customerName = salmaster.posCustomer.name,
        //            phoneNumber=salmaster.posCustomer.phone,
        //            netAmount = salmaster.NetTotal,
        //            grossDiscount=salmaster.DiscountOnTotal,
        //            grossVAT=salmaster.VATOnTotal,
        //            storeId=salmaster.storeId,
        //            grossTotal=salmaster.totalAmount

        //        });

        //    }
           
        //    if (salesDetailModels == null)
        //    {
        //        salesDetailModels = new List<PosSalesDetailModel>();
        //    }
        //    return salesDetailModels;


           
        //}

        public async Task<bool> UpdateSalesInvoiceMasterById(int id)
        {
            SalesInvoiceMaster salesInvoiceMaster = await _context.OSalesInvoiceMasters.FindAsync(id);
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
            SalesInvoiceMaster salesInvoiceMaster = await _context.OSalesInvoiceMasters.FindAsync(id);
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

        public async Task<bool> UpdateSalesInvoiceReturnMasterStockCloseById(int id)
        {
            SalesReturnInvoiceMaster salesInvoiceMaster = await _context.SalesReturnInvoiceMasters.FindAsync(id);
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
            _context.OSalesInvoiceMasters.Remove(_context.OSalesInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentInvoiceWithDue>> GetDueSalesInvoice(int customerId)
        {
            IEnumerable<SalesInvoiceMaster> salesInvoiceMasters;
            if (customerId == 0)
            {
                salesInvoiceMasters = await _context.OSalesInvoiceMasters.Where(x => x.isClose == 0 ).AsNoTracking().ToListAsync();
            }
            else
            {
                salesInvoiceMasters = await _context.OSalesInvoiceMasters.Where(x => x.isClose == 0 && x.relSupplierCustomerResourseId == customerId).AsNoTracking().ToListAsync();
            }

            List<PaymentInvoiceWithDue> data = new List<PaymentInvoiceWithDue>();
            foreach (SalesInvoiceMaster salesInvoiceMaster in salesInvoiceMasters)
            {
                var totalAmount = salesInvoiceMaster.NetTotal;
                var collectionDue = _context.OSalesCollectionDetails.Where(x => x.salesCollectionBillInfo.salesInvoiceMasterId == salesInvoiceMaster.Id).Sum(s => s.Amount);
                SalesInvoiceMaster datastore = _context.OSalesInvoiceMasters.Where(x => x.Id == salesInvoiceMaster.Id && x.isClose == 0).FirstOrDefault();
                var Due = totalAmount - collectionDue;
                if (Due > 0)
                {
                    data.Add(new PaymentInvoiceWithDue
                    {
                        salesInvoiceMaster = salesInvoiceMaster,
                        due = Due,
                        storeId=datastore.storeId
                    });
                }
            }
            return data;
        }

        #endregion
        #region SaleReturn
        public async Task<int> SaveSalesReturnInvoiceMaster(SalesReturnInvoiceMaster salesReturnInvoiceMaster)
        {
            if (salesReturnInvoiceMaster.Id != 0)
                _context.SalesReturnInvoiceMasters.Update(salesReturnInvoiceMaster);
            else
                _context.SalesReturnInvoiceMasters.Add(salesReturnInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesReturnInvoiceMaster.Id;
        }
        public async Task<IEnumerable<SalesReturnInvoiceMaster>> GetAllSalesReturnInvoiceMaster()
        {
            return await _context.SalesReturnInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).Include(x => x.relSupplierCustomerResourse.resource).ToListAsync();
        }
        public async Task<SalesReturnInvoiceMaster> GetSalesReturnInvoiceMasterById(int id)
        {
            return await _context.SalesReturnInvoiceMasters.Include(x => x.relSupplierCustomerResourse.Leads).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SalesReturnInvoiceMaster>> GetAllDueSalesReturnInvoiceMaster()
        {
            return await _context.SalesReturnInvoiceMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse.resource).Where(x=>x.isStockClose==0).ToListAsync();
        }
        #endregion


        public async Task<CustomerCollectionReportVM> GetSalesInvoiceRecipt(int customerId,string fromDate,string toDate,int storeId)
        {
          
            decimal? openingDue =  _context.OSalesInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == customerId  && x.invoiceDate < Convert.ToDateTime(fromDate)).Sum(x =>x.NetTotal);
            List<SalesInvoiceMaster> invoiceMasters = new List<SalesInvoiceMaster>();
            IEnumerable < SalesInvoiceMaster > salesInvoiceMasters = await _context.OSalesInvoiceMasters.Where(x =>x.relSupplierCustomerResourseId == customerId  && x.invoiceDate >= Convert.ToDateTime(fromDate) && x.invoiceDate <= Convert.ToDateTime(toDate)).AsNoTracking().ToListAsync();
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
           
            decimal? openingPayment = _context.OSalesCollections.Where(x => x.collectionDate < Convert.ToDateTime(fromDate)  && x.relSupplierCustomerResourseId == customerId).Sum(x => x.collectionAmount);

            IEnumerable<SalesCollectionDetail> salesCollectionDetails = await _context.OSalesCollectionDetails.Include(x=>x.salesCollection).Where(x=>x.salesCollection.relSupplierCustomerResourseId==customerId  && x.salesCollection.collectionDate>= Convert.ToDateTime(fromDate) && x.salesCollection.collectionDate<=Convert.ToDateTime(toDate)).Include(x=>x.salesCollectionBillInfo).AsNoTracking().ToListAsync();
            foreach (SalesCollectionDetail SIMR in salesCollectionDetails)
            {

                collectionReportDetailsModels.Add(new CollectionReportDetailsModel
                {
                    invoiceNumber = SIMR.salesCollectionBillInfo.salesInvoiceMaster.invoiceNumber,
                    collectionNumber=SIMR.salesCollection.collectionNumber,
                    collectionDate = SIMR.salesCollection.collectionDate,
                    collectionAmount = SIMR.Amount
                });

            }
          
         decimal? openingDueR=   _context.SalesReturnInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == customerId  && x.invoiceDate < Convert.ToDateTime(fromDate)).Sum(x => x.totalAmount);

            IEnumerable<SalesReturnInvoiceMaster> salesInvoiceMastersR = await _context.SalesReturnInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == customerId  && x.invoiceDate >= Convert.ToDateTime(fromDate) && x.invoiceDate <= Convert.ToDateTime(toDate)).AsNoTracking().ToListAsync();
            foreach(SalesReturnInvoiceMaster SIMR in salesInvoiceMastersR)
            {

                invoiceMasters.Add(new SalesInvoiceMaster
                {
                    invoiceNumber=SIMR.invoiceNumber,
                    invoiceDate=SIMR.invoiceDate,
                    NetTotal=-SIMR.totalAmount
                });

            }


          
            decimal? openingPaymentR = _context.BillReturnPaymentMasters.Where(x => x.relSupplierCustomerResourseId == customerId  && x.billPaymentDate < Convert.ToDateTime(fromDate)).Sum(x => x.totalAmount);

            IEnumerable<BillReturnPaymentDetail> salesCollectionDetailsR = await _context.BillReturnPaymentDetails.Include(x => x.salesReturnInvoiceMaster).Include(x => x.billReturnPaymentMaster).Where(x => x.billReturnPaymentMaster.relSupplierCustomerResourseId == customerId  && x.billReturnPaymentMaster.billPaymentDate >= Convert.ToDateTime(fromDate) && x.billReturnPaymentMaster.billPaymentDate <= Convert.ToDateTime(toDate)).Include(x => x.billReturnPaymentMaster).AsNoTracking().ToListAsync();
            foreach (BillReturnPaymentDetail SIMR in salesCollectionDetailsR)
            {

                collectionReportDetailsModels.Add(new CollectionReportDetailsModel
                {
                    invoiceNumber = SIMR.salesReturnInvoiceMaster.invoiceNumber,
                    collectionNumber = SIMR.billReturnPaymentMaster.billPaymentNo,
                    collectionDate = SIMR.billReturnPaymentMaster.billPaymentDate,
                    collectionAmount = -SIMR.amount
                });

            }

            CustomerCollectionReportVM reportVM = new CustomerCollectionReportVM
            {
                openingDue = openingDue-openingDueR,
                openingPayment = openingPayment- openingPaymentR,
                salesInvoiceMaster= invoiceMasters,
              
                collectionReportDetailsModels= collectionReportDetailsModels
            };
           



            return reportVM;
        }

        public async Task<CustomerCollectionReportVM> GetSalesReturnInvoiceRecipt(int customerId, string fromDate, string toDate)
        {
            var openingDue = from M in _context.SalesReturnInvoiceMasters
                             where M.relSupplierCustomerResourseId == customerId && M.invoiceDate <= Convert.ToDateTime(fromDate)

                             select new SalesReturnInvoiceMaster
                             {
                                 totalAmount = M.totalAmount
                             };

            IEnumerable<SalesReturnInvoiceMaster> salesInvoiceMasters = await _context.SalesReturnInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == customerId && x.invoiceDate >= Convert.ToDateTime(fromDate) && x.invoiceDate <= Convert.ToDateTime(toDate)).AsNoTracking().ToListAsync();

            var openingPayment = from C in _context.BillReturnPaymentMasters
                                 join D in _context.BillReturnPaymentDetails on C.Id equals D.salesReturnInvoiceMasterId
                                 where C.relSupplierCustomerResourseId == customerId && C.billPaymentDate <= Convert.ToDateTime(fromDate)
                                 select new BillReturnPaymentDetail
                                 {
                                     amount = D.amount
                                 };

            IEnumerable<BillReturnPaymentDetail> salesCollectionDetails = await _context.BillReturnPaymentDetails.Include(x=>x.salesReturnInvoiceMaster).Include(x => x.billReturnPaymentMaster).Where(x => x.billReturnPaymentMaster.relSupplierCustomerResourseId == customerId && x.billReturnPaymentMaster.billPaymentDate >= Convert.ToDateTime(fromDate) && x.billReturnPaymentMaster.billPaymentDate <= Convert.ToDateTime(toDate)).Include(x => x.billReturnPaymentMaster).AsNoTracking().ToListAsync();

            CustomerCollectionReportVM reportVM = new CustomerCollectionReportVM
            {
                openingDue = openingDue.AsEnumerable().Sum(o => o.totalAmount),
                openingPayment = openingPayment.AsEnumerable().Sum(o => o.amount),
                salesReturnInvoiceMasters = salesInvoiceMasters,
                billReturnPaymentDetails = salesCollectionDetails
            };
            return reportVM;
        }
        public async Task<bool> UpdateSalesReturnInvoiceMasterById(int id)
        {
            SalesReturnInvoiceMaster salesInvoiceMaster = await _context.SalesReturnInvoiceMasters.FindAsync(id);
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

        #region Terms
        public async Task<int> SaveSalesTerms(SalesTermsConditions salesInvoiceMaster)
        {
            if (salesInvoiceMaster.Id != 0)
                _context.SalesTermsConditions.Update(salesInvoiceMaster);
            else
                _context.SalesTermsConditions.Add(salesInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesInvoiceMaster.Id;
        }





        public async Task<SalesTermsConditions> GetTermsConditionsById(int id)
        {
            return await _context.SalesTermsConditions.Include(x => x.salesInvoiceMaster).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SalesTermsConditions>> GetTermsConditionsByMasterId(int id)
        {
            return await _context.SalesTermsConditions.Include(x => x.salesInvoiceMaster).Where(x => x.salesInvoiceMasterId == id).ToListAsync();
        }




        public async Task<bool> DeleteTermsConditionsById(int id)
        {
            _context.SalesTermsConditions.Remove(_context.SalesTermsConditions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteTermsConditionsByMasterId(int id)
        {
            _context.SalesTermsConditions.RemoveRange(_context.SalesTermsConditions.Where(x => x.salesInvoiceMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }



        #endregion
    }
}
