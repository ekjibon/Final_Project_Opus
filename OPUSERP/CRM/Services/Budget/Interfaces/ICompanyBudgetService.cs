using OPUSERP.CRM.Data.Entity.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Budget.Interfaces
{
    public interface ICompanyBudgetService
    {
        Task<int> SaveCompanyBudget(CompanyBudgets companyBudgets);
        Task<IEnumerable<CompanyBudgets>> GetAllcompanyBudgets();
        Task<CompanyBudgets> GetcompanyBudgetsById(int id);
        Task<bool> DeletcompanyBudgetsById(int id);
        Task<IEnumerable<CompanyBudgets>> GetcompanyBudgetsByyearId(int yearID);
    }
}
