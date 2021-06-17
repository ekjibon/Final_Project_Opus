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
    public class TeamBudgetService: ITeamBudgetService
    {
        private readonly ERPDbContext _context;

        public TeamBudgetService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveTeamBudget(TeamBudgets teamBudgets)
        {
            if (teamBudgets.Id != 0)
                _context.TeamBudgets.Update(teamBudgets);
            else
                _context.TeamBudgets.Add(teamBudgets);

            await _context.SaveChangesAsync();
            return teamBudgets.Id;
        }
        public async Task<IEnumerable<TeamBudgets>> GetAllTeamBudgets()
        {
            return await _context.TeamBudgets.Include(x => x.companyBudgets.company).Include(x=>x.team).AsNoTracking().ToListAsync();
        }
        public async Task<TeamBudgets> GetTeamBudgetsById(int id)
        {
            return await _context.TeamBudgets.FindAsync(id);
        }
        public async Task<bool> DeletTeamBudgetsById(int id)
        {
            _context.TeamBudgets.Remove(_context.TeamBudgets.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletTeamBudgetsBycompanybudgetId(int id)
        {
            _context.TeamBudgets.RemoveRange(_context.TeamBudgets.Where(x=>x.companyBudgetsId==id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TeamBudgets>> GetTeamBudgetsByyearId(int yearID)
        {
            return await _context.TeamBudgets.Include(x => x.companyBudgets.company).Include(x => x.companyBudgets.year).Include(x=>x.team).Where(x=>x.companyBudgets.yearId==yearID).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<TeamBudgets>> GetTeamBudgetsBycompanybudgetId(int yearID)
        {
            return await _context.TeamBudgets.Include(x => x.companyBudgets.company).Include(x => x.companyBudgets.year).Include(x => x.team).Where(x => x.companyBudgetsId == yearID).AsNoTracking().ToListAsync();
        }
    }
}
