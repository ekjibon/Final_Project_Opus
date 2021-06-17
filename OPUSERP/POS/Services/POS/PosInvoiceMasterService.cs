using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class PosInvoiceMasterService: IPosInvoiceMasterService
    {
        private readonly ERPDbContext _context;

        public PosInvoiceMasterService(ERPDbContext context)
        {
            _context = context;
        }

        #region POS Replica Master

        public async Task<IEnumerable<PosInvoiceMaster>> GetPosInvoiceMaster()
        {
            return await _context.posInvoiceMasters.AsNoTracking().Include(x=>x.posCustomer).OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<PosInvoiceMaster> GetPosInvoiceMasterById(int id)
        {
            return await _context.posInvoiceMasters.Where(x=>x.Id==id).Include(x => x.posCustomer).FirstOrDefaultAsync();
        }

        public async Task<int> SavePosInvoiceMaster(PosInvoiceMaster posInvoiceMaster)
        {
            if (posInvoiceMaster.Id != 0)
                _context.posInvoiceMasters.Update(posInvoiceMaster);
            else
                _context.posInvoiceMasters.Add(posInvoiceMaster);
            await _context.SaveChangesAsync();
            return posInvoiceMaster.Id;
        }
        public async Task<bool> UpdateSalesReturnInvoiceMasterById(int id)
        {
            PosSalesReturnInvoiceMaster salesInvoiceMaster = await _context.PosSalesReturnInvoiceMasters.FindAsync(id);
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

        public async Task<bool> DeletePosInvoiceMasterById(int id)
        {
            _context.posInvoiceMasters.Remove(_context.posInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region POS Replica Master

        public async Task<IEnumerable<PosRepInvoiceMaster>> GetPosRepInvoiceMaster()
        {          
            return await _context.posRepInvoiceMasters.AsNoTracking().Include(x => x.posCustomer).ToListAsync();
        }

        public async Task<IEnumerable<PosRepInvoiceMaster>> GetPosRepInvoiceMasterbyDateRange(DateTime fromDate,DateTime toDate)
        {

            return await _context.posRepInvoiceMasters.Where(x=>x.invoiceDate>=fromDate.Date&&x.invoiceDate<=toDate.Date).AsNoTracking().Include(x => x.posCustomer).ToListAsync();
        }

        public async Task<PosRepInvoiceMaster> GetPosRepInvoiceMasterById(int id)
        {
            return await _context.posRepInvoiceMasters.Where(x => x.Id == id).Include(x => x.posCustomer).FirstOrDefaultAsync();
        }

        public async Task<int> SavePosRepInvoiceMaster(PosRepInvoiceMaster posRepInvoiceMaster)
        {
            if (posRepInvoiceMaster.Id != 0)
                _context.posRepInvoiceMasters.Update(posRepInvoiceMaster);
            else
                _context.posRepInvoiceMasters.Add(posRepInvoiceMaster);
            await _context.SaveChangesAsync();
            return posRepInvoiceMaster.Id;
        }

        public async Task<bool> DeletePosRepInvoiceMasterById(int id)
        {
            _context.posInvoiceMasters.Remove(_context.posInvoiceMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region SalesReturnInvoice

        public async Task<int> SaveSalesReturnInvoiceMaster(PosSalesReturnInvoiceMaster salesReturnInvoiceMaster)
        {
            if (salesReturnInvoiceMaster.Id != 0)
                _context.PosSalesReturnInvoiceMasters.Update(salesReturnInvoiceMaster);
            else
                _context.PosSalesReturnInvoiceMasters.Add(salesReturnInvoiceMaster);
            await _context.SaveChangesAsync();
            return salesReturnInvoiceMaster.Id;
        }

        public async Task<IEnumerable<PosSalesReturnInvoiceMaster>> GetAllSalesReturnInvoiceMaster()
        {
            return await _context.PosSalesReturnInvoiceMasters.AsNoTracking().Include(x => x.posCustomer).ToListAsync();
        }

        public async Task<PosSalesReturnInvoiceMaster> GetSalesReturnInvoiceMasterById(int id)
        {
            return await _context.PosSalesReturnInvoiceMasters.Include(x => x.posCustomer).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PosSalesReturnInvoiceMaster>> GetAllDueSalesReturnInvoiceMaster()
        {
            return await _context.PosSalesReturnInvoiceMasters.AsNoTracking().Include(x => x.posCustomer).Where(x => x.isStockClose == 0).ToListAsync();
        }

        public async Task<IEnumerable<PosInvoiceMaster>> GetSaleInvoiceListForReturn()
        {

            IEnumerable<int> dataIds = await _context.PosSalesReturnInvoiceMasters.Where(x => x.isClose == 1).Select(x => x.Id).ToListAsync();
            IEnumerable<int?> masterId = await _context.PosSalesReturnInvoiceDetails.Where(x => dataIds.Contains((int)x.salesReturnInvoiceMasterId)).Select(x => x.salesInvoiceDetail.posInvoiceMasterId).ToListAsync();

            IEnumerable<PosInvoiceMaster> salesInvoiceMasters = await _context.posInvoiceMasters.AsNoTracking().Include(x => x.posCustomer).Where(x => !masterId.Contains(x.Id)).ToListAsync();


            return salesInvoiceMasters;



        }

        public async Task<IEnumerable<StockItemViewModel>> GetReturnDetailsInvoiceList(int Id)
        {

            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<PosInvoiceDetail> salesInvoiceDetails = _context.posInvoiceDetails.Where(x => x.posInvoiceMasterId == Id).Include(x => x.itemPriceFixing.itemSpecification.Item);
            foreach (PosInvoiceDetail invoiceDetail in salesInvoiceDetails)
            {
                var totalquantity = invoiceDetail.quantity;
                var stockDue = _context.PosSalesReturnInvoiceDetails.Where(x => x.salesInvoiceDetailId == invoiceDetail.Id).Sum(s => s.quantity);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {

                    data.Add(new StockItemViewModel

                    {
                        Id = invoiceDetail.Id,
                        purchaseId = invoiceDetail?.posInvoiceMasterId,
                        ItemPriceId = invoiceDetail?.itemPriceFixingId,
                        itemSpecificationId = invoiceDetail?.itemPriceFixing?.itemSpecificationId,
                        description = "",
                        deliveryLoacationId = 1,
                        quantity = invoiceDetail?.quantity,
                        dueQuantity = Due,
                        rate = invoiceDetail.itemPriceFixing?.price,
                        currencyId = 1,
                        vatPercent = 0,
                        taxPercent = 0,
                        itemName = invoiceDetail?.itemPriceFixing?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = invoiceDetail?.itemPriceFixing?.itemSpecification?.specificationName,
                        PONO = invoiceDetail.posInvoiceMaster?.invoiceNumber,
                        unitName = invoiceDetail.itemPriceFixing.itemSpecification?.Item?.unit?.unitName

                    });
                }
            }
            return data;

        }

        #endregion
    }
}
