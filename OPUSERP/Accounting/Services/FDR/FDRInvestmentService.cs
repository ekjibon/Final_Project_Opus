using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Accounting.Models;

namespace OPUSERP.Accounting.Services.FDR
{
    public class FDRInvestmentService : IFDRInvestmentService
    {
        private readonly ERPDbContext _context;

        public FDRInvestmentService(ERPDbContext context)
        {
            _context = context;
        }

        #region FDR Invesment Master

        public async Task<int> SaveFDRInvesmentMaster(FDRInvesmentMaster fDRInvesmentMaster)
        {
            try
            {
                if (fDRInvesmentMaster.Id != 0)
                {
                    _context.fDRInvesmentMasters.Update(fDRInvesmentMaster);
                }
                else
                {
                    _context.fDRInvesmentMasters.Add(fDRInvesmentMaster);
                }

                await _context.SaveChangesAsync();
                return fDRInvesmentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<FDRInvesmentMaster>> GetFDRInvesmentMaster()
        {
            return await _context.fDRInvesmentMasters.OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRInvestmentALLView>> GetFDRInvestmentALLView()
        {
            //var master = await _context.fDRInvesmentMasters.Where(x => x.FDRStatus == 0 || x.FDRStatus == 2 || x.FDRStatus == 6).Include(x => x.bankChargeMaster.bankBranchDetails.bank).Include(x => x.bankChargeMaster.bankBranchDetails.bankBranch).AsNoTracking().ToListAsync();
            var master = await _context.fDRInvesmentMasters.Include(x => x.bank).Where(x => x.FDRStatus == 0).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
            List<FDRInvestmentALLView> fDRInvestmentALLViews = new List<FDRInvestmentALLView>();
            foreach (FDRInvesmentMaster fDRInvesmentMaster in master)
            {
                fDRInvestmentALLViews.Add(new FDRInvestmentALLView
                {
                    fDRInvesmentMaster = fDRInvesmentMaster,
                    fDRInvestmentDetails = await _context.fDRInvestmentDetails.Include(x => x.bank).Where(x => x.fDRInvesmentMasterId == fDRInvesmentMaster.Id && x.ApprovalStatus == 0).OrderByDescending(a => a.Id).ToListAsync()
                });
            }
            return fDRInvestmentALLViews;
        }

        public async Task<IEnumerable<FDRInvestmentALLView>> GetFDRInvestmentALLViewForSelf()
        {
            var master = await _context.fDRInvesmentMasters.Include(x => x.bank).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
            List<FDRInvestmentALLView> fDRInvestmentALLViews = new List<FDRInvestmentALLView>();
            foreach (FDRInvesmentMaster fDRInvesmentMaster in master)
            {
                fDRInvestmentALLViews.Add(new FDRInvestmentALLView
                {
                    fDRInvesmentMaster = fDRInvesmentMaster,
                    //fDRInvestmentDetails = await _context.fDRInvestmentDetails.Where(x => x.fDRInvesmentMasterId == fDRInvesmentMaster.Id && x.ApprovalStatus == 0).Include(x => x.bank).ToListAsync()
                    fDRInvestmentDetails = await _context.fDRInvestmentDetails.Where(x => x.fDRInvesmentMasterId == fDRInvesmentMaster.Id).Include(x => x.bank).OrderByDescending(a=>a.Id).ToListAsync()
                });
            }
            return fDRInvestmentALLViews;
        }

        public async Task<FDRInvesmentMaster> GetFDRInvesmentMasterId(int Id)
        {
            return await _context.fDRInvesmentMasters.Where(x => x.Id == Id).OrderByDescending(a => a.Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteFDRInvesmentMasterById(int Id)
        {
            _context.fDRInvesmentMasters.RemoveRange(_context.fDRInvesmentMasters.Where(x => x.Id == Id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region FDR Investment Detail

        public async Task<int> SaveFDRInvestmentDetail(FDRInvestmentDetail fDRInvestmentDetail)
        {
            try
            {
                if (fDRInvestmentDetail.Id != 0)
                {
                    _context.fDRInvestmentDetails.Update(fDRInvestmentDetail);
                }
                else
                {
                    _context.fDRInvestmentDetails.Add(fDRInvestmentDetail);
                }

                await _context.SaveChangesAsync();
                return fDRInvestmentDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetail()
        {
            return await _context.fDRInvestmentDetails.OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetailByMasterId(int Id)
        {
            return await _context.fDRInvestmentDetails.Include(x => x.bank).Include(x => x.ReceiveLinkLedger).Include(x => x.LinkledgerRelation).Where(x => x.fDRInvesmentMasterId == Id && x.ApprovalStatus == 0).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRInvestmentDetail>> GetFDRInvestmentDetailApproveByMasterId(int Id)
        {
            return await _context.fDRInvestmentDetails.Include(x => x.bank).Include(x => x.ReceiveLinkLedger).Include(x => x.LinkledgerRelation).Where(x => x.fDRInvesmentMasterId == Id && x.ApprovalStatus == 1).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteFDRInvestmentDetailByMasterId(int id)
        {
            _context.fDRInvestmentDetails.RemoveRange(_context.fDRInvestmentDetails.Where(x => x.fDRInvesmentMasterId == id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateFDRInvestmentMasterStatusId(int id, int status)
        {
            FDRInvesmentMaster master = await _context.fDRInvesmentMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            master.FDRStatus = status;
            _context.fDRInvesmentMasters.Update(master);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateFDRInvestmentDetailsStatusId(int id, int status)
        {
            FDRInvestmentDetail master = await _context.fDRInvestmentDetails.Where(x => x.Id == id).FirstOrDefaultAsync();
            master.ApprovalStatus = status;
            _context.fDRInvestmentDetails.Update(master);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateFDRInvestmentDetailsStatusIdByMasterId(int fdrMasterId, int status)
        {
            FDRInvestmentDetail master = await _context.fDRInvestmentDetails.Where(x => x.fDRInvesmentMasterId == fdrMasterId).FirstOrDefaultAsync();
            master.ApprovalStatus = status;
            _context.fDRInvestmentDetails.Update(master);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SP_GET_FDRDetailsViewModel>> GetFdrDetails(int fdrMasterId, int approvalId)
        {
            return await _context.sP_GET_FDRDetailsViewModels.FromSql($"SP_GET_FDRDetails {fdrMasterId}, {approvalId}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region FDR Renewal

        public async Task<IEnumerable<FDRAutoNoViewModel>> GetFdrAutoNo(string bnkName, DateTime ftDate, string accountNo, string destinationType, string ftType)
        {
            return await _context.fDRAutoNoViewModels.FromSql($"SP_GetFTNO {bnkName}, {ftDate}, {accountNo}, {destinationType}, {ftType}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRMaturityStatusForRenewalViewModel>> GetFDRRenewalPendingList(int? bankId, DateTime? frmDate, DateTime? toDate, string userName)
        {
            return await _context.fDRMaturityStatusForRenewalViewModels.FromSql($"SP_GETFDRMaturityStatusForRenewal {bankId}, {frmDate}, {toDate}, {userName}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRMaturityStatusForRenewalViewModel>> GetFDRRenewalList(int? bankId, DateTime? frmDate, DateTime? toDate)
        {
            return await _context.fDRMaturityStatusForRenewalViewModels.FromSql($"SP_GETFDRRenewalList {bankId}, {frmDate}, {toDate}").AsNoTracking().ToListAsync();
        }

        public async Task<FDRMaturityStatusForRenewalViewModel> GetFDRInformationForRenewal(int? masterId, int? detailsId, int? isEdit)
        {
            return await _context.fDRMaturityStatusForRenewalViewModels.FromSql($"SP_Get_FDR_Information_For_Renewal {masterId}, {detailsId}, {isEdit}").FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FDRAutoNoViewModel>> SaveFdrRenew(int FTID, int FDRID, int ParentFTID, int ParentFDRID, string ftno, string ftDate, string openDate, string maturityDate, decimal rate, decimal interestAmt, string tenure, string tenureType, decimal? EncashBC, string UserID, string bankAccNo, string bankBranch, string fdrNo)
        {
            return await _context.fDRAutoNoViewModels.FromSql($"SP_Save_FDR_Renew {FTID}, {FDRID}, {ParentFTID}, {ParentFDRID}, {ftno}, {ftDate}, {openDate}, {maturityDate}, {rate}, {interestAmt}, {tenure}, {tenureType}, {EncashBC}, {UserID}, {bankAccNo}, {bankBranch}, {fdrNo}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region FDR Encashment

        public async Task<int> SaveFDREncashment(FDREncashment fDREncashment)
        {
            try
            {
                if (fDREncashment.Id != 0)
                {
                    _context.fDREncashments.Update(fDREncashment);
                }
                else
                {
                    _context.fDREncashments.Add(fDREncashment);
                }

                await _context.SaveChangesAsync();
                return fDREncashment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<FDREncashment>> GetFDREncashmentByfDRInvestmentDetailId(int fDRInvestmentDetailId)
        {
            return await _context.fDREncashments.Include(x => x.fDRInvestmentDetail).Where(x => x.fDRInvestmentDetailId == fDRInvestmentDetailId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteFDREncashmentByfDRInvestmentDetailId(int fDRInvestmentDetailId)
        {
            _context.fDREncashments.RemoveRange(_context.fDREncashments.Where(x => x.fDRInvestmentDetailId == fDRInvestmentDetailId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<GETFDRMaturityStatusViewModel>> GETFDRMaturityStatus(string bankName, string frmDate, string toDate, string userName)
        {
            return await _context.gETFDRMaturityStatusViewModels.FromSql($"SP_GETFDRMaturityStatus {bankName}, {frmDate}, {toDate}, {userName}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetListOfFDREncashmentWithFDRViewModel>> GetListOfFDREncashmentWithFDR(string bankName, string frmDate, string toDate, string userName, int? fDRType)
        {
            return await _context.getListOfFDREncashmentWithFDRViewModels.FromSql($"SP_GetListOfFDREncashmentWithFDR {bankName}, {frmDate}, {toDate}, {userName}, {fDRType}").AsNoTracking().ToListAsync();
        }
        
        public async Task<IEnumerable<GetFDRInformationForEncashmentViewModel>> GetFDRInformationForEncashment(int? fdrID)
        {
            return await _context.getFDRInformationForEncashmentViewModels.FromSql($"SP_Get_FDR_Information_For_Encashment {fdrID}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetListOfFDREncashmentWithFDRViewModel>> GetFDREncashmentInfo(int? fdrID)
        {
            return await _context.getListOfFDREncashmentWithFDRViewModels.FromSql($"SP_GetFDREncashmentInfo_FDRID {fdrID}").AsNoTracking().ToListAsync();
        }
        public async Task<bool> UpdatefDREncashmentApprove(int? Id, int? status, string remarks)
        {
            var Encash = _context.fDREncashments.Find(Id);
            Encash.status = status;
            Encash.remarks = remarks;
            _context.Entry(Encash).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFDREncashmentByEncashId(int id)
        {
            _context.fDREncashments.RemoveRange(_context.fDREncashments.Where(x => x.Id == id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region FDR Report

        public async Task<IEnumerable<FDRReportViewModel>> ReportFdrFtInstruction(int? fdrMasterId)
        {
            return await _context.fDRReportViewModels.FromSql($"SP_RPT_GetFTInstruction {fdrMasterId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRReportViewModel>> GetFdrReportingHistory(int? bankId, DateTime? frmDate, DateTime? toDate,string sOF)
        {
            return await _context.fDRReportViewModels.FromSql($"SP_Get_FDR_Reporting_History {bankId}, {frmDate}, {toDate}, {sOF}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRReportViewModel>> ReportCapitalFdrEncashment(int? fdrDetailsId)
        {
            return await _context.fDRReportViewModels.FromSql($"SP_RPT_GetFDREncashment {fdrDetailsId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRScheduleReportViewModel>> FdrScheduleReport(string FDate, string TDate)
        {
           
            return await _context.fDRScheduleReportViewModels.FromSql($"SP_RPT_FDRSheduleReport {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FDRInterestScheduleReportViewModel>> FdrInterestScheduleReport(int? fdrId)
        {

            return await _context.fDRInterestScheduleReportViewModels.FromSql($"SP_RPT_FDRInterestReport {fdrId}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
