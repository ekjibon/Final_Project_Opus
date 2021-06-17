using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Services.Inventory.interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.Inventory
{
    public class PurchasePartsService: IPurchasePartsService
    {
        private readonly ERPDbContext _context;

        public PurchasePartsService(ERPDbContext context)
        {
            _context = context;
        }
        #region Purchase Parts Master
        public async Task<int> SavePurchasePartsMaster(PurchasePartsMaster purchasePartsMaster)
        {
            if (purchasePartsMaster.Id != 0)
            {
                purchasePartsMaster.updatedBy = purchasePartsMaster.createdBy;
                purchasePartsMaster.updatedAt = DateTime.Now;
                _context.PurchasePartsMasters.Update(purchasePartsMaster);
            }
            else
            {
                purchasePartsMaster.createdAt = DateTime.Now;
                _context.PurchasePartsMasters.Add(purchasePartsMaster);
            }

            await _context.SaveChangesAsync();
            return purchasePartsMaster.Id;
        }

        public async Task<IEnumerable<PurchasePartsMaster>> GetAllPurchasePartsMaster()
        {
            return await _context.PurchasePartsMasters.Include(x=>x.spareParts).AsNoTracking().ToListAsync();
        }

        public async Task<PurchasePartsMaster> GetPurchasePartsMasterById(int id)
        {
            return await _context.PurchasePartsMasters.Where(x=>x.Id==id).Include(x=>x.spareParts).FirstOrDefaultAsync();
        }

        public IQueryable<PurchasePartsMaster> GetPurchasePartsMasterByPartsId(int partsId)
        {
            return _context.PurchasePartsMasters.Where(x=>x.sparePartsId==partsId);
        }

        public async Task<bool> DeletePurchasePartsMasterById(int id)
        {
            _context.PurchasePartsMasters.Remove(_context.PurchasePartsMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Purchase Parts Details
        public async Task<int> SavePurchasePartsDetails(PurchasePartsDetail purchasePartsDetail)
        {
            if (purchasePartsDetail.Id != 0)
            {
                purchasePartsDetail.updatedBy = purchasePartsDetail.createdBy;
                purchasePartsDetail.updatedAt = DateTime.Now;
                _context.PurchasePartsDetails.Update(purchasePartsDetail);
            }
            else
            {
                purchasePartsDetail.createdAt = DateTime.Now;
                _context.PurchasePartsDetails.Add(purchasePartsDetail);
            }

            await _context.SaveChangesAsync();
            return purchasePartsDetail.Id;
        }

        public async Task<IEnumerable<PurchasePartsDetail>> GetAllPurchasePartsDetails()
        {
            return await _context.PurchasePartsDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PurchasePartsDetail> GetPurchasePartsDetailsById(int id)
        {
            return await _context.PurchasePartsDetails.FindAsync(id);
        }

        public IQueryable<PurchasePartsDetail> GetPurchasePartsDetailsByMasterId(int masterId)
        {
            return _context.PurchasePartsDetails.Where(x => x.purchasePartsMasterId == masterId && x.isUse == 0);
        }

        public IQueryable<PurchasePartsDetail> GetUnUsedPurchasePartsDetailsByMasterId(int masterId)
        {
            return _context.PurchasePartsDetails.Where(x => x.purchasePartsMasterId == masterId && x.isUse==1);
        }

        public async Task<bool> DeletePurchasePartsDetailsByMasterId(int masterId)
        {
            _context.PurchasePartsDetails.RemoveRange(_context.PurchasePartsDetails.Where(x=>x.purchasePartsMasterId==masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
