using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service.Interface
{
   public interface IBudgetBranchService
    {

        Task<bool> SaveBudgetBranchAddress(BudgetBranchAddress budgetBranchAddress);
        Task<IEnumerable<BudgetBranchAddress>> GetBudgetBranchAddress();
        Task<BudgetBranchAddress> GetBudgetBranchAddressById(int id);
        Task<bool> DeleteBudgetBranchAddressById(int id);
        Task<IEnumerable<BudgetBranchAddress>> GetBudgetBranchAddressByBranchId(int id);
    }
}
