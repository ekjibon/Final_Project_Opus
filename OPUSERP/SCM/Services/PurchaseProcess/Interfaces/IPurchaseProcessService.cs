using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseProcess.Interfaces
{
    public interface IPurchaseProcessService
    {
        #region  CS Master

        Task<int> SaveCSMaster(CSMaster cSMaster);
        void UpdateCSMaster(int id, int status);
        Task<IEnumerable<CSMaster>> GetCSMasterList(int userId);
        Task<IEnumerable<CSMaster>> GetCSMasterListForPO(int userId);
        Task<IEnumerable<CSMaster>> GetCSMasterByReqId(int reqId);
        Task<CSMaster> GetCSMasterById(int id);
        Task<bool> DeleteCSMasterById(int id);
        Task<string> GetCSNumber();

        Task<IEnumerable<CSMaster>> GetCSListForApprove(int userId);
        

        #endregion

        #region  CS Details
        Task<int> SaveCSDetailsSingle(CSDetail cSDetail);
        void SaveCSDetails(List<CSDetail> cSDetails);

        Task<bool> UpdateCSDetailApproval(int? Id, decimal? qty, decimal? rate);


        Task<IEnumerable<CSDetail>> GetCSDetailListByMasterId(int masterId);
        Task<IEnumerable<CSItemVM>> GetCSDetailListBySupplierId(int supplierId);
        Task<IEnumerable<CSItemVM>> GetCSDetailListByCSAndSupplierId(int csMasterId,int supplierId);
        Task<IEnumerable<CSSupplierVM>> GetCSSupplierListBycsId(int csId);

        Task<IEnumerable<QuotationDetailsVM>> GetQuotationDetailView(int csMasterId);
        Task<IEnumerable<CSDetailsVM>> GetCSDetailView(int csMasterId);

        //Task<int> SaveCSDetails(CSDetail cSDetail);
        //void UpdateCSDetails(int id, int status);
        //Task<IEnumerable<CSDetail>> GetCSDetailList(int userId);
        //Task<IEnumerable<CSDetail>> GetCSDetailsByCSId(int csId);
        //Task<CSDetail> GetCSDetailsById(int id);
        //Task<bool> DeleteCSDetailsById(int id);
        //Task<bool> DeleteCSDetailsByCSId(int id);

        #endregion

        #region PR WITH CS
        Task<IEnumerable<RequisitionForCSViewModel>> GetRequisitionListForBuyer(int userId);
        Task<IEnumerable<GetSupplierWiseItemDetailsViewModel>> GetSupplierWiseItemDetails(int reqDetailID);
        #endregion
    }
}
