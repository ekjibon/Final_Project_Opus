using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Inventory;

namespace OPUSERP.VMS.Services.Inventory.interfaces
{
    public interface IPartsIssueService
    {
        #region Parts Issue Master
        Task<int> SavePartsIssueMaster(PartsIssueMaster partsIssue);
        Task<IEnumerable<PartsIssueMaster>> GetAllPartsIssueMaster();
        Task<PartsIssueMaster> GetPartsIssueMasterById(int id);
        IQueryable<PartsIssueMaster> GePartsIssueMasterByPartsId(int partsId);
        Task<bool> DeletePartsIssueMasterById(int id);
        #endregion

        #region Parts Issue Details

        Task<int> SavePartsIssueDetails(PartsIssueDetails issueDetails);
        Task<IEnumerable<PartsIssueDetails>> GetAllPartsIssueDetails();
        Task<PartsIssueDetails> GetPartsIssueDetailsById(int id);
        IQueryable<PartsIssueDetails> GetPartsIssueDetailsByMasterId(int partsId);
        Task<bool> DeletePartsIssueDetailsByMasterId(int masterId);

        #endregion
    }
}
