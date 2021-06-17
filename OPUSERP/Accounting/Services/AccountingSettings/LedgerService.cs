using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings
{
    public class LedgerService : ILedgerService
    {
        private readonly ERPDbContext _context;

        public LedgerService(ERPDbContext context)
        {
            _context = context;
        }
        #region Ledger
        public async Task<int> Saveledger(Ledger ledger)
        {
            try
            {
                if (ledger.Id != 0)
                {
                    _context.Ledgers.Update(ledger);
                }
                else
                {
                    _context.Ledgers.Add(ledger);
                }

                await _context.SaveChangesAsync();
                return ledger.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Ledger>> GetLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 0 || x.haveSubLedger == 1).Include(x => x.group.nature).Include(x => x.specialBranchUnit).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerBySbuId(int? sbuId)
        {
            if (sbuId == 0)
            {
                return await _context.Ledgers.Where(x => x.haveSubLedger == 0 || x.haveSubLedger == 1).Include(x => x.group.nature).Include(x => x.specialBranchUnit).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Ledgers.Where(x => x.specialBranchUnitId == sbuId && ( x.haveSubLedger == 0 || x.haveSubLedger == 1)).Include(x => x.group.nature).Include(x => x.specialBranchUnit).AsNoTracking().ToListAsync();
            }
            
        }

        public async Task<Ledger> GetLedgerDetailsById(int id)
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 0 || x.haveSubLedger == 1).Include(x => x.group).Where(x=>x.Id== id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Ledger>> GetAllSubLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2).Include(x => x.group).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetAllSubLedgerBySbu(int sbuId)
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.specialBranchUnitId == sbuId).Include(x => x.group).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetAllSubLedgerbyledger( int id)
        {
            List<int?> ids = _context.LedgerRelations.Where(x => x.ledgerId == id && x.subLedgerId!=null).Select(x => x.subLedgerId).ToList();
            return await _context.Ledgers.Where(x => ids.Contains(x.Id)&& x.haveSubLedger == 2).Include(x => x.group).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerWithSubLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 1).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerLedgerBySbu(int sbuId)
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger != 2 && x.specialBranchUnitId == sbuId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerWithSubLedgerBySbu(int sbuId)
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 1 && x.specialBranchUnitId==sbuId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerWithoutSubLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger != 2).Include(x=>x.group).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetLedgerCountByGorupId(int grpId)
        {
            return await _context.Ledgers.Where(x => x.groupId == grpId).OrderByDescending(x => x.accountCode).Include(x => x.group).AsNoTracking().ToListAsync();
        }

        public async Task<Ledger> GetLedgerCodeByGorupId(int grpId)
        {
            return await _context.Ledgers.Where(x => x.groupId == grpId).OrderByDescending(x => x.accountCode).FirstOrDefaultAsync();
        }

        public async Task<Ledger> GetLedgerWithBigSubLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.accountCode.StartsWith('S')).OrderByDescending(x => x.accountCode).FirstOrDefaultAsync();
        }
        public async Task<Ledger> GetLedgerWithBigSubLedgerSupplier()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.accountCode.StartsWith('V')).OrderByDescending(x => x.accountCode).FirstOrDefaultAsync();
        }
        public async Task<Ledger> GetLedgerWithBigSubLedgerCustomer()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.accountCode.StartsWith('C')).OrderByDescending(x => x.accountCode).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ledger>> GetSubLedger()
        {
            return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.accountCode.StartsWith('S')).Include(x => x.specialBranchUnit).OrderByDescending(x => x.accountCode).ToListAsync();
        }

        public async Task<IEnumerable<Ledger>> GetSubLedgerBySbuId(int? sbuId)
        {
            if (sbuId == 0)
            {
                return await _context.Ledgers.Where(x => x.haveSubLedger == 2 && x.accountCode.StartsWith('S')).Include(x => x.specialBranchUnit).OrderByDescending(x => x.accountCode).ToListAsync();
            }
            else
            {
                return await _context.Ledgers.Where(x => x.specialBranchUnitId==sbuId && x.haveSubLedger == 2 && x.accountCode.StartsWith('S')).Include(x => x.specialBranchUnit).OrderByDescending(x => x.accountCode).ToListAsync();
            }
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerRelationBySubLedgerId(int subLedgerId)
        {
            return await _context.LedgerRelations.Where(x => x.subLedgerId == subLedgerId).Include(x => x.ledger).ToListAsync();
        }

        public async Task<int> GetLedgerCheck(int id, string name,int sbuId)
        {
            var Result = await _context.Ledgers.Where(x => x.accountName == name && x.specialBranchUnitId == sbuId && x.Id != id ).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> GetLedgerCheckL(int id, string name, int sbuId)
        {
            var Result = await _context.Ledgers.Where(x => x.accountName == name && x.specialBranchUnitId == sbuId && x.Id != id && x.haveSubLedger!=2).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> GetLedgerCheckbyName(string name)
        {
            var Result = await _context.Ledgers.Where(x => x.accountName == name).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerForPayment(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && (L.ledgerType == "Cash" || L.ledgerType == "Bank") && L.specialBranchUnitId == sbuId //&& AG.isCashBank=="Yes" 
                                 group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    natureId= g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetOnlyCashLedger(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && (L.ledgerType == "Cash") && L.specialBranchUnitId == sbuId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetOnlyBankLedger(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && (L.ledgerType == "Bank") && L.specialBranchUnitId == sbuId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerForVoucherSettingBank()
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && AG.VGrpTypeCode == 1 && EF.Functions.Like(L.accountName, "%bank%")
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LedgerRelation>> GetLedgerForVoucherSettingCash()
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && AG.VGrpTypeCode == 1 && EF.Functions.Like(L.accountName, "%cash%")
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerForParticular(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && (L.ledgerType != "Cash" && L.ledgerType != "Bank") && L.specialBranchUnitId == sbuId//&& AG.isCashBank == "No" 
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, L.haveSubLedger,AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    haveSubLedger = g.Key.haveSubLedger,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetAllLedgerForJournal(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && L.specialBranchUnitId == sbuId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, L.haveSubLedger, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    haveSubLedger = g.Key.haveSubLedger,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerAllBySbuId(int sbuId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 && L.specialBranchUnitId == sbuId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, L.haveSubLedger, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    haveSubLedger = g.Key.haveSubLedger,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetLedgerForOpeningBalance()
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id
                                join AG in _context.AccountGroups on L.groupId equals AG.Id
                                where LR.subLedgerId.GetValueOrDefault(0) == 0 
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName, L.haveSubLedger, AG.natureId } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName,
                                    haveSubLedger = g.Key.haveSubLedger,
                                    natureId = g.Key.natureId
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        //LR.subLedgerId.GetValueOrDefault(0)


        public async Task<IEnumerable<LedgerRelation>> GetSubledgerByLedgerId(int ledgerId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.subLedgerId equals L.Id
                                where LR.subLedgerId.GetValueOrDefault(0) != 0 && L.haveSubLedger == 2 && LR.ledgerId == ledgerId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName
                                }).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<LedgerRelation>> GetSubledgerByLedgerBrId(int ledgerId,int braId)
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.subLedgerId equals L.Id
                                where LR.subLedgerId.GetValueOrDefault(0) != 0 && L.haveSubLedger == 2 && LR.ledgerId == ledgerId&&L.specialBranchUnitId==braId
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,
                                    ledgerId = g.Key.ledgerId,
                                    subLedgerId = g.Key.subLedgerId,
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName
                                }).AsNoTracking().ToListAsync();
            return result;
        }


        public async Task<Ledger> GetledgerById(int id)
        {
            try
            {
                var record = await _context.Ledgers.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public async Task<IEnumerable<LedgerRelation>> GetSubledgerR()
        {
            var data= await _context.LedgerRelations
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<IEnumerable<LedgerRelation>> GetledgerRelationByLedgerRelationId(int ledgerId)
        {
            return await _context.LedgerRelations
                .Where(x => x.Id == ledgerId)
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().ToListAsync();
        }

        public async Task<LedgerRelation> GetledgerRelationById(int ledgerId)
        {
            return await _context.LedgerRelations
                .Where(x => x.Id == ledgerId)
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<LedgerRelation>> GetledgerRelationByLedgerId(int ledgerId)
        {
            return await _context.LedgerRelations
                .Where(x => x.ledgerId == ledgerId)
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().ToListAsync();
        }
        public async Task<LedgerRelation> GetledgerRelationByLedgerwosId(int ledgerId)
        {
            return await _context.LedgerRelations
                .Where(x => x.ledgerId == ledgerId && x.subLedgerId == null)
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<LedgerRelation> GetledgerRelationByLedgerSubledgerId(int ledgerId, int subledgerId)
        {
            return await _context.LedgerRelations
                .Where(x => x.ledgerId == ledgerId && x.subLedgerId == subledgerId)
                .Include(x => x.ledger)
                .Include(x => x.subLedger)
                .AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<AutoNumberModel> GetAutoNumber(int accountId, int voucherTypeId, string voucherDate)
        {
            var result = await _context.AutoNumberModels.FromSql($"SP_Get_VoucherNo {accountId},{voucherTypeId},{voucherDate}").FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteledgerById(int id)
        {
            _context.Ledgers.Remove(_context.Ledgers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }



        public async Task<bool> SaveLedgerRelation(LedgerRelation ledgerRelation)
        {
            if (ledgerRelation.Id != 0)
                _context.LedgerRelations.Update(ledgerRelation);
            else
                _context.LedgerRelations.Add(ledgerRelation);
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<LedgerRelation>> GetLedgerForFdr()
        {
            var result = await (from LR in _context.LedgerRelations
                                join L in _context.Ledgers on LR.ledgerId equals L.Id                                
                                where EF.Functions.Like(L.accountName, "%bank%")
                                group new { LR, L } by new { LR.Id, LR.ledgerId, LR.subLedgerId, L.accountCode, L.accountName } into g
                                select new LedgerRelation
                                {
                                    Id = g.Key.Id,                                    
                                    accountCode = g.Key.accountCode,
                                    accountName = g.Key.accountName
                                }).OrderBy(x => x.accountName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<bool> DeleteledgerRelationByLedgerId(int ledgerId)
        {
            _context.LedgerRelations.RemoveRange(_context.LedgerRelations.Where(a => a.ledgerId == ledgerId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteledgerRelationBySubLedgerId(int subLedgerId)
        {
            _context.LedgerRelations.RemoveRange(_context.LedgerRelations.Where(a => a.subLedgerId == subLedgerId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region company
        public async Task<Company> Company(int id)
        {
            try
            {
                var record = await _context.Companies.FindAsync(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion
    }
}
