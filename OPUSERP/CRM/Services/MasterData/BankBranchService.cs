using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class BankBranchService : IBankBranchService
    {
        private readonly ERPDbContext _context;

        public BankBranchService(ERPDbContext context)
        {
            _context = context;
        }
        #region FIType
        public async Task<bool> SaveFIType(FIType fIType)
        {
            if (fIType.Id != 0)
                _context.FITypes.Update(fIType);
            else
                _context.FITypes.Add(fIType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FIType>> GetAllFIType()
        {
            return await _context.FITypes.ToListAsync();
        }

        public async Task<FIType> GeFITypeById(int id)
        {
            return await _context.FITypes.FindAsync(id);
        }

        public async Task<bool> DeleteFITypeById(int id)
        {
            _context.FITypes.Remove(_context.FITypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Bank
        public async Task<bool> SaveBank(Bank bank)
        {
            if (bank.Id != 0)
                _context.Banks.Update(bank);
            else
                _context.Banks.Add(bank);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bank>> GetAllBank()
        {
            return await _context.Banks.Include(x => x.fiType).ToListAsync();
        }
        public async Task<IEnumerable<Bank>> GetAllBankbyFIId(int Id)
        {
            return await _context.Banks.Include(x => x.fiType).Where(x=>x.fiTypeId==Id).ToListAsync();
        }

        public async Task<Bank> GeBankById(int id)
        {
            return await _context.Banks.FindAsync(id);
        }

        public async Task<bool> DeleteBankById(int id)
        {
            _context.Banks.Remove(_context.Banks.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Bank Branch
        public async Task<bool> SaveBankBranch(BankBranch bankBranch)
        {
            if (bankBranch.Id != 0)
                _context.BankBranches.Update(bankBranch);
            else
                _context.BankBranches.Add(bankBranch);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankBranch>> GetAllBankBranch()
        {
            return await _context.BankBranches.ToListAsync();
        }

        public async Task<BankBranch> GeBankBranchById(int id)
        {
            return await _context.BankBranches.FindAsync(id);
        }

        public async Task<bool> DeleteBankBranchById(int id)
        {
            _context.BankBranches.Remove(_context.BankBranches.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region BankBranchDetails
        public async Task<bool> SaveBankBranchDetails(BankBranchDetails bankBranchDetails)
        {
            if (bankBranchDetails.Id != 0)
                _context.BankBranchDetails.Update(bankBranchDetails);
            else
                _context.BankBranchDetails.Add(bankBranchDetails);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankBranchDetails>> GetAllBankBranchDetails()
        {
            return await _context.BankBranchDetails.Include(x => x.bank).Include(x => x.bankBranch).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BankBranchDetails>> GetBranchByBankId(int bankId)
        {
            return await _context.BankBranchDetails.Include(x => x.bankBranch).Where(x => x.bankId == bankId).AsNoTracking().ToListAsync();
        }

        //public async Task<IEnumerable<BankBranchDetails>> GetDistinctBank()
        //{

        //    List<Bank> BankBranchDetailsLsit = new List<Bank>();

        //    List<BankBranchDetails> BankBranchDetails = await _context.BankBranchDetails.ToListAsync();
        //    List<Bank> lstIds = BankBranchDetails.Select(x => x.bank).Distinct().ToList();

        //    foreach (Bank bankBranchDetails in lstIds)
        //    {
        //        BankBranchDetailsLsit.Add(new Bank
        //        {
        //            Id= bankBranchDetails.Id,
        //            bankName=bankBranchDetails.bankName
        //        });
        //    }

        //    return BankBranchDetails;
        //}
        #endregion

        #region LeadsBankInfo
        public async Task<bool> SaveLeadBank(LeadsBankInfo leadsBankInfo)
        {
            if (leadsBankInfo.Id != 0)
                _context.LeadsBankInfos.Update(leadsBankInfo);
            else
                _context.LeadsBankInfos.Add(leadsBankInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeadsBankInfo>> GetLeadsBankInfoByLeadId(int LeadId)
        {
            return await _context.LeadsBankInfos.Where(x => x.leadsId == LeadId).Include(x=>x.crmdepartments).Include(x=>x.crmdesignations).Include(x => x.bankBranchDetails.bank).Include(x => x.bankBranchDetails.bankBranch).Include(x => x.leads).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<LeadsBankInfo>> GetLeadsBankInfo()
        {
            return await _context.LeadsBankInfos.Include(x => x.bankBranchDetails.bank).Include(x => x.bankBranchDetails.bankBranch).Include(x => x.leads).AsNoTracking().ToListAsync();
        }

        public async Task<LeadsBankInfo> GeLeadsBankInfoById(int id)
        {
            return await _context.LeadsBankInfos.Include(x=>x.leads).Include(x => x.bankBranchDetails.bank).Include(x => x.bankBranchDetails.bankBranch).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteLeadsBankInfoById(int id)
        {
            _context.LeadsBankInfos.Remove(_context.LeadsBankInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region BankChargeMaster

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
                .Include(x => x.bankBranchDetails.bank).Include(x => x.bankBranchDetails.bankBranch).Include(x => x.ledgerRelation.ledger).AsNoTracking()
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BankChargeMaster>> GetBankChargeMasterByBankBranchDetailsId(int id)
        {
            try
            {
                return await _context.bankChargeMasters.Where(x => x.bankBranchDetailsId == id)
                .Include(x => x.bankBranchDetails.bank).Include(x => x.bankBranchDetails.bankBranch).Include(x => x.ledgerRelation.ledger).AsNoTracking()
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BankChargeMaster> GetSOF(string bankAccountNo)
        {
            try
            {
                return await _context.bankChargeMasters.Where(x => x.AccountNumber == bankAccountNo).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<FDRCalFormula>> GetFDRCalFormula()
        {
            try
            {
                return await _context.fDRCalFormulas.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
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
        #endregion


    }
}
