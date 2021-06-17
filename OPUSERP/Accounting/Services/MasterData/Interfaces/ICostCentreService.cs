using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData.Interfaces
{
    public interface ICostCentreService
    {
        #region Cost Centre

        Task<int> SavecostCentre(CostCentre costCentre);
        Task<IEnumerable<CostCentre>> GetCostCentres();
        Task<CostCentre> GetcostCentreById(int id);
        Task<bool> DeletecostCentresById(int id);

        #endregion

        #region Auto Voucher Name

        Task<int> SaveAutoVoucherName(AutoVoucherName autoVoucherName);
        Task<IEnumerable<AutoVoucherName>> GetAllAutoVoucherName();
        Task<AutoVoucherName> GetAutoVoucherNameId(int id);
        Task<bool> DeleteAutoVoucherNameById(int id);

        #endregion

        #region Auto Voucher Master

        Task<int> SaveAutoVoucherMaster(AutoVoucherMaster autoVoucherMaster);
        Task<IEnumerable<AutoVoucherMaster>> GetAllAutoVoucherMaster();
        Task<IEnumerable<AutoVoucherMaster>> GetDuplicateAutoVoucherMaster(int masterId, int? nameId);
        Task<AutoVoucherMaster> GetAutoVoucherMasterById(int id);
        Task<bool> DeleteAutoVoucherMasterById(int id);

        #endregion

        #region Auto Voucher Detail

        Task<int> SaveAutoVoucherDetail(AutoVoucherDetail autoVoucherDetail);
        Task<IEnumerable<AutoVoucherDetail>> GetAutoVoucherDetailByMasterId(int masterId);
        Task<AutoVoucherDetail> GetAutoVoucherDetailById(int id);
        Task<bool> DeleteAutoVoucherDetailByMasterId(int masterId);

        #endregion

        #region Auto Voucher SP FOR FDR/Investment

        Task<IEnumerable<DeleteVoucherViewModel>> FDRCreateAutoVoucher(decimal? amount, string fdrDate, string fdrNo, string createBy);
        Task<IEnumerable<DeleteVoucherViewModel>> FDREncashmentAutoVoucher(decimal? totalRecAmount, decimal? banhchrgAmount, decimal? taxAmount, decimal? fdrAmount, decimal? interestAmount, string fdrDate, string fdrNo, string createBy);
        Task<IEnumerable<DeleteVoucherViewModel>> FDRenewAutoVoucher(decimal? amount, string fdrDate, string fdrNo, string createBy);
        #endregion
    }
}
