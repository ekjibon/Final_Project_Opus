using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.CRM.Services.Budget.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Budget
{
    public class CompanyBudgetService:ICompanyBudgetService
    {
        private readonly ERPDbContext _context;

        public CompanyBudgetService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveCompanyBudget(CompanyBudgets companyBudgets)
        {
            if (companyBudgets.Id != 0)
                _context.CompanyBudgets.Update(companyBudgets);
            else
                _context.CompanyBudgets.Add(companyBudgets);

            await _context.SaveChangesAsync();
            return companyBudgets.Id;
        }
        public async Task<IEnumerable<CompanyBudgets>> GetAllcompanyBudgets()
        {
            return await _context.CompanyBudgets.Include(x => x.company).Include(x=>x.year).AsNoTracking().ToListAsync();
        }
        public async Task<CompanyBudgets> GetcompanyBudgetsById(int id)
        {
            return await _context.CompanyBudgets.FindAsync(id);
        }
        public async Task<bool> DeletcompanyBudgetsById(int id)
        {
            _context.CompanyBudgets.Remove(_context.CompanyBudgets.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CompanyBudgets>> GetcompanyBudgetsByyearId(int yearID)
        {
            return await _context.CompanyBudgets.Include(x => x.company).Include(x => x.year).Where(x=>x.yearId==yearID).AsNoTracking().ToListAsync();
        }
    }
}
