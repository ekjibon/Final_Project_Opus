using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.SPModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface ILeadsService
    {
        #region Lead

        Task<int> SaveLeads(Leads Leads);
        Task<bool> UpdateLeads(int? id, int? leadprogressstatus, string reamrks, string updateBy);
        Task<IEnumerable<Leads>> GetAllLeads();
        Task<Leads> GetLeadsById(int id);
        Task<bool> DeleteLeadsById(int id);

        Task<IEnumerable<GetLeadInfoListViewModel>> GetLeadInfoAsQueryAble(string ownername);
        Task<IEnumerable<AllLeadJson>> GetLeadInfoAsQueryAblebyOwner(string LeadOwner);
        Task<IEnumerable<Leads>> GetLeadsByLeadOwner(string LeadOwner);
        Task<IEnumerable<Leads>> GetAllLeadsByuser(string user);
        Task<IEnumerable<Leads>> GetLeadslistById(int id);
        Task<LeadAutoNumber> GetLeadAutoNumberBySp();
        Task<Leads> GetTotalLead();
        Task<IEnumerable<GetLeadsForConversionListViewModel>> GetLeadsForConversion(string ownername);
        Task<IEnumerable<GetLeadInfoVerificationListViewModel>> GetLeadInfoVerification(string ownername);
        Task<IEnumerable<GetLeadInfoPrintListViewModel>> GetLeadInfoPrint(string ownername);
        Task<Leads> GetLeadDetailsById(int Id);
        Task<IEnumerable<Leads>> GetLeadsForCollection();
        Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeOwnerUserOneToOne(string previousOwner, string previousOwnerUser, string newOwner, string newOwnerUser, string user);
        Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeOwnerSingleUser(int? leadsId, string previousOwner, string previousOwnerUser, string newOwner, string newOwnerUser, string user);
        Task<IEnumerable<System.Object>> GetLeadContactAddress(int leadId);
        Task<IEnumerable<System.Object>> GetLeadPatientInfoByLeadId(int leadId);
        Task<string> GetLeadNameAndCodeById(int? Id);
        Task<IEnumerable<Leads>> GetLeadInfoByfil(string org);
        Task<IEnumerable<AllClientViewModel>> GetAllLeadsByuserSP(string ownername);
        Task<IEnumerable<AllClientViewModel>> GetAllLeadsByNameSP(string ownername);
        Task<IEnumerable<AllClientViewModel>> GetAllLeadsListSP();
        Task<IEnumerable<AllClientCViewModel>> GetAllLeadsCByuserSP(string ownername);
        Task<IEnumerable<AllClientCViewModel>> GetAllLeadsCByNameSP(string ownername);
        Task<IEnumerable<AllClientCViewModel>> GetAllLeadCsListSP();
        Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeActivity(int? leadsId, int? isactive, string user);
        Task<IEnumerable<GetLeadInfoListViewModel>> GetLeadInfofilterAsQueryAble(string ownername, string bank, string branch, string BD, string leadId);
        Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySPfilter(string BD, string LeadId);

        #endregion

        #region Lead History

        Task<bool> SaveLeadsHistory(LeadsHistory leadsHistory);
        Task<IEnumerable<LeadsHistory>> GetLeadsHistoryByLeadId(int leadId);
        Task<IEnumerable<LeadsHistory>> GetAllLeadsHistory();
        Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySP();
        Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySurSP();
        Task<IEnumerable<ChangeOwnerLogShowViewModel>> GetchangeOwnerLogShowViewModelsSP();

        #endregion

        #region Lead Report

        Task<IEnumerable<LeadReportViewModel>> GetLeadReport(int? sectorId, int? fitypeId, int? sourceId, string leadOwner);

        #endregion
    }
}
