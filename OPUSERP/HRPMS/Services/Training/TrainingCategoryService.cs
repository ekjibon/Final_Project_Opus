using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingCategoryService : ITrainingCategoryService
    {
        private readonly ERPDbContext _context;

        public TrainingCategoryService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteModuleTrainingCategoryId(int id)
        {
            _context.moduleTrainingCategories.Remove(_context.moduleTrainingCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ModuleTrainingCategory>> GetAllModuleTrainingCategory()
        {
            return await _context.moduleTrainingCategories.AsNoTracking().ToListAsync();
        }

        public async Task<ModuleTrainingCategory> GetModuleTrainingCategoryId(int id)
        {
            return await _context.moduleTrainingCategories.FindAsync(id);
        }

        public async Task<bool> SaveModuleTrainingCategory(ModuleTrainingCategory moduleTtrainingCategory)
        {
            if (moduleTtrainingCategory.Id != 0)
                _context.moduleTrainingCategories.Update(moduleTtrainingCategory);
            else
                _context.moduleTrainingCategories.Add(moduleTtrainingCategory);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
