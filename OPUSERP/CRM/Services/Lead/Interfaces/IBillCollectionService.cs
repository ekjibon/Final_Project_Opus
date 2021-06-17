using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Models.Dashboard;
using OPUSERP.VMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IBillCollectionService
    {

        #region Billcollection

        Task<int> SaveBillCollection(BillCollection billCollection);
        Task<IEnumerable<BillCollection>> GetBillCollection();
        Task<IEnumerable<BillCollection>> GetBillCollectionbyLeadId(int LeadId);
        Task<BillCollection> getBillCollectionById(int Id);
        Task<IEnumerable<BillCollection>> GetBillCollectionbybillId(int LeadId);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteBybillId(int id);
        Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingList();
        Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListfilter(string ownername,string TeamLeader, string Fa, string BD, string LeadId);
        Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListDatefilter(string ownername, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<BillCollectionSPViewModel>> GetBillCollectionPendingListFC();

        #endregion

        #region BillCollectionHistory
        Task<int> SaveBillCollectionHistory(BillCollectionHistory billCollection);
        Task<IEnumerable<BillCollectionHistory>> GetBillCollectionHistory();
        Task<IEnumerable<BillCollectionHistory>> GetBillCollectionHistorybyCollectionId(int LeadId);
        #endregion

        #region Dashboard

        Task<IEnumerable<ChartViewModel>> GetCollectionDashboardByDateArea(DateTime? frmDate, DateTime? toDate, int? areaId);

        #endregion
    }
}
