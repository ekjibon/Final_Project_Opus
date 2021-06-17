using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IHOBudgetRequsitionService
    {
        Task<int> SaveBudgetRequsitionMaster(HOBudgetRequsitionMaster budgetRequsitionMaster);
        Task<IEnumerable<HOBudgetRequsitionMaster>> GetBudgetRequsitionMaster();
        Task<HOBudgetRequsitionMaster> GetBudgetRequsitionMasterById(int id);
        Task<IEnumerable<HOBudgetRequsitionMaster>> GetBudgetRequsitionMasterByUserId(int UserId);
        Task<bool> DeleteBudgetRequsitionMasterById(int id);
        Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterForApproval(int userId);

        Task<bool> SaveBudgetRequsitionDetail(HOBudgetRequsitionDetail budgetRequsitionDetail);
        Task<IEnumerable<HOBudgetRequsitionDetail>> GetBudgetRequsitionDetail();
        Task<HOBudgetRequsitionDetail> GetBudgetRequsitionDetailById(int id);
        Task<bool> DeleteBudgetRequsitionDetailById(int id);
        Task<IEnumerable<HOBudgetRequsitionDetail>> GetBudgetRequsitionDetailBymasterId(int id);
        Task<bool> DeleteBudgetRequsitionDetailBymasterId(int id);

        Task<IEnumerable<ColumnHeading>> GetAllColumnBySp();
        void UpdateBudgetRequsitionStatusById(int reqId, int status);
        Task<IEnumerable<HOBudgetRequsitionDetail>> PROCESS_HOBudgetRequsitionDetails(int fiscalYearId, int hOBudgetRequsitionMasterId);
        Task<IEnumerable<HOBudgetRequsitionDetail>> PROCESSHOBudgetRequsition(int fiscalYearId);
    }
}
