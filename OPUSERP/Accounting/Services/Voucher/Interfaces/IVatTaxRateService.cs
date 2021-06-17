using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Models.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher.Interfaces
{
    public interface IVatTaxRateService
    {
        #region VatTaxRateService

        Task<int> SavevatTaxRate(VatTaxRate vatTaxRate);
        Task<IEnumerable<VatTaxRate>> GetvatTaxRate();
        Task<IEnumerable<VatTaxRate>> GetvatTaxRatebyTypeId(int Id);
        Task<bool> DeleteVatTaxRateById(int id);

        #endregion


    }
}
