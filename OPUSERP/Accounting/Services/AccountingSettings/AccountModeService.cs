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
    public class AccountModeService : IAccountModeService
    {
        private readonly ERPDbContext _context;

        public AccountModeService(ERPDbContext context)
        {
            _context = context;
        }
        #region Account Mode
        public async Task<int> SaveaccountMode(AccountMode accountMode)
        {
            try
            {
                if (accountMode.Id != 0)
                {
                    _context.AccountModes.Update(accountMode);
                }
                else
                {
                    _context.AccountModes.Add(accountMode);
                }

                await _context.SaveChangesAsync();
                return accountMode.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<AccountMode>> GetaccountMode()
        {
            return await _context.AccountModes.AsNoTracking().ToListAsync();
        }

       

        public async Task<AccountMode> GetaccountModeById(int id)
        {
            try
            {
                var record = await _context.AccountModes.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeleteaccountModeById(int id)
        {
            _context.AccountModes.Remove(_context.AccountModes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
