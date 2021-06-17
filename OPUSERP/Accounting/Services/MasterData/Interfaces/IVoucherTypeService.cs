using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData.Interfaces
{
   public interface IVoucherTypeService
    {
        #region VoucherType

        Task<bool> SaveVoucherType(VoucherType voucherType);
        Task<IEnumerable<VoucherType>> GetVoucherType();
        Task<VoucherType> GetVoucherTypeById(int id);
        Task<bool> DeleteVoucherTypeById(int id);

        #endregion

        #region Transection Mode

        Task<IEnumerable<TransectionMode>> GetTransectionMode();

        #endregion
    }
}
