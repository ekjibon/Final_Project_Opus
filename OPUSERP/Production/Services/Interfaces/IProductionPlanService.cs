using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.Interfaces
{
   public interface IProductionPlanService
    {
        Task<int> SaveProductionPlan(ProductionPlan productionPlan);
        Task<IEnumerable<ProductionPlan>> GetProductionPlan();
        Task<ProductionPlan> GetProductionPlanById(int Id);
        Task<bool> DeleteProductionPlanById(int Id);

        Task<int> SaveProductionBatch(ProductionBatch productionBatch);
        Task<IEnumerable<ProductionBatch>> GetProductionBatch();
        Task<IEnumerable<ProductionBatch>> GetProductionBatchByPlanId(int id);
        Task<ProductionBatch> GetProductionBatchById(int Id);
        Task<bool> DeleteProductionBatchById(int Id);
        Task<bool> DeleteProductionBatchByPlanId(int Id);
    }
}
