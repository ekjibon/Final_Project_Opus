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
    public class MemberBudgetService: IMemberBudgetService
    {
        private readonly ERPDbContext _context;

        public MemberBudgetService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveMemberBudget(MemberBudgets memberBudgets)
        {
            if (memberBudgets.Id != 0)
                _context.MemberBudgets.Update(memberBudgets);
            else
                _context.MemberBudgets.Add(memberBudgets);

            await _context.SaveChangesAsync();
            return memberBudgets.Id;
        }
        public async Task<IEnumerable<MemberBudgets>> GetAllMemberBudgets()
        {
            return await _context.MemberBudgets.Include(x => x.teamBudgets.companyBudgets.company).Include(x=>x.team).AsNoTracking().ToListAsync();
        }
        public async Task<MemberBudgets> GetMemberBudgetsById(int id)
        {
            return await _context.MemberBudgets.FindAsync(id);
        }
        public async Task<bool> DeletMemberBudgetsById(int id)
        {
            _context.MemberBudgets.Remove(_context.MemberBudgets.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletMemberBudgetsBycompanybudgetId(int id)
        {
            _context.MemberBudgets.RemoveRange(_context.MemberBudgets.Where(x=>x.teamBudgets.companyBudgetsId==id));

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletMemberBudgetsByteambudgetId(int id)
        {
            _context.MemberBudgets.RemoveRange(_context.MemberBudgets.Where(x => x.teamBudgetsId == id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<MemberBudgets>> GetMemberBudgetsByyearId(int yearID)
        {
            return await _context.MemberBudgets.Include(x => x.teamBudgets.companyBudgets.company).Include(x => x.teamBudgets.companyBudgets.year).Include(x=>x.team).Where(x=>x.teamBudgets.companyBudgets.yearId==yearID).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MemberBudgets>> GetMemberBudgetsBycompanybudgetId(int yearID)
        {
            return await _context.MemberBudgets.Include(x => x.teamBudgets.companyBudgets.company).Include(x => x.teamBudgets.companyBudgets.year).Include(x => x.team).Where(x => x.teamBudgets.companyBudgetsId == yearID).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MemberBudgets>> GetMemberBudgetsByteambudgetId(int yearID)
        {
            return await _context.MemberBudgets.Include(x => x.teamBudgets.companyBudgets.company).Include(x => x.teamBudgets.companyBudgets.year).Include(x => x.team).Where(x => x.teamBudgetsId == yearID).AsNoTracking().ToListAsync();
        }
    }
}
