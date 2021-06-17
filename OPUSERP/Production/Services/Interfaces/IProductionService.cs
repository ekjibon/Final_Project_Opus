using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.Interfaces
{
    public interface IProductionService
    {
        #region Production Master
        Task<int> SaveProductionMaster(ProductionMaster production);
        
        Task<IEnumerable<ProductionMaster>> GetAllProductionInfo();
        Task<IEnumerable<ProductionMaster>> GetAllContractualProductionInfo();
        Task<ProductionMaster> GetAllProductionInfoById(int id);
        Task<ProductionMaster> GetAllContractualProductionInfoById(int id);

        Task<IEnumerable<ProductionMaster>> GetAllProductionByBOMId(int id);

        Task<bool> DeleteProductionbyId(int id);
        Task<bool> UpdateProductionMasterStockCloseById(int id);
        #endregion

        #region Production Master
        Task<int> SaveProductionDetails(ProductionDetails productionDetails);
        Task<bool> SaveProductionDetailList(List<ProductionDetails> productionDetails);

        Task<IEnumerable<ProductionDetails>> GetAllProductionDetailsByProductionId(int productionId);

        Task<IEnumerable<BoMProductionDetailsViewModel>> GetBoMDetailsByBoMId(int bomId,decimal? qty);

        Task<bool> DeleteProductionDetailsbyId(int id);
        Task<bool> DeleteProductionDetailsbyProductionId(int productionId);
        Task<ProductionDetails> GetProductionDetailsById(int productionId);
        #endregion
    }
}
