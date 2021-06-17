using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Notes;
using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Notes.Interfaces
{
    public interface INotesService
    {


        #region Proposal Master

        Task<int> SaveNotes(CRMNoteMaster proposalMaster);
        Task<IEnumerable<CRMNoteMaster>> GetAllCRMNoteMaster();
        Task<CRMNoteMaster> GetCRMNoteMasterById(int id);
        Task<bool> DeleteCRMNoteMasterById(int id);
        Task<IEnumerable<CRMNoteMaster>> GetCRMNoteMasterByLeadId(int leadId);
        #endregion

       

    }
}
