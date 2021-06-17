using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.SPModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IBillGenerateService
    {

        #region Bill Generate

        Task<int> SaveBillGenerate(BillGenerate billGenerate);
        Task<IEnumerable<BillGenerate>> GetAllBillGenerate();
        Task<BillGenerate> GetBillGenerateById(int id);
        Task<bool> DeleteBillGenerateById(int id);        
        Task<IEnumerable<BillGenerate>> GetBillGenerateByLeadId(int Id);
        Task<BillGenerateInfoViewModel> GetbillgenerateInfobybillId(int Id);
        Task<IEnumerable<BillGenerateListSPViewModel>> GetAllBillGenerateBySP();
        #endregion

        #region Approved for Cro

        Task<IEnumerable<ApprovedforCro>> GetClientForBillGenerate();
        Task<IEnumerable<BillGenerateSPViewModel>> BillGeneratePendingList(int appCroId);
        Task<BillGenerateSPViewModel> BillGeneratePendingListById(int appCroId);
        Task<ApprovedforCro> GetBillGenerateInfoByClient(int clientId, int ratingYearId);
        Task<ApprovedforCro> GetBillGenerateInfoById(int id);
        Task<IEnumerable<ApprovedforCro>> GetRatingYearForBillGenerate(int clientId);
        Task<IEnumerable<BillGenerate>> GetBillGenerateByCroId(int Id);

        #endregion

        #region Bank Branch Details

        Task<IEnumerable<BankBranchDetails>> GetBankBranchDetails();

        #endregion

        #region Bill Generate History

        Task<bool> SaveBillGenerateHistory(BillGenerateHistory billGenerateHistory);
        Task<IEnumerable<BillGenerateHistory>> GetBillGenerateHistoryByLeadId(int leadId);


        #endregion

        #region Invoice Report
        Task<IEnumerable<BillGenerateReportViewModel>> GetInvoiceReportByInvoiceId(int invoiceId);

        #endregion
    }
}
