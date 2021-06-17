using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher.Interfaces
{
    public interface IRateTypeService
    {
        #region RateTypeService

        Task<int> SaveRateType(RateType rateType);
        Task<IEnumerable<RateType>> GetrateType();
        Task<bool> DeleteRateTypeById(int id);

        #endregion


    }
}
