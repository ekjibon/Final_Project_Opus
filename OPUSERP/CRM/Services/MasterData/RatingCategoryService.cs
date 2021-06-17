using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class RatingCategoryService : IRatingCategoryService
    {
        private readonly ERPDbContext _context;

        public RatingCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRatingCategory(RatingCategory ratingCategory)
        {
            if (ratingCategory.Id != 0)
                _context.RatingCategories.Update(ratingCategory);
            else
                _context.RatingCategories.Add(ratingCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RatingCategory>> GetAllRatingCategory()
        {
            return await _context.RatingCategories.ToListAsync();
        }

        public async Task<RatingCategory> GetRatingCategoryById(int id)
        {
            return await _context.RatingCategories.FindAsync(id);
        }

        public async Task<bool> DeleteRatingCategoriesById(int id)
        {
            _context.RatingCategories.Remove(_context.RatingCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
