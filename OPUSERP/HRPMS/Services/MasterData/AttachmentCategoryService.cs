using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{

    public class AttachmentCategoryService : IAttachmentCategoryService
    {
        private readonly ERPDbContext _context;

        public AttachmentCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        #region Atttachment Group
        public async Task<IEnumerable<AtttachmentGroup>> GetAllAtttachmentGroup()
        {
            return await _context.atttachmentGroups.OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
       

        public async Task<AtttachmentGroup> GetAtttachmentGroupById(int id)
        {
            return await _context.atttachmentGroups.FindAsync(id);
        }

        public async Task<bool> SaveAtttachmentGroup(AtttachmentGroup atttachmentGroup)
        {
            if (atttachmentGroup.Id != 0)
                _context.atttachmentGroups.Update(atttachmentGroup);
            else
                _context.atttachmentGroups.Add(atttachmentGroup);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAtttachmentGroupById(int id)
        {
            _context.atttachmentGroups.Remove(_context.atttachmentGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region Atttachment Category
        public async Task<IEnumerable<AtttachmentCategory>> GetAllAttachmentCategory()
        {
            return await _context.atttachmentCategories.Include(a => a.atttachmentGroup).OrderByDescending(a=>a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AtttachmentCategory>> GetAllAttachmentCategoryByGroupId(int groupId)
        {
            return await _context.atttachmentCategories.Include(a => a.atttachmentGroup).Where(a => a.atttachmentGroupId == groupId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<AtttachmentCategory> GetAttachmentCategoryById(int id)
        {
            return await _context.atttachmentCategories.FindAsync(id);
        }

        public async Task<bool> SaveAttachmentCategory(AtttachmentCategory atttachmentCategory)
        {
            if (atttachmentCategory.Id != 0)
                _context.atttachmentCategories.Update(atttachmentCategory);
            else
                _context.atttachmentCategories.Add(atttachmentCategory);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAttachmentCategoryById(int id)
        {
            _context.atttachmentCategories.Remove(_context.atttachmentCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
