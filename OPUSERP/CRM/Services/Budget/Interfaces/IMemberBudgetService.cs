using OPUSERP.CRM.Data.Entity.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Budget.Interfaces
{
    public interface IMemberBudgetService
    {
        Task<int> SaveMemberBudget(MemberBudgets memberBudgets);
        Task<IEnumerable<MemberBudgets>> GetAllMemberBudgets();
        Task<MemberBudgets> GetMemberBudgetsById(int id);
        Task<bool> DeletMemberBudgetsById(int id);
        Task<bool> DeletMemberBudgetsBycompanybudgetId(int id);
        Task<bool> DeletMemberBudgetsByteambudgetId(int id);
        Task<IEnumerable<MemberBudgets>> GetMemberBudgetsByyearId(int yearID);
        Task<IEnumerable<MemberBudgets>> GetMemberBudgetsBycompanybudgetId(int yearID);
        Task<IEnumerable<MemberBudgets>> GetMemberBudgetsByteambudgetId(int yearID);
    }
}
