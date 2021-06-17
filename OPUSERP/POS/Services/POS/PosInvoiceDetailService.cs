using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class PosInvoiceDetailService : IPosInvoiceDetailService
    {
        private readonly ERPDbContext _context;

        public PosInvoiceDetailService(ERPDbContext context)
        {
            _context = context;
        }

        #region Pos Invoice Detail

        public async Task<IEnumerable<PosInvoiceDetail>> GetPosInvoiceDetail()
        {
            return await _context.posInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PosInvoiceDetail> GetPosInvoiceDetailById(int id)
        {
            return await _context.posInvoiceDetails.FindAsync(id);
        }

        public async Task<IEnumerable<PosInvoiceDetail>> GetPosInvoiceDetailByMasterId(int masterId)
        {
            return await _context.posInvoiceDetails.Include(x => x.itemPriceFixing.itemSpecification.Item).AsNoTracking().Where(x => x.posInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<int> SavePosInvoiceDetail(PosInvoiceDetail posInvoiceDetail)
        {
            if (posInvoiceDetail.Id != 0)
                _context.posInvoiceDetails.Update(posInvoiceDetail);
            else
                _context.posInvoiceDetails.Add(posInvoiceDetail);
            await _context.SaveChangesAsync();
            return posInvoiceDetail.Id;
        }

        public async Task<bool> DeletePosInvoiceDetailById(int id)
        {
            _context.posInvoiceDetails.Remove(_context.posInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePosInvoiceDetailByMasterId(int id)
        {
            _context.posInvoiceDetails.RemoveRange(_context.posInvoiceDetails.Where(x => x.posInvoiceMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region POS Replica Details

        public async Task<IEnumerable<PosRepInvoiceDetail>> GetPosRepInvoiceDetail()
        {
            return await _context.posRepInvoiceDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PosRepInvoiceDetail> GetPosRepInvoiceDetailById(int id)
        {
            return await _context.posRepInvoiceDetails.FindAsync(id);
        }

        public async Task<IEnumerable<PosRepInvoiceDetail>> GetPosRepInvoiceDetailByMasterId(int masterId)
        {
            return await _context.posRepInvoiceDetails.Include(x => x.itemPriceFixing.itemSpecification.Item).AsNoTracking().Where(x => x.posRepInvoiceMasterId == masterId).ToListAsync();
        }

        public async Task<int> SavePosRepInvoiceDetail(PosRepInvoiceDetail posRepInvoiceDetail)
        {
            if (posRepInvoiceDetail.Id != 0)
                _context.posRepInvoiceDetails.Update(posRepInvoiceDetail);
            else
                _context.posRepInvoiceDetails.Add(posRepInvoiceDetail);
            await _context.SaveChangesAsync();
            return posRepInvoiceDetail.Id;
        }

        public async Task<bool> DeletePosRepInvoiceDetailById(int id)
        {
            _context.posRepInvoiceDetails.Remove(_context.posRepInvoiceDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePosRepInvoiceDetailByMasterId(int id)
        {
            _context.posRepInvoiceDetails.RemoveRange(_context.posRepInvoiceDetails.Where(x => x.posRepInvoiceMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePosRepInvoiceDetailByCustomerId(int customerId)
        {
            List<PosRepInvoiceMaster> PosRepInvoiceMaster = await _context.posRepInvoiceMasters.Where(x => x.posCustomerId == customerId).ToListAsync();

            foreach (var data in PosRepInvoiceMaster)
            {
                _context.posRepInvoiceDetails.RemoveRange(_context.posRepInvoiceDetails.Where(x => x.posRepInvoiceMasterId == data.Id));
                await _context.SaveChangesAsync();
            }

            _context.posRepInvoiceMasters.RemoveRange(_context.posRepInvoiceMasters.Where(x => x.posCustomerId == customerId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region ReturnSaleDetail

        public async Task<int> SaveSalesReturnInvoiceDetail(PosSalesReturnInvoiceDetail salesReturnInvoiceDetail)
        {
            if (salesReturnInvoiceDetail.Id != 0)
                _context.PosSalesReturnInvoiceDetails.Update(salesReturnInvoiceDetail);
            else
                _context.PosSalesReturnInvoiceDetails.Add(salesReturnInvoiceDetail);
            await _context.SaveChangesAsync();
            return salesReturnInvoiceDetail.Id;
        }

        public async Task<IEnumerable<PosSalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetail()
        {
            return await _context.PosSalesReturnInvoiceDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PosSalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailBySaleMaster(int MasterId)
        {
            return await _context.PosSalesReturnInvoiceDetails.Where(x => x.salesInvoiceDetail.posInvoiceMasterId == MasterId).AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteSalesReturnInvoiceDetailByMasterId(int masterId)
        {
            _context.PosSalesReturnInvoiceDetails.RemoveRange(_context.PosSalesReturnInvoiceDetails.Where(x => x.salesReturnInvoiceMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PosSalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailByMasterId(int masterId)
        {
            return await _context.PosSalesReturnInvoiceDetails.Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.salesInvoiceDetail.posInvoiceMaster.posCustomer).AsNoTracking().Where(x => x.salesReturnInvoiceMasterId == masterId).ToListAsync();
        }
        public async Task<PosSalesReturnInvoiceDetail> GetSalesReturnInvoiceDetailById(int id)
        {
            return await _context.PosSalesReturnInvoiceDetails.FindAsync(id);
        }

        #endregion
    }
}
