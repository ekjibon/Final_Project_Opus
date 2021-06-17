using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IBankBranchService
    {
        #region FIType
        Task<bool> SaveFIType(FIType fIType);
        Task<IEnumerable<FIType>> GetAllFIType();
        Task<FIType> GeFITypeById(int id);
        Task<bool> DeleteFITypeById(int id);
        #endregion

        Task<bool> SaveBank(Bank bank);
        Task<IEnumerable<Bank>> GetAllBank();
        Task<Bank> GeBankById(int id);
        Task<IEnumerable<Bank>> GetAllBankbyFIId(int Id);
        Task<bool> DeleteBankById(int id);

        //Bank Branch
        Task<bool> SaveBankBranch(BankBranch bankBranch);
        Task<IEnumerable<BankBranch>> GetAllBankBranch();
        Task<BankBranch> GeBankBranchById(int id);
        Task<bool> DeleteBankBranchById(int id);
        Task<IEnumerable<LeadsBankInfo>> GetLeadsBankInfo();

        //Bank Branch Details
        Task<bool> SaveBankBranchDetails(BankBranchDetails bankBranchDetails);
        Task<IEnumerable<BankBranchDetails>> GetAllBankBranchDetails();
        Task<IEnumerable<BankBranchDetails>> GetBranchByBankId(int bankId);

        //LeadsBankInfo
        Task<bool> SaveLeadBank(LeadsBankInfo leadsBankInfo);
        Task<IEnumerable<LeadsBankInfo>> GetLeadsBankInfoByLeadId(int LeadId);
        Task<LeadsBankInfo> GeLeadsBankInfoById(int id);
        Task<bool> DeleteLeadsBankInfoById(int id);

        Task<int> SaveBankChargeMaster(BankChargeMaster bankChargeMaster);
        Task<IEnumerable<BankChargeMaster>> GetBankChargeMaster();
        Task<BankChargeMaster> GetBankChargeMasterById(int id);
        Task<bool> DeleteBankChargeMasterById(int id);
        Task<IEnumerable<BankChargeMaster>> GetBankChargeMasterByBankBranchDetailsId(int id);

        Task<BankChargeMaster> GetSOF(string bankAccountNo);

        Task<IEnumerable<FDRCalFormula>> GetFDRCalFormula();
    }
}
