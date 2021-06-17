using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramCategoryService: IProgramCategoryService
    {
        private readonly ERPDbContext _context;

        public ProgramCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramCategory(ProgramCategory programCategory)
        {
            if (programCategory.Id != 0)
            {
                programCategory.updatedBy = programCategory.createdBy;
                programCategory.updatedAt = DateTime.Now;
                _context.programCategories.Update(programCategory);
            }
            else
            {
                programCategory.createdAt = DateTime.Now;
                _context.programCategories.Add(programCategory);
            }
            await _context.SaveChangesAsync();
            return programCategory.Id;
        }

        public async Task<IEnumerable<ProgramCategory>> GetProgramCategory()
        {
            return await _context.programCategories.Include(x=>x.programMainCategory).AsNoTracking().ToListAsync();
        }

        public async Task<ProgramCategory> GetProgramCategoryById(int id)
        {
            return await _context.programCategories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ProgramCategory>> GetProgramCategoryByCatId(int id)
        {
            return await _context.programCategories.Where(x => x.programMainCategoryId == id).ToListAsync();
        }

        public async Task<bool> DeleteProgramCategoryById(int id)
        {
            _context.programCategories.Remove(_context.programCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
