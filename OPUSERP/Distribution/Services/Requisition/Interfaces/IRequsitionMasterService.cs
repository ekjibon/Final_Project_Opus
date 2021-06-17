using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.Requisition.Interfaces
{
    public interface IRequsitionMasterService
    {
        #region Requistion
        Task<int> SaveRequisitionMaster(RequisitionMaster requisitionMaster);
        Task<IEnumerable<RequisitionMaster>> GetAllRequistionMaster();
        Task<RequisitionMaster> GetRequistionMasterById(int id);
        Task<IEnumerable<RequisitionMaster>> GetRequistionMasterBydistributorId(int id);

        Task<IEnumerable<RequisitionMaster>> GetRequistionMasterByareaId(int id);
        Task<bool> DeleteRequisitionMasterById(int id);
        Task<IEnumerable<RequisitionApprovalViewModel>> GetBudgetRequsitionMasterForApproval(int userId);

        #endregion

    }
}
