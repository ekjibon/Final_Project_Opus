
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Services.Interfaces;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ERPDbContext _context;

        public PurchaseService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StockItemViewModel>> GetReturnDetailsInvoiceList(int Id)
        {

            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<SalesInvoiceDetail> salesInvoiceDetails = _context.OSalesInvoiceDetails.Where(x => x.salesInvoiceMasterId == Id).Include(x => x.itemSpecication.Item);
            foreach (SalesInvoiceDetail invoiceDetail in salesInvoiceDetails)
            {
                var totalquantity = invoiceDetail.quantity;
                var stockDue = _context.salesReturnInvoiceDetails.Where(x => x.salesInvoiceDetailId == invoiceDetail.Id).Sum(s => s.quantity);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {

                    data.Add(new StockItemViewModel

                    {
                        Id = invoiceDetail.Id,
                        purchaseId = invoiceDetail?.salesInvoiceMasterId,
                        ItemPriceId = invoiceDetail?.itemPriceFixingId,
                        itemSpecificationId = invoiceDetail?.itemSpecicationId,
                        description = "",
                        deliveryLoacationId = 1,
                        quantity = invoiceDetail?.quantity,
                        dueQuantity = Due,
                        rate = invoiceDetail.itemPriceFixing?.price,
                        currencyId = 1,
                        vatPercent = 0,
                        taxPercent = 0,
                        itemName = invoiceDetail?.itemPriceFixing?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = invoiceDetail?.itemSpecication?.specificationName,
                        PONO = invoiceDetail.salesInvoiceMaster?.invoiceNumber,
                        unitName = invoiceDetail.itemSpecication?.Item?.unit?.unitName

                    });
                }
            }
            return data;

        }
        #region Purchase Order Master 
        public async Task<int> SavePurchaseOrderMaster(PurchaseOrdersMaster purchaseOrderMaster)
        {
            try
            {
                if (purchaseOrderMaster.Id != 0)
                {
                    purchaseOrderMaster.updatedBy = purchaseOrderMaster.createdBy;
                    purchaseOrderMaster.updatedAt = DateTime.Now;
                    _context.PurchaseOrderMasterss.Update(purchaseOrderMaster);
                }
                else
                {
                    purchaseOrderMaster.createdAt = DateTime.Now;
                    _context.PurchaseOrderMasterss.Add(purchaseOrderMaster);
                }

                await _context.SaveChangesAsync();
                return purchaseOrderMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SalesInvoiceMaster>> GetDueStockPymentInvoiceList()
        {

            IEnumerable<SalesInvoiceMaster> salesInvoiceMasters = await _context.OSalesInvoiceMasters.Where(x => x.isStockClose == 0).AsNoTracking().Include(x => x.relSupplierCustomerResourse.Leads).ToListAsync();


            return salesInvoiceMasters;
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockoutDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<SalesInvoiceDetail> salesInvoiceDetails = await _context.OSalesInvoiceDetails.Where(x => x.salesInvoiceMasterId == Id).Include(x => x.itemSpecication.Item).ToListAsync();
            foreach (SalesInvoiceDetail invoiceDetail in salesInvoiceDetails)
            {
                var totalquantity = invoiceDetail.quantity;
                var stockDue = _context.StockDetails.Where(x => x.salesInvoiceDetailId == invoiceDetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {
                    var priceFix = await _context.OItemPriceFixings.Where(x => x.itemSpecificationId == invoiceDetail.itemSpecicationId).LastOrDefaultAsync();

                    data.Add(new StockItemViewModel

                    {
                        Id = invoiceDetail.Id,
                        purchaseId = invoiceDetail?.salesInvoiceMasterId,
                        itemSpecificationId = invoiceDetail?.itemSpecicationId,
                        description = "",
                        deliveryLoacationId = 1,
                        quantity = invoiceDetail?.quantity,
                        dueQuantity = Due,
                        rate = priceFix?.price,
                        currencyId = 1,
                        vatPercent = 0,
                        taxPercent = 0,
                        itemName = invoiceDetail?.itemSpecication?.Item?.itemName,
                        itemSpecificationName = invoiceDetail?.itemSpecication?.specificationName,
                        PONO = invoiceDetail.salesInvoiceMaster?.invoiceNumber,
                        unitName = invoiceDetail.itemSpecication?.Item?.unit?.unitName

                    });
                }
            }
            return data;

        }

        public async Task<IEnumerable<PurchaseOrdersMaster>> GetPurchaseOrderMaster()
        {
            return await _context.PurchaseOrderMasterss.AsNoTracking().Include(x => x.relSupplierCustomerResourse).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrdersMaster>> GetPurchaseOrderMasterBySupplierId(int Id)
        {
            return await _context.PurchaseOrderMasterss.Where(x => x.relSupplierCustomerResourseId == Id).AsNoTracking().Include(x => x.relSupplierCustomerResourse).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PurchaseOrdersMaster> GetPurchaseOrderMasterById(int Id)
        {
            return await _context.PurchaseOrderMasterss.Include(x => x.relSupplierCustomerResourse).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public IQueryable<PurchaseOrdersMaster> GetPurchaseOrderMasterByUser(string userName)
        {
            return _context.PurchaseOrderMasterss.Where(x => x.createdBy == userName);
        }

        public async Task<PurchaseOrdersMaster> GetGetvoucherMasterById(int id)
        {
            try
            {
                var record = await (from vm in _context.PurchaseOrderMasterss
                                    select new PurchaseOrdersMaster
                                    {

                                    }).FirstOrDefaultAsync();

                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeletePurchaseOrderMasterById(int id)
        {
            _context.PurchaseOrderMasterss.Remove(_context.PurchaseOrderMasterss.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<PurchaseOrdersMaster>> GetDueStockPurchaseInvoiceList()
        {
            IEnumerable<PurchaseOrdersMaster> purchaseOrderMasters = await _context.PurchaseOrderMasterss.Where(x => x.isStockClose == 0).AsNoTracking().Include(x => x.relSupplierCustomerResourse).ToListAsync();
            return purchaseOrderMasters;
        }

       

        //public async Task<IEnumerable<PurchaseInvoiceWithDue>> GetDuePurchaseInvoiceByCustomerId(int customerId)
        //{
        //    IEnumerable<PurchaseOrdersMaster> purchaseOrderMasters = await _context.PurchaseOrderMasterss.Where(x => x.isClose == 0 && x.relSupplierCustomerResourseId == customerId).AsNoTracking().ToListAsync();

        //    List<PurchaseInvoiceWithDue> data = new List<PurchaseInvoiceWithDue>();
        //    foreach (PurchaseOrdersMaster purchaseOrderMaster in purchaseOrderMasters)
        //    {
        //        var totalAmount = purchaseOrderMaster.netTotal;
        //        var collectionDue = _context.BillPaymentDetails.Where(x => x.billReceiveInformation.purchaseOrderMasterId == purchaseOrderMaster.Id).Sum(s => s.amount);
        //        var Due = totalAmount - collectionDue;
        //        if (Due > 0)
        //        {
        //            data.Add(new PurchaseInvoiceWithDue
        //            {
        //                purchaseOrderMaster = purchaseOrderMaster,
        //                due = Due,
        //            });
        //        }
        //    }
        //    return data;
        //}

        public async Task<IEnumerable<BillReceiveInformation>> GetDueBillReceiveByCustomerId(int customerId)
        {
            return await _context.billReceiveInformation.Where(x => x.ispaid == 0 && x.relSupplierCustomerResourseId == customerId).AsNoTracking().Include(x => x.relSupplierCustomerResourse).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<bool> UpdatePurchaseOrderMasterById(int id)
        {
            PurchaseOrdersMaster purchaseOrderMaster = await _context.PurchaseOrderMasterss.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.isClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<bool> UpdatePurchaseOrderMasterStockCloseById(int id)
        {
            PurchaseOrdersMaster purchaseOrderMaster = await _context.PurchaseOrderMasterss.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }
        public async Task<bool> UpdatePurchaseOrderMasterStockOpenById(int id)
        {
            PurchaseOrdersMaster purchaseOrderMaster = await _context.PurchaseOrderMasterss.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.isStockClose = 0;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<PurchaseOrdersDetail> purchaseOrderDetails = await _context.PurchaseOrderDetailss.Where(x => x.purchaseId == Id).Include(x => x.itemSpecification.Item).ToListAsync();
            foreach (PurchaseOrdersDetail purchasedetail in purchaseOrderDetails)
            {
                var totalquantity = purchasedetail.quantity;
                var stockDue = _context.StockDetails.Where(x => x.purchaseOrdersDetailId == purchasedetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {

                    data.Add(new StockItemViewModel

                    {
                        Id = purchasedetail.Id,
                        purchaseId = purchasedetail?.purchaseId,
                        itemSpecificationId = purchasedetail.itemSpecificationId,
                        description = purchasedetail?.description,
                        //deliveryLoacationId = purchasedetail?.deliveryLoacationId,
                        quantity = purchasedetail?.quantity,
                        dueQuantity = Due,
                        rate = purchasedetail.rate,
                        currencyId = purchasedetail?.currencyId,
                        vatPercent = purchasedetail?.vatPercent,
                        taxPercent = purchasedetail?.taxPercent,
                        itemName = purchasedetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = purchasedetail?.itemSpecification?.specificationName,
                        PONO = purchasedetail.purchase?.poNo,
                        unitName = purchasedetail.itemSpecification?.Item?.unit?.unitName

                    });
                }
            }



            return data;
        }

        #endregion

        #region Purchase Order Details 
        public async Task<int> SavePurchaseOrderDetail(PurchaseOrdersDetail purchaseOrderDetail)
        {
            try
            {
                if (purchaseOrderDetail.Id != 0)
                {
                    purchaseOrderDetail.updatedBy = purchaseOrderDetail.createdBy;
                    purchaseOrderDetail.updatedAt = DateTime.Now;
                    _context.PurchaseOrderDetailss.Update(purchaseOrderDetail);
                }
                else
                {
                    purchaseOrderDetail.createdAt = DateTime.Now;
                    _context.PurchaseOrderDetailss.Add(purchaseOrderDetail);
                }

                await _context.SaveChangesAsync();
                return purchaseOrderDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<PurchaseOrdersDetail> GetPurchaseOrderDetailByPOId(int poId)
        {
            return _context.PurchaseOrderDetailss.Include(x => x.itemSpecification.Item.unit).Where(x => x.purchaseId == poId);
        }
        public Task<PurchaseOrdersDetail> GetPurchaseOrderDetailById(int poId)
        {
            return _context.PurchaseOrderDetailss.Include(x => x.itemSpecification.Item.unit).Where(x => x.Id == poId).FirstOrDefaultAsync();
        }
        public Task<PurchaseOrdersDetail> GetPurchaseOrderDetailBySpecId(int specId)
        {

            return _context.PurchaseOrderDetailss.Include(x => x.itemSpecification.Item.unit).Where(x => x.itemSpecificationId == specId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePurchaseOrderDetailByPOId(int poId)
        {
            _context.PurchaseOrderDetailss.RemoveRange(_context.PurchaseOrderDetailss.Where(x => x.purchaseId == poId));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion



        #region Delivery Location
        public async Task<int> SaveDeliveryLoacation(DeliveryLocation deliveryLoacation)
        {
            try
            {
                if (deliveryLoacation.Id != 0)
                {
                    deliveryLoacation.updatedBy = deliveryLoacation.createdBy;
                    deliveryLoacation.updatedAt = DateTime.Now;
                    _context.DeliveryLocations.Update(deliveryLoacation);
                }
                else
                {
                    deliveryLoacation.createdAt = DateTime.Now;
                    _context.DeliveryLocations.Add(deliveryLoacation);
                }

                await _context.SaveChangesAsync();
                return deliveryLoacation.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DeliveryLocation>> GetDeliveryLoacation()
        {
            return await _context.DeliveryLocations.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteDeliveryLoacationById(int id)
        {
            _context.DeliveryLocations.Remove(_context.DeliveryLocations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion



    }
}
