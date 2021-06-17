using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IActivityMasterService
    {
        Task<int> SaveActivityMaster(ActivityMaster ActivityMaster);
        Task<IEnumerable<ActivityMaster>> GetAllActivityMaster();
        Task<ActivityMaster> GetActivityMasterById(int id);
        Task<IEnumerable<ActivityMaster>> GetActivityMasterByLeadId(int id);
        Task<bool> DeleteActivityMasterById(int id);
        Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByparentId(int id);
        Task<IEnumerable<ActivityMasterCViewModel>> GetActivityMasterCByLeadId(int id);
        Task<IEnumerable<ActivityMaster>> GetAllActivityMasterbyuser(string aspuser);
        Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByuserparentId(int id, string username);
        Task<IEnumerable<ColdActivityMasterCAViewModel>> GetColdActivityMasterByparentId(int id);
        Task<IEnumerable<ColdActivityMasterCAViewModel>> GetColdActivityMasterByuserparentId(int id, string username);
        Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByleadId(int id);
        Task<IEnumerable<ActivityReportViewModel>> GetActivityReportViewModels(string FromDate, string ToDate, string TaskOwner, int LeadId, string TaskBy);


    }
}
