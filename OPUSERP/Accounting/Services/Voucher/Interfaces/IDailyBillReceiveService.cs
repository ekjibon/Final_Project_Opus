using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher.Interfaces
{
    public interface IDailyBillReceiveService
    {
        #region DailyBillReceive

        Task<int> SavedailyBillReceive(DailyBillReceive dailyBillReceive);
        Task<IEnumerable<DailyBillReceive>> GetdailyBillReceive();
      
        Task<bool> DeletedailyBillReceiveById(int id);
        Task<SLNoViewModel> GetSLNumber();
        Task<AutoProcessNumberViewModel> GetProcessNumber();
        Task<DailyBillReceive> GetdailyBillReceivebyId(int id);
        Task<bool> UpdatedailyBillReceive(int? Id, int? VMID);
        #endregion
        #region bill payment
        Task<DailyBillPaymentViewModel> GetDailyBillPaymentViewModel(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal MushokProvided, decimal Rebatable, decimal Deductable, decimal VATPayBy, decimal Rebate);
        Task<DailyBillPaymentViewModel> GetDailyBillPaymentViewModelTax(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal VATPayBy, int vendorId, int taxyearId, int calId, decimal BaseCalculatePrice);
        #endregion

    }
}
