using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings
{
    public class GroupNatureService:IGroupNatureService
    {
        private readonly ERPDbContext _context;

        public GroupNatureService(ERPDbContext context)
        {
            _context = context;
        }
        #region Group Nature
        public async Task<int> SavegroupNature(GroupNature groupNature)
        {
            try
            {
                if (groupNature.Id != 0)
                {
                    _context.GroupNatures.Update(groupNature);
                }
                else
                {
                    _context.GroupNatures.Add(groupNature);
                }

                await _context.SaveChangesAsync();
                return groupNature.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<GroupNature>> GetgroupNature()
        {
            return await _context.GroupNatures.AsNoTracking().ToListAsync();
        }

       

        public async Task<GroupNature> GetgroupNatureById(int id)
        {
            try
            {
                var record = await _context.GroupNatures.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeletegroupNatureById(int id)
        {
            _context.GroupNatures.Remove(_context.GroupNatures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
