using OPUSERP.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Production.Services
{
    public class ProductionPlanService: IProductionPlanService
    {
        private readonly ERPDbContext _context;

        public ProductionPlanService(ERPDbContext context)
        {
            _context = context;
        }

        #region Production Plan
        public async Task<int> SaveProductionPlan(ProductionPlan productionPlan)
        {
            try
            {
                if (productionPlan.Id != 0)
                {
                    productionPlan.updatedBy = productionPlan.createdBy;
                    productionPlan.updatedAt = DateTime.Now;
                    _context.productionPlans.Update(productionPlan);
                }
                else
                {
                    productionPlan.createdAt = DateTime.Now;
                    _context.productionPlans.Add(productionPlan);
                }

                await _context.SaveChangesAsync();
                return productionPlan.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductionPlan>> GetProductionPlan()
        {
            return await _context.productionPlans?.Include(x=>x.bOMMaster.itemSpecification).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<ProductionPlan> GetProductionPlanById(int Id)
        {
            return await _context.productionPlans.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProductionPlanById(int Id)
        {
            _context.productionPlans.Remove(_context.productionPlans.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region Production Batch
        public async Task<int> SaveProductionBatch(ProductionBatch productionBatch)
        {
            try
            {
                if (productionBatch.Id != 0)
                {
                    productionBatch.updatedBy = productionBatch.createdBy;
                    productionBatch.updatedAt = DateTime.Now;
                    _context.productionBatches.Update(productionBatch);
                }
                else
                {
                    productionBatch.createdAt = DateTime.Now;
                    _context.productionBatches.Add(productionBatch);
                }

                await _context.SaveChangesAsync();
                return productionBatch.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductionBatch>> GetProductionBatch()
        {
            return await _context.productionBatches.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ProductionBatch>> GetProductionBatchByPlanId(int id)
        {
            return await _context.productionBatches.Where(x=>x.productionPlanId==id).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProductionBatch> GetProductionBatchById(int Id)
        {
            return await _context.productionBatches.Where(x => x.Id == Id).Include(x=>x.productionPlan.bOMMaster).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProductionBatchById(int Id)
        {
            _context.productionBatches.Remove(_context.productionBatches.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductionBatchByPlanId(int Id)
        {
            _context.productionBatches.RemoveRange(await _context.productionBatches.Where(x=>x.productionPlanId==Id).ToListAsync());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


    }
}
