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
    public class ProgramMainCategoryService: IProgramMainCategoryService
    {
        private readonly ERPDbContext _context;

        public ProgramMainCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramMainCategory(ProgramMainCategory  programMainCategoryService)
        {
            if (programMainCategoryService.Id != 0)
            {
                programMainCategoryService.updatedBy = programMainCategoryService.createdBy;
                programMainCategoryService.updatedAt = DateTime.Now;
                _context.programMainCategories.Update(programMainCategoryService);
            }
            else
            {
                programMainCategoryService.createdAt = DateTime.Now;
                _context.programMainCategories.Add(programMainCategoryService);
            }
            await _context.SaveChangesAsync();
            return programMainCategoryService.Id;
        }

        public async Task<IEnumerable<ProgramMainCategory>> GetProgramMainCategory()
        {
            return await _context.programMainCategories.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramMainCategory> GetProgramMainCategoryById(int id)
        {
            return await _context.programMainCategories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramMainCategoryById(int id)
        {
            _context.programMainCategories.Remove(_context.programMainCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
