using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IBudgetDisbursmentMasterService
    {
        Task<IEnumerable<BudgetDisbursementMaster>> GetBudgetDisbursementMaster();
        Task<IEnumerable<BudgetDisbursementMaster>> GetBudgetDisbursementMasterByBranchId(int id);
        Task<BudgetDisbursementMaster> GetBudgetDisbursementMasterById(int id);
        Task<int> SaveBudgetDisbursementMaster(BudgetDisbursementMaster budgetDisbursementMaster);
        Task<bool> DeleteBudgetDisbursementMasterById(int id);
        Task<bool> SaveBudgetDisbursementDetail(BudgetDisbursementDetail budgetDisbursementDetail);
        Task<IEnumerable<BudgetDisbursementDetail>> GetBudgetDisbursementDetail();
        Task<IEnumerable<BudgetDisbursementDetail>> GetBudgetDisbursementDetailBymasterId(int id);

        Task<BudgetDisbursementDetail> GetBudgetDisbursementDetailById(int id);
        Task<bool> DeleteBudgetDisbursementDetailById(int id);
        Task<bool> DeleteBudgetDisbursementDetailBymasterId(int id);
    }
}
