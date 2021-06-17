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
    public class AccountGroupService : IAccountGroupService
    {
        private readonly ERPDbContext _context;

        public AccountGroupService(ERPDbContext context)
        {
            _context = context;
        }
        #region Account Group
        public async Task<int> SaveaccountGroup(AccountGroup accountGroup)
        {
            try
            {
                if (accountGroup.Id != 0)
                {                   
                    _context.AccountGroups.Update(accountGroup);
                }
                else
                {
                    _context.AccountGroups.Add(accountGroup);
                }

                await _context.SaveChangesAsync();
                return accountGroup.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<AccountGroup>> GetaccountGroup()
        {
            return await _context.AccountGroups.Include(x => x.nature).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AccountGroup>> GetAccountGroupByNatureId(int? natureId)
        {
            return await _context.AccountGroups.Include(x => x.nature).Where(x => x.natureId == natureId).AsNoTracking().ToListAsync();
        }

        public async Task<AccountGroup> GetAccountGroupDetailsById(int id)
        {
            return await _context.AccountGroups.Include(x => x.parentGroup).Include(x => x.nature).Where(x=>x.Id== id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<AccountGroup> GetaccountGroupById(int id)
        {
            try
            {
                var record = await _context.AccountGroups.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeleteaccountGroupById(int id)
        {
            _context.AccountGroups.Remove(_context.AccountGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Balance Sheet Master

        public async Task<int> SaveBalanceSheetMaster(BalanceSheetMaster balanceSheetMaster)
        {
            try
            {
                if (balanceSheetMaster.Id != 0)
                {
                    _context.BalanceSheetMasters.Update(balanceSheetMaster);
                }
                else
                {
                    _context.BalanceSheetMasters.Add(balanceSheetMaster);
                }

                await _context.SaveChangesAsync();
                return balanceSheetMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<BalanceSheetMaster>> GetBalanceSheetMaster()
        {
            return await _context.BalanceSheetMasters.Include(x => x.accountGroup.nature).AsNoTracking().ToListAsync();
        }
        public async Task<BalanceSheetMaster> GetBalanceSheetMasterById(int Id)
        {
            return await _context.BalanceSheetMasters.FindAsync(Id);
        }
        public async Task<bool> DeleteBalanceSheetMasterById(int id)
        {
            _context.BalanceSheetMasters.Remove(_context.BalanceSheetMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion
        #region Balance Sheet Notes

        public async Task<int> SavenoteMaster(NoteMaster noteMaster)
        {
            try
            {
                if (noteMaster.Id != 0)
                {
                    _context.NoteMasters.Update(noteMaster);
                }
                else
                {
                    _context.NoteMasters.Add(noteMaster);
                }

                await _context.SaveChangesAsync();
                return noteMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<NoteMaster>> GetNoteMaster()
        {
            return await _context.NoteMasters.Include(x => x.balanceSheetMaster).AsNoTracking().ToListAsync();
        }
        public async Task<NoteMaster> GetNoteMasterById(int Id)
        {
            return await _context.NoteMasters.FindAsync(Id);
        }
        public async Task<IEnumerable<NoteMaster>> GetNoteMasterByMasterId(int Id)
        {
            return await _context.NoteMasters.Include(x => x.balanceSheetMaster).Where(x=>x.balanceSheetMasterId==Id).AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteNoteMasterById(int id)
        {
            _context.NoteMasters.Remove(_context.NoteMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
        #region Balance Sheet Details

        public async Task<int> SaveBalanceSheetDetails(BalanceSheetDetails balanceSheetDetails)
        {
            try
            {
                if (balanceSheetDetails.Id != 0)
                {
                    _context.BalanceSheetDetails.Update(balanceSheetDetails);
                }
                else
                {
                    _context.BalanceSheetDetails.Add(balanceSheetDetails);
                }

                await _context.SaveChangesAsync();
                return balanceSheetDetails.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<BalanceSheetDetails> GetBalanceSheetDetailsById(int Id)
        {
            return await _context.BalanceSheetDetails.Include(x => x.ledger).Include(x => x.noteMaster.balanceSheetMaster).Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetailsByNoteMasterId(int Id)
        {
            return await _context.BalanceSheetDetails.Include(x => x.ledger).Include(x => x.noteMaster.balanceSheetMaster).Where(x => x.noteMasterId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetails()
        {
            return await _context.BalanceSheetDetails.Include(x=>x.ledger).Include(x=>x.noteMaster.balanceSheetMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetailsByBalanceSheetMasterId(int Id)
        {
            return await _context.BalanceSheetDetails.Include(x => x.ledger).Include(x => x.noteMaster.balanceSheetMaster).Where(x => x.noteMaster.balanceSheetMasterId == Id).AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteBalanceSheetDetailsById(int id)
        {
            _context.BalanceSheetDetails.Remove(_context.BalanceSheetDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


    }
}
