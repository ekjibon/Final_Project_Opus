using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.Interfaces
{
    public interface IBOMService
    {
        #region BOM Master
        Task<int> SaveBOMMaster(BOMMaster bOMMaster);
        Task<IEnumerable<BOMMaster>> GetAllBOMMaster();
        Task<BOMMaster> GetBOMMasterById(int Id);
        Task<bool> DeleteBOMMasterById(int Id);
        Task<bool> UpdateBOMMasterById(int id, int itemspecId);
        #endregion

        #region BOM Details
        Task<int> SaveBOMDetail(BOMDetail bOMDetail);
        Task<IEnumerable<BOMDetail>> GetBOMDetailByBOMMasterId(int BOMMasterId);
        Task<BOMDetail> GetAllBOMDetailsById(int Id);
        Task<bool> DeleteBOMDetailById(int id);
        Task<bool> DeleteBOMDetailByBOMMasterId(int BOMMasterId);
        #endregion

        #region Overhead Cost
        Task<int> SaveOverheadCost(OverheadCost overheadCost);
        Task<OverheadCost> GetOverheadCostById(int id);
        Task<IEnumerable<OverheadCost>> GetAllOverheadCost();
        #endregion

        #region BOM Overhead Detail
        Task<int> SaveBOMOverheadDetail(BOMOverheadDetail bOMOverheadDetail);
        Task<IEnumerable<BOMOverheadDetail>> GetBOMOverheadDetailByBOMMasterId(int BOMMasterId);
        Task<BOMOverheadDetail> GetBOMOverheadDetailById(int Id);
        Task<bool> DeleteBOMOverheadDetailById(int id);
        Task<bool> DeleteBOMOverheadDetailByBOMMasterId(int BOMMasterId);
        #endregion
    }
}
