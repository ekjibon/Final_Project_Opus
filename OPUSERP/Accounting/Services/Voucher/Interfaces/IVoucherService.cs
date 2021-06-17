using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher.Interfaces
{
    public interface IVoucherService
    {
        #region Voucher Master

        Task<int> SavevoucherMaster(VoucherMaster voucherMaster);
        Task<IEnumerable<VoucherMaster>> GetvoucherMaster();
        IQueryable<VoucherMaster> GetvoucherMasterByTypeId(int TypeId);
        Task<VoucherMaster> GetGetvoucherMasterById(int id);
        Task<VoucherMaster> GetGetvoucherMasterByJournalId(int id);
        Task<bool> DeletevoucherMasterById(int id);
        Task<IEnumerable<VoucherMaster>> GetvoucherMasterDetailsByTypeId(int TypeId);

        Task<bool> UpdateVoucherMasterStatus(int? id, int? isPosted, string updateBy);
        Task<bool> UpdateVoucherNo(int? id, string newVoucherNo);
        Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListForDelete(string FDate, string TDate, string voucherNo);
        Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListByVoucherNo(string voucherNo);
        Task<IEnumerable<DeleteVoucherViewModel>> DeleteVoucher(int? voucherMasterId, string remarks, string user);
        Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListForReBack(string FDate, string TDate, string voucherNo);
        Task<VoucherMaster> GetVoucherMasterByRefNo(string refNo);
        #endregion

        #region Voucher Details

        Task<int> SavevoucherDetail(VoucherDetail voucherDetail);
        Task<IEnumerable<VoucherDetail>> GetvoucherDetail();
        Task<IEnumerable<VoucherDetail>> GetvoucherDetailByMasterId(int MasterId);
        Task<IEnumerable<VoucherDetail>> GetvoucherDetailByMasterIdEdit(int MasterId);
        Task<VoucherDetail> GetGetvoucherDetailById(int id);
        Task<bool> DeletevoucherDetailByVMId(int vmid);
        Task<IEnumerable<VoucherDetail>> GetGetvoucherDetailByLedgerRelId(int id);

        #endregion

        #region Cost Centre Allocation

        Task<int> SavecostCentreAllocation(CostCentreAllocation costCentreAllocation);
        Task<IEnumerable<CostCentreAllocation>> GetCostCentreAllocations();
        Task<IEnumerable<CostCentreAllocation>> GetCostCentreAllocationsByDetailId(int id);
        Task<bool> DeleteCostCentreAllocationByVMId(int vmid);
        #endregion

        #region Dashboard

        Task<IEnumerable<VoucherViewModel>> GetVoucherListDashboard(string userName);
        Task<IEnumerable<VoucherViewModel>> CountVoucherByType(string userName);

        #endregion

        #region Voucher Setting 

        Task<int> SaveVoucherSetting(VoucherSetting voucherSetting);
        Task<IEnumerable<VoucherSetting>> GetVoucherSetting();
        Task<bool> DeleteVoucherSettingById(int id);

        #endregion

        #region Voucher Approve Log

        Task<int> SaveVoucherApproveLog(VoucherApproveLog voucherApproveLog);
        Task<IEnumerable<VoucherApproveLog>> GetVoucherApproveLog();
        Task<IEnumerable<VoucherApproveLog>> GetVoucherApproveLogByMasterId(int? masterId);
        Task<IEnumerable<VoucherMaster>> GetNotApproveVoucherList();
        //Task<VoucherApproveLog> GetVoucherApproveLogById(int? id);
        //Task<bool> DeleteVoucherApproveLogById(int? id);

        #endregion
    }
}
