using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.FDR.Interface
{
   public interface IFDRInvestmentService
    {

        #region FDR Invesment Master
        Task<int> SaveFDRInvesmentMaster(FDRInvesmentMaster fDRInvesmentMaster);
        Task<IEnumerable<FDRInvesmentMaster>> GetFDRInvesmentMaster();
        Task<FDRInvesmentMaster> GetFDRInvesmentMasterId(int Id);
        Task<bool> DeleteFDRInvesmentMasterById(int Id);
        #endregion

        #region FDR Investment Detail
        Task<int> SaveFDRInvestmentDetail(FDRInvestmentDetail fDRInvestmentDetail);
        Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetail();
        Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetailByMasterId(int masterId);
        Task<bool> DeleteFDRInvestmentDetailByMasterId(int masterId);
        Task<IEnumerable<FDRInvestmentALLView>> GetFDRInvestmentALLView();
        Task<bool> UpdateFDRInvestmentMasterStatusId(int id, int status);
        Task<bool> UpdateFDRInvestmentDetailsStatusId(int id, int status);
        Task<bool> UpdateFDRInvestmentDetailsStatusIdByMasterId(int fdrMasterId, int status);
        Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetailApproveByMasterId(int Id);
        Task<IEnumerable<FDRInvestmentALLView>> GetFDRInvestmentALLViewForSelf();

        Task<IEnumerable<SP_GET_FDRDetailsViewModel>> GetFdrDetails(int fdrMasterId, int approvalId);

        #endregion

        #region FDR Renewal

        Task<IEnumerable<FDRAutoNoViewModel>> GetFdrAutoNo(string bnkName, DateTime ftDate, string accountNo, string destinationType, string ftType);

        Task<IEnumerable<FDRMaturityStatusForRenewalViewModel>> GetFDRRenewalPendingList(int? bankId, DateTime? frmDate, DateTime? toDate, string userName);

        Task<IEnumerable<FDRMaturityStatusForRenewalViewModel>> GetFDRRenewalList(int? bankId, DateTime? frmDate, DateTime? toDate);

        Task<FDRMaturityStatusForRenewalViewModel> GetFDRInformationForRenewal(int? masterId, int? detailsId, int? isEdit);

        Task<IEnumerable<FDRAutoNoViewModel>> SaveFdrRenew(int FTID, int FDRID, int ParentFTID, int ParentFDRID, string ftno, string ftDate, string openDate, string maturityDate, decimal rate, decimal interestAmt, string tenure, string tenureType, decimal? EncashBC, string UserID, string bankAccNo, string bankBranch, string fdrNo);
        #endregion

        #region FDR Encashment

        Task<int> SaveFDREncashment(FDREncashment fDREncashment);
        Task<IEnumerable<FDREncashment>> GetFDREncashmentByfDRInvestmentDetailId(int fDRInvestmentDetailId);
        Task<bool> DeleteFDREncashmentByfDRInvestmentDetailId(int fDRInvestmentDetailId);
        Task<IEnumerable<GETFDRMaturityStatusViewModel>> GETFDRMaturityStatus(string bankName, string frmDate, string toDate, string userName);
        Task<IEnumerable<GetFDRInformationForEncashmentViewModel>> GetFDRInformationForEncashment(int? fdrID);
        Task<IEnumerable<GetListOfFDREncashmentWithFDRViewModel>> GetListOfFDREncashmentWithFDR(string bankName, string frmDate, string toDate, string userName, int? fDRType);
        Task<IEnumerable<GetListOfFDREncashmentWithFDRViewModel>> GetFDREncashmentInfo(int? fdrID);
        Task<bool> UpdatefDREncashmentApprove(int? Id, int? status, string remarks);
        Task<bool> DeleteFDREncashmentByEncashId(int id);

        #endregion

        #region FDR Report

        Task<IEnumerable<FDRReportViewModel>> ReportFdrFtInstruction(int? fdrMasterId);
        Task<IEnumerable<FDRReportViewModel>> GetFdrReportingHistory(int? bankId, DateTime? frmDate, DateTime? toDate, string sOF);
        Task<IEnumerable<FDRReportViewModel>> ReportCapitalFdrEncashment(int? fdrDetailsId);
        Task<IEnumerable<FDRScheduleReportViewModel>> FdrScheduleReport(string FDate, string TDate);
        Task<IEnumerable<FDRInterestScheduleReportViewModel>> FdrInterestScheduleReport(int? fdrId);
        #endregion
    }
}
