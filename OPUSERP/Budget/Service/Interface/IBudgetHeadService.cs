using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IBudgetHeadService
    {
        Task<bool> SaveBudgetMainHead(BudgetMainHead budgetMainHead);
        Task<IEnumerable<BudgetMainHead>> GetBudgetMainHead();
        Task<BudgetMainHead> GetBudgetMainHeadById(int id);
        Task<bool> DeleteBudgetMainHeadById(int id);

        Task<bool> SaveBudgetSubHead(BudgetSubHead budgetSubHead);
        Task<IEnumerable<BudgetSubHead>> GetBudgetSubHead();
        Task<IEnumerable<BudgetSubHead>> GetBudgetSubHeadByMainHeadId(int mainHeadId);
        Task<BudgetSubHead> GetBudgetSubHeadById(int id);
        Task<bool> DeleteBudgetSubHeadById(int id);

        Task<int> SaveBudgetHead(BudgetHead budgetHead);
        Task<IEnumerable<BudgetHead>> GetBudgetHead();
        Task<BudgetHead> GetBudgetHeadById(int id);
        Task<bool> DeleteBudgetHeadById(int id);

        Task<BudgetHead> GetBudgetHeadByCode(string code);


        Task<IEnumerable<BudgetHeadDetail>> GetBudgetHeadDetail();
        Task<BudgetHeadDetail> GetBudgetHeadDetailById(int id);
        Task<IEnumerable<BudgetHeadDetail>> GetBudgetHeadDetailByHeadId(int id);
        Task<int> SaveBudgetHeadDetail(BudgetHeadDetail budgetHeadDetail);
        Task<bool> DeleteBudgetHeadDetailById(int id);
        Task<bool> DeleteBudgetHeadDetailByHeadId(int id);

    }
}
