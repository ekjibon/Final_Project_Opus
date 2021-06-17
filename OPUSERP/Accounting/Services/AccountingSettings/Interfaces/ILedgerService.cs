using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings.Interfaces
{
    public interface ILedgerService
    {
        Task<int> Saveledger(Ledger ledger);
        Task<IEnumerable<Ledger>> GetLedger();
        Task<IEnumerable<Ledger>> GetLedgerBySbuId(int? sbuId);
        Task<IEnumerable<Ledger>> GetLedgerCountByGorupId(int grpId);
        Task<IEnumerable<LedgerRelation>> GetLedgerForPayment(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetOnlyCashLedger(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetOnlyBankLedger(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetLedgerForParticular(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetAllLedgerForJournal(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetLedgerAllBySbuId(int sbuId);
        Task<int> GetLedgerCheckL(int id, string name, int sbuId);
        Task<Ledger> GetledgerById(int id);
        Task<IEnumerable<LedgerRelation>> GetSubledgerByLedgerId(int ledgerId);
        Task<AutoNumberModel> GetAutoNumber(int accountId, int voucherTypeId, string voucherDate);
        Task<bool> DeleteledgerById(int id);
        Task<IEnumerable<LedgerRelation>> GetledgerRelationByLedgerRelationId(int ledgerId);
        Task<IEnumerable<LedgerRelation>> GetledgerRelationByLedgerId(int ledgerId);
        Task<LedgerRelation> GetledgerRelationById(int ledgerId);
        Task<Company> Company(int id);
        Task<bool> SaveLedgerRelation(LedgerRelation ledgerRelation);
        Task<int> GetLedgerCheck(int id, string name, int sbuId);
        Task<Ledger> GetLedgerCodeByGorupId(int grpId);
        Task<IEnumerable<Ledger>> GetLedgerWithSubLedger();
        Task<IEnumerable<Ledger>> GetLedgerWithSubLedgerBySbu(int sbuId);
        Task<IEnumerable<Ledger>> GetLedgerWithoutSubLedger();
        Task<Ledger> GetLedgerWithBigSubLedger();
        Task<IEnumerable<Ledger>> GetSubLedger();
        Task<IEnumerable<Ledger>> GetSubLedgerBySbuId(int? sbuId);
        Task<IEnumerable<Ledger>> GetAllSubLedger();
        Task<IEnumerable<Ledger>> GetAllSubLedgerBySbu(int sbuId);
        Task<IEnumerable<LedgerRelation>> GetSubledgerR();
        Task<IEnumerable<LedgerRelation>> GetLedgerRelationBySubLedgerId(int subLedgerId);
        Task<IEnumerable<LedgerRelation>> GetLedgerForVoucherSettingBank();
        Task<IEnumerable<LedgerRelation>> GetLedgerForVoucherSettingCash();
        Task<int> GetLedgerCheckbyName(string name);
        Task<Ledger> GetLedgerWithBigSubLedgerSupplier();
        Task<Ledger> GetLedgerWithBigSubLedgerCustomer();
        Task<LedgerRelation> GetledgerRelationByLedgerSubledgerId(int ledgerId, int subledgerId);
        Task<LedgerRelation> GetledgerRelationByLedgerwosId(int ledgerId);
        Task<IEnumerable<LedgerRelation>> GetLedgerForFdr();
        Task<IEnumerable<LedgerRelation>> GetSubledgerByLedgerBrId(int ledgerId, int braId);
        Task<Ledger> GetLedgerDetailsById(int id);
        Task<IEnumerable<LedgerRelation>> GetLedgerForOpeningBalance();
        Task<IEnumerable<Ledger>> GetAllSubLedgerbyledger(int id);
        Task<bool> DeleteledgerRelationByLedgerId(int ledgerId);
        Task<bool> DeleteledgerRelationBySubLedgerId(int subLedgerId);
        Task<IEnumerable<Ledger>> GetLedgerLedgerBySbu(int sbuId);
    }
}
