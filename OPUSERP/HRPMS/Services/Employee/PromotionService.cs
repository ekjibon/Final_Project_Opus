using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class PromotionService : IPromotionService
    {
        private readonly ERPDbContext _context;

        public PromotionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePromotionById(int id)
        {
            _context.promotions.Remove(_context.promotions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotion()
        {
            return await _context.promotions.AsNoTracking().ToListAsync();
        }

        public async Task<Promotion> GetPromotionById(int id)
        {
            return await _context.promotions.FindAsync(id);
        }

        public async Task<IEnumerable<Promotion>> GetPromotionInfoByEmpId(int empId)
        {
            return await _context.promotions.Where(x => x.employeeId == empId).Include(x=>x.designation).Include(x=>x.salaryGrade).AsNoTracking().ToListAsync();
        }

        public async Task<int> SavePromotion(Promotion promotion)
        {
            if (promotion.Id != 0)
                _context.promotions.Update(promotion);
            else
                _context.promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion.Id;
        } 
    }
}
