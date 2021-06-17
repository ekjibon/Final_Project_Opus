using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData.Interfaces
{
   public interface IFundSourceService
    {
        #region Fund Source

        Task<bool> SaveFundSource(FundSource voucherType);
        Task<IEnumerable<FundSource>> GetFundSource();
        Task<FundSource> GetFundSourceById(int id);
        Task<bool> DeleteFundSourceById(int id);

        #endregion

      
    }
}
