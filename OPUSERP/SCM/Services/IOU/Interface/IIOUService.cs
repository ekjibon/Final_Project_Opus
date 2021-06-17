using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Models;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.IOU.Interface
{
   public interface IIOUService
    {
        #region RFQ Master
        Task<int> SaveRFQMaster(RFQMaster iOUMaster);
        Task<IEnumerable<RFQMaster>> GetRFQMaster();
        Task<RFQMaster> GetRFQMasterbyId(int Id);
        Task<int> SaveRFQReqDetail(RFQReqDetail iOUMaster);
        Task<bool> UpdateRFQMaster(int? id, string updateBy);
        Task<IEnumerable<RFQReqDetail>> GetRFQReqDetail();
        Task<IEnumerable<RFQReqDetail>> GetRFQReqDetailbyreqId(int? reqId);
        Task<int> SaveRFQSupDetail(RFQSupDetail iOUMaster);
        Task<IEnumerable<RFQSupDetail>> GetRFQSupDetails();
        Task<IEnumerable<RFQSupDetail>> GetRFQSupDetailsbyrfqid(int? Id);

        Task<int> SaveRFQPriceMaster(RFQPriceMaster iOUMaster);
        Task<IEnumerable<RFQPriceMaster>> GetRFQPriceMaster();
        Task<RFQPriceMaster> GetRFQPriceMasterbyId(int Id);
        Task<IEnumerable<RFQPriceMaster>> GetRFQPriceMasterbyrfqmasterId(int Id);
        Task<int> SaveRFQReqDetailPrice(RFQReqDetailPrice iOUMaster);
        Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPrice();
        Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPricebyreqId(int? reqId, int? suppid);
        Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPricebyrfqId(int? reqId);
        Task<int> SaveRFQDocumentAttachmentDetail(RFQDocumentAttachmentDetail iOUMaster);
        Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetails();
        Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetailbyrfqid(int? Id, int? suppid);
        Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetailbyrfqmasterid(int? Id);
        Task<bool> DeleteRFQPriceDetailsByMasterId(int id);
        #endregion
        #region IOU Master
        Task<int> SaveIOUMaster(IOUMaster iOUMaster);
        Task<IEnumerable<IOUMaster>> GetIOUMaster(string userName);
        Task<IOUMaster> GetIOUMasterById(int id);
        Task<bool> DeleteIOUMasterById(int id);
        Task<string> GetIOUNo();
        void UpdateIOUMaster(int id, int status);
        void UpdateRFQMasterById(int reqId, int status);
        Task<IEnumerable<IOUMaster>> GetIOUListForApprove(int userId);
        Task<IEnumerable<IOUMaster>> GetIOUMasterByUserName(string userName);
        Task<IEnumerable<IOUMaster>> GetApprovedIOUMasterListByUserName(string userName);
        Task<IEnumerable<IOUMaster>> GetIssuedIOUInformation(string userName);
        Task<IEnumerable<IOUMaster>> GetIOUMasterForPayment();

        #endregion

        #region IOU Details
        Task<int> SaveIOUDetails(IOUDetails iOUDetails);
        Task<IEnumerable<IOUDetails>> GetIOUDetails();
        Task<IOUDetails> GetIOUDetailsById(int id);
        Task<IEnumerable<IOUDetails>> GetIOUDetailsByMasterId(int MasterId);
        Task<bool> DeleteIOUDetailsById(int id);
        Task<bool> DeleteIOUDetailsByMasterId(int id);
        #endregion

        #region IOU Payment
        Task<int> SaveIOUPayment(IOUPayment iOUPayment);
        Task<IEnumerable<IOUPayment>> GetIOUPayment();
        Task<bool> UpdateIOUPaymentForApprove(int Id, decimal? paymentAmount, int? statusId);
        Task<IEnumerable<IOUPayment>> GetIOUPaymentByType(int type);
        Task<IEnumerable<IOUPayment>> GetIOUPaymentByMasterId(int id);
        Task<IOUPayment> GetIOUPaymentById(int id);
        Task<bool> DeleteIOUPaymentById(int id);
        Task<bool> DeleteIOUPaymentByMasterId(int id);
        Task<int> UpdateIOUPayment(IOUPayment iOUPayment);
        Task<IEnumerable<IOUPaymentStatusVM>> GetIOUPaymentStatus(int projectId, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<IOUPayment>> GetIOUPaymentByiOUPaymentMasterId(int iOUPaymentMasterId);
        Task<IEnumerable<IOUPaymentStatusVM>> GetIOUPaymentStatusEmployee(int attentionToId, DateTime? fromDate, DateTime? toDate);
        #endregion

        #region IOU Payment Master
        Task<int> SaveIOUPaymentMaster(IOUPaymentMaster iOUPaymentMaster);
        Task<IEnumerable<IOUPaymentMaster>> GetAllIOUPaymentMaster();
        Task<IEnumerable<IOUPaymentMaster>> GetIOUPaymentMasterListForApprove(int userId);
        Task<IOUPaymentMaster> GetIOUPaymentMasterById(int id);
        Task<bool> DeleteIOUPaymentMasterById(int id);
        Task<string> GetIOUPaymentNo();
        Task<string> GetIOUOwner(int id);
        void UpdateIOUPaymentMaster(int id, int status);
        Task<bool> UpdateIOUPaymentForReceivedPayment(int Id, decimal? paymentAmount, int? receivebyId, DateTime? receiveDate, string paymentMode, string chequeNo, string bankName);
        #endregion

        //AutoNumberViewModel GetPuerchaseOrderNo(int reqId);

        Task<IEnumerable<IOUMaster>> GetIOUMasterByUserNameNDateTime(string userName, string formDate, string toDate);
        Task<IEnumerable<IOUPaymentMaster>> GetIssuedIOUPaymentMasterByUserName(string userName);
        Task<IEnumerable<IOUPaymentMaster>> GetIOUPaymentMasterByUserName(string userName);
        Task<IEnumerable<RFQApprovedListViewModel>> GetRFQApprovedListViewModels(int userId);
    }
}
