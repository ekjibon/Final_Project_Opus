using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings.Interfaces
{
    public interface IAccountGroupService
    {

        #region Account Group

        Task<int> SaveaccountGroup(AccountGroup accountGroup);
        Task<IEnumerable<AccountGroup>> GetaccountGroup();
        Task<IEnumerable<AccountGroup>> GetAccountGroupByNatureId(int? natureId);
        Task<AccountGroup> GetaccountGroupById(int id);
        Task<bool> DeleteaccountGroupById(int id);
        Task<AccountGroup> GetAccountGroupDetailsById(int id);

        #endregion

        #region Balance Sheet Master

        Task<int> SaveBalanceSheetMaster(BalanceSheetMaster balanceSheetMaster);
        Task<IEnumerable<BalanceSheetMaster>> GetBalanceSheetMaster();
        Task<BalanceSheetMaster> GetBalanceSheetMasterById(int Id);
        Task<bool> DeleteBalanceSheetMasterById(int id);

        #endregion
        #region Balance Sheet Notes
        Task<int> SavenoteMaster(NoteMaster noteMaster);
        Task<IEnumerable<NoteMaster>> GetNoteMaster();
        Task<NoteMaster> GetNoteMasterById(int Id);
        Task<IEnumerable<NoteMaster>> GetNoteMasterByMasterId(int Id);
        Task<bool> DeleteNoteMasterById(int id);
        #endregion
        #region Balance Sheet Details

        Task<int> SaveBalanceSheetDetails(BalanceSheetDetails balanceSheetDetails);
        Task<BalanceSheetDetails> GetBalanceSheetDetailsById(int Id);
        Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetails();
        Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetailsByNoteMasterId(int Id);
        Task<IEnumerable<BalanceSheetDetails>> GetBalanceSheetDetailsByBalanceSheetMasterId(int Id);
        Task<bool> DeleteBalanceSheetDetailsById(int id);

        #endregion
    }
}
