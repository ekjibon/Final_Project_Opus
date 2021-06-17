using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
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
    public class OSalesCollectionService : IOSalesCollectionService
    {
        private readonly ERPDbContext _context;

        public OSalesCollectionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveSalesCollectionBillInfo(SalesCollectionBillInfo salesCollection)
        {
            if (salesCollection.Id != 0)
                _context.salesCollectionBillInfos.Update(salesCollection);
            else
                _context.salesCollectionBillInfos.Add(salesCollection);
            await _context.SaveChangesAsync();
            return salesCollection.Id;
        }

        public async Task<bool> UpdateSalesCollectionBillInfoById(int id)
        {
            SalesCollectionBillInfo purchaseOrderMaster = await _context.salesCollectionBillInfos.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.ispaid = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfo()
        {
            return await _context.salesCollectionBillInfos.AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).ToListAsync();
        }
        

        public async Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfoByInvoiceId(int Id)
        {
            return await _context.salesCollectionBillInfos.Where(x => x.salesInvoiceMasterId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfoByCustomerId(int Id)
        {
            return await _context.salesCollectionBillInfos.Where(x => x.relSupplierCustomerResourseId == Id && x.ispaid==0).AsNoTracking().ToListAsync();
        }

        public async Task<SalesCollectionBillInfo> GetSalesCollectionBillInfoById(int Id)
        {
            return await _context.salesCollectionBillInfos.Where(x => x.Id == Id).Include(x => x.relSupplierCustomerResourse.Leads).FirstOrDefaultAsync();
        }



        public async Task<int> SaveSalesCollection(SalesCollection salesCollection)
        {
            if (salesCollection.Id != 0)
                _context.OSalesCollections.Update(salesCollection);
            else
                _context.OSalesCollections.Add(salesCollection);
            await _context.SaveChangesAsync();
            return salesCollection.Id;
        }

        public async Task<IEnumerable<SalesCollection>> GetAllSalesCollection()
        {
            return await _context.OSalesCollections.Include(x=>x.relSupplierCustomerResourse.Leads).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesCollection>> GetSalesCollectionListById(int id)
        {
            return await _context.OSalesCollections.Where(x => x.Id == id).Include(x => x.relSupplierCustomerResourse.Leads).Include(x => x.paymentMode).AsNoTracking().Include(x => x.salesInvoiceMaster).ToListAsync();
        }

        public async Task<SalesCollection> GetSalesCollectionById(int id)
        {
            return await _context.OSalesCollections.Where(x=>x.Id==id).Include(x=>x.company).Include(x=> x.relSupplierCustomerResourse.Leads).Include(x=>x.paymentMode).FirstOrDefaultAsync();
        }
        public async Task<BillReturnPaymentMaster> GetSalesReturnPaymentById(int id)
        {
            return await _context.BillReturnPaymentMasters.Where(x => x.Id == id).Include(x => x.company).Include(x => x.relSupplierCustomerResourse.Leads).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteSalesCollectionById(int id)
        {
            _context.OSalesCollections.Remove(_context.OSalesCollections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetDuesCustomerList()
        {
            List<int?> relCustomerIds = await _context.OSalesInvoiceMasters.Where(x => x.isClose == 0).Select(x => x.relSupplierCustomerResourseId).ToListAsync();

            return await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 4).Where(x => relCustomerIds.Contains(x.Id)).Include(x => x.resource).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetCustomerListForSalesReport()
        {
            List<int?> relCustomerIds = await _context.OSalesInvoiceMasters.Select(x => x.relSupplierCustomerResourseId).ToListAsync();

            return await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 4).Where(x => relCustomerIds.Contains(x.Id)).Include(x => x.resource).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetCollectionCustomerList()
        {
            List<int?> relCustomerIds = await _context.OSalesCollections.Select(x => x.relSupplierCustomerResourseId).ToListAsync();

            return await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 4).Where(x => relCustomerIds.Contains(x.Id)).Include(x => x.Leads).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetReturnCustomerList()
        {
            List<int?> relCustomerIds = await _context.BillReturnPaymentMasters.Select(x => x.relSupplierCustomerResourseId).ToListAsync();

            return await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 4).Where(x => relCustomerIds.Contains(x.Id)).Include(x => x.resource).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CustomerWithDue>> GetCustomerWithDue()
        {
            List<int?> relCustomerIds = await _context.OSalesInvoiceMasters.Where(x => x.isClose == 0).Select(x => x.relSupplierCustomerResourseId).ToListAsync();

            IEnumerable<RelSupplierCustomerResourse> relSupplierCustomerResourses = await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 4).Where(x=> relCustomerIds.Contains(x.Id)).Include(x => x.resource).Include(x => x.Leads).AsNoTracking().ToListAsync();

            List<CustomerWithDue> data = new List<CustomerWithDue>();
            foreach (RelSupplierCustomerResourse relSupplierCustomerResourse in relSupplierCustomerResourses)
            {
                var haveToPay = _context.OSalesInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == relSupplierCustomerResourse.Id).Sum(s => s.NetTotal);
                SalesInvoiceMaster datastore = _context.OSalesInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == relSupplierCustomerResourse.Id && x.isClose == 0).FirstOrDefault();

                var collection = _context.OSalesCollections.Where(x => x.relSupplierCustomerResourseId == relSupplierCustomerResourse.Id).Sum(s => s.collectionAmount);
                var Due = haveToPay - collection;
                if (Due > 0)
                {
                    data.Add(new CustomerWithDue
                    {
                        relSupplierCustomerResourse = relSupplierCustomerResourse,
                        haveToPay = haveToPay,
                        paid = collection,
                        due = Due,
                        storeId= Convert.ToInt32(datastore.storeId)
                    });
                }

            }
            return data;
        }
        public async Task<IEnumerable<VoucherMaster>> GetSalesVoucherApproveList()
        {
            return await _context.VoucherMasters.Where(x=>x.isPosted ==0 && EF.Functions.Like(x.refNo, "%Collection%")).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VoucherMaster>> GetPurchaseVoucherApproveList()
        {
            return await _context.VoucherMasters.Where(x => x.isPosted == 0 && EF.Functions.Like(x.refNo, "%Payment%")).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateVoucherMasterisPostedById(int masterId, int status)
        {
            var voucher =  _context.VoucherMasters.Find(masterId);
            voucher.isPosted = status;
            _context.Entry(voucher).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
      
        public async Task<IEnumerable<SalesCollection>> GetSalesCollectionForManualVoucher()
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Select(x => x.refNo).ToListAsync();
            return await _context.OSalesCollections.Include(x => x.relSupplierCustomerResourse.resource).Where(x => ! refno.Contains(x.collectionNumber)).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucher()
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Select(x => x.refNo).ToListAsync();
            return await _context.PosCollectionMaster.Where(x => !refno.Contains(x.collectionNumber)).Include(x=>x.posCustomer).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucherForCard()
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Where(x=>x.voucherTypeId==8).Select(x => x.refNo).ToListAsync();
            return await _context.PosCollectionMaster.Where(x => !refno.Contains(x.collectionNumber)).Where(x=>x.paymentModeId== 4).Include(x=>x.posCustomer).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucherForMobile()
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Where(x => x.voucherTypeId == 8).Select(x => x.refNo).ToListAsync();
            return await _context.PosCollectionMaster.Where(x => !refno.Contains(x.collectionNumber)).Where(x=>x.paymentModeId== 5).Include(x=>x.posCustomer).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForVoucherForCardReport(int id, string fromDate, string toDate)
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Where(x=>x.voucherTypeId==8).Select(x => x.refNo).ToListAsync();
            return await _context.PosCollectionMaster.Where(x => !refno.Contains(x.collectionNumber)).Where(x=>x.paymentModeId== 4 && x.bankInfoId==id && x.collectionDate>=Convert.ToDateTime(fromDate)&& x.collectionDate <= Convert.ToDateTime(toDate)).Include(x=>x.posCustomer).Include(x => x.bankInfo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForVoucherForMobileReport(int id, string fromDate, string toDate)
        {
            List<string> refno = await _context.VoucherMasters.Distinct().Where(x => x.voucherTypeId == 8).Select(x => x.refNo).ToListAsync();
            return await _context.PosCollectionMaster.Where(x => !refno.Contains(x.collectionNumber)).Where(x => x.paymentModeId == 5 && x.mobileBankingTypeId == id && x.collectionDate >= Convert.ToDateTime(fromDate) && x.collectionDate <= Convert.ToDateTime(toDate)).Include(x => x.posCustomer).Include(x => x.mobileBankingType).AsNoTracking().ToListAsync();
        }

        public async Task<CollectionSummaryReport> GetSummaryCollection(int id)
        {
            var invoice = await _context.OSalesInvoiceMasters.Where(x => x.relSupplierCustomerResourseId == id).SumAsync(x => x.totalAmount);
            var collection = await _context.OSalesCollections.Where(x => x.relSupplierCustomerResourseId == id).SumAsync(x => x.Amount);
            CollectionSummaryReport collectionSummaryReport = new CollectionSummaryReport
            {
                salesInvoiceMaster = await _context.OSalesInvoiceMasters.Include(x=>x.relSupplierCustomerResourse.distributorType).Where(x=>x.relSupplierCustomerResourseId==id).ToListAsync(),
                salesCollection = await _context.OSalesCollections.Include(x => x.relSupplierCustomerResourse.distributorType).Where(x=>x.relSupplierCustomerResourseId==id).ToListAsync(),
                totalCollection = collection,
                totalInvoice = invoice,
                haveTopay = invoice - collection
            };
            return collectionSummaryReport;
         }

    }
}
