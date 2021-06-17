using OPUSERP.CRM.Data.Entity.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Budget.Interfaces
{
    public interface ITeamBudgetService
    {
        Task<int> SaveTeamBudget(TeamBudgets teamBudgets);
        Task<IEnumerable<TeamBudgets>> GetAllTeamBudgets();
        Task<TeamBudgets> GetTeamBudgetsById(int id);
        Task<bool> DeletTeamBudgetsById(int id);
        Task<bool> DeletTeamBudgetsBycompanybudgetId(int id);
        Task<IEnumerable<TeamBudgets>> GetTeamBudgetsByyearId(int yearID);
        Task<IEnumerable<TeamBudgets>> GetTeamBudgetsBycompanybudgetId(int id);
    }
}
