using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.FDR
{
    public class BankChargeMasterService: IBankChargeMasterService
    {
        private readonly ERPDbContext _context;

        public BankChargeMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveBankChargeMaster(BankChargeMaster bankChargeMaster)
        {
            try
            {
                if (bankChargeMaster.Id != 0)
                {
                    _context.bankChargeMasters.Update(bankChargeMaster);
                }
                else
                {
                    _context.bankChargeMasters.Add(bankChargeMaster);
                }

                await _context.SaveChangesAsync();
                return bankChargeMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<BankChargeMaster>> GetBankChargeMaster()
        {
            try
            {
                return await _context.bankChargeMasters
                .Include(x => x.bankBranchDetails.bank).Include(x => x.ledgerRelation.ledger).AsNoTracking()
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        

        public async Task<BankChargeMaster> GetBankChargeMasterById(int id)
        {
            try
            {
                var record = await _context.bankChargeMasters.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeleteBankChargeMasterById(int id)
        {
            _context.bankChargeMasters.Remove(_context.bankChargeMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        
    }
}
