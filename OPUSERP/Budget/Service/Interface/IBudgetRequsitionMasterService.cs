using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IBudgetRequsitionMasterService
    {
        Task<IEnumerable<FiscalYear>> GetFiscalYear();
        Task<FiscalYear> GetFiscalYearById(int id);
        Task<int> SaveFiscalYear(FiscalYear fiscalYear);
        Task<bool> DeleteFiscalYearById(int id);


        Task<int> SaveBudgetRequsitionMaster(BudgetRequsitionMaster budgetRequsitionMaster);
        Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMaster();
        Task<BudgetRequsitionMaster> GetBudgetRequsitionMasterById(int id);
        Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByBranchId(int id);
        Task<bool> DeleteBudgetRequsitionMasterById(int id);
        Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterForApproval(int userId);
        Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByBranchIdAndType(int id, int type);

        Task<bool> SaveBudgetRequsitionDetail(BudgetRequsitionDetail budgetRequsitionDetail);
        Task<IEnumerable<BudgetRequsitionDetail>> GetBudgetRequsitionDetail();
        Task<BudgetRequsitionDetail> GetBudgetRequsitionDetailById(int id);
        Task<bool> DeleteBudgetRequsitionDetailById(int id);
        Task<IEnumerable<BudgetRequsitionDetail>> GetBudgetRequsitionDetailBymasterId(int id);
        Task<bool> DeleteBudgetRequsitionDetailBymasterId(int id);
        Task<IEnumerable<BudgetRequsitionMaster>> GetBudgetRequsitionMasterByuserId(int id);
        Task<IEnumerable<ColumnHeading>> GetAllColumnBySp();
        void UpdateBudgetRequsitionStatusById(int reqId, int status);
        void UpdateBudgetRequsitionIsProcessByYear(int year);
        void UpdateBudgetRequsitionStatusFinById(int reqId, int status);

        Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFin();
        Task<IEnumerable<BranchBudgetViewModel>> GetBudgetRequsitionMasterFinForBranchApprove(int statusId, string userName);
        Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByBranchId(int id);
        Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByuserId(int id);
        Task<IEnumerable<BudgetRequsitionMasterFin>> GetBudgetRequsitionMasterFinByBranchIdAndType(int id, int type);
        Task<BudgetRequsitionMasterFin> GetBudgetRequsitionMasterFinById(int id);
        Task<int> SaveBudgetRequsitionMasterFin(BudgetRequsitionMasterFin budgetRequsitionMaster);
        Task<bool> DeleteBudgetRequsitionMasterFinById(int id);
        Task<bool> SaveBudgetRequsitionDetailFin(BudgetRequsitionDetailFin budgetRequsitionDetail);
        Task<IEnumerable<BudgetRequsitionDetailFin>> GetBudgetRequsitionDetailFin();
        Task<IEnumerable<BudgetRequsitionDetailFin>> GetBudgetRequsitionDetailFinBymasterId(int id);
        Task<IEnumerable<BudgetRequisitionApprovedListViewModel>> GetBudgetRequsitionMasterFinForApproval(int userId);
        Task<BudgetRequsitionDetailFin> GetBudgetRequsitionDetailFinById(int id);
        Task<bool> DeleteBudgetRequsitionDetailFinById(int id);
        Task<bool> DeleteBudgetRequsitionDetailFinBymasterId(int id);
    }
}
