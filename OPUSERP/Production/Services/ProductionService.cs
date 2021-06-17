using OPUSERP.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Production.Services
{
    public class ProductionService: IProductionService
    {
        private readonly ERPDbContext _context;

        public ProductionService(ERPDbContext context)
        {
            _context = context;
        }

        #region Production Master
        public async Task<int> SaveProductionMaster(ProductionMaster production)
        {
            if (production.Id != 0)
                _context.ProductionMasters.Update(production);
            else
                _context.ProductionMasters.Add(production);
            await _context.SaveChangesAsync();
            return production.Id;
        }

        public async Task<IEnumerable<ProductionMaster>> GetAllProductionInfo()
        {
            List<ProductionMaster> paymentModes = await _context.ProductionMasters.Where(x=>x.operationType== "production").Include(x=>x.bOM.itemSpecification).AsNoTracking().ToListAsync();
            return paymentModes;
        }

        public async Task<IEnumerable<ProductionMaster>> GetAllContractualProductionInfo()
        {
            List<ProductionMaster> paymentModes = await _context.ProductionMasters.Where(x => x.operationType == "contratual").Include(x => x.bOM.itemSpecification).AsNoTracking().ToListAsync();
            return paymentModes;
        }

        public async Task<ProductionMaster> GetAllProductionInfoById(int id)
        {
            ProductionMaster production = await _context.ProductionMasters.Include(x=>x.bOM.itemSpecification.Item.unit).Where(x=>x.Id==id).FirstOrDefaultAsync();
            return production;
        }

        public async Task<ProductionMaster> GetAllContractualProductionInfoById(int id)
        {
            ProductionMaster production = await _context.ProductionMasters.Include(x=>x.supplier).Include(x => x.bOM.itemSpecification.Item.unit).Where(x => x.Id == id).FirstOrDefaultAsync();
            return production;
        }

        public async Task<IEnumerable<ProductionMaster>> GetAllProductionByBOMId(int bomId)
        {
            List<ProductionMaster> paymentModes = await _context.ProductionMasters.Include(x => x.bOM).Where(x=>x.bOMId==bomId).AsNoTracking().ToListAsync();
            return paymentModes;
        }

        public async Task<bool> DeleteProductionbyId(int id)
        {
            _context.ProductionMasters.Remove(_context.ProductionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Production Details
        public async Task<int> SaveProductionDetails(ProductionDetails production)
        {
            if (production.Id != 0)
                _context.ProductionDetails.Update(production);
            else
                _context.ProductionDetails.Add(production);
            await _context.SaveChangesAsync();
            return production.Id;
        }

        public async Task<bool> SaveProductionDetailList(List<ProductionDetails> productionDetails)
        {
            await _context.ProductionDetails.AddRangeAsync(productionDetails);
          
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductionDetails>> GetAllProductionDetailsByProductionId(int productionId)
        {
            List<ProductionDetails> paymentModes = await _context.ProductionDetails.Include(x => x.production).Include(x=>x.bOMDetail.itemSpecification.Item).Where(x => x.productionId == productionId).AsNoTracking().ToListAsync();
            return paymentModes;
        }

        public async Task<ProductionDetails> GetProductionDetailsById(int productionId)
        {
            ProductionDetails paymentModes = await _context.ProductionDetails.Include(x => x.production).Include(x => x.bOMDetail.itemSpecification.Item).Where(x => x.Id == productionId).AsNoTracking().FirstOrDefaultAsync();
            return paymentModes;
        }

        public async Task<bool> DeleteProductionDetailsbyId(int id)
        {
            _context.ProductionDetails.Remove(_context.ProductionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
      
        public async Task<bool> DeleteProductionDetailsbyProductionId(int productionId)
        {
            _context.ProductionDetails.RemoveRange(_context.ProductionDetails.Where(x=>x.productionId==productionId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateProductionMasterStockCloseById(int id)
        {
            ProductionMaster purchaseOrderMaster = await _context.ProductionMasters.FindAsync(id);
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

        public async Task<IEnumerable<BoMProductionDetailsViewModel>> GetBoMDetailsByBoMId(int bomId,decimal? qty)
        {
            var result =await (from b in _context.BOMDetails
                         join spec in _context.ItemSpecifications on b.itemSpecificationId equals spec.Id
                         join i in _context.Items on spec.itemId equals i.Id
                         join u in _context.Units on i.unitId equals u.Id
                         where b.bOMMasterId == bomId
                               select new BoMProductionDetailsViewModel
                               {
                                   Id = b.Id,
                                   itemCode = i.itemCode,
                                   itemName=i.itemName,
                                   unitName=u.unitName,
                                   quantity = b.quantity,
                                   productQty = b.quantity * qty,
                                   productPrice = b.quantity * qty * b.rate,
                                   wastePercent=b.wastePercent
                               }).AsNoTracking().ToListAsync();
            return result;
        }

        #endregion
    }
}
