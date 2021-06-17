using OPUSERP.CLUB.Data.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance.Interface
{
    public interface IInvoiceService
    {
        Task<bool> SaveInvoice(Invoice invoice);
        Task<IEnumerable<Invoice>> GetInvoice();
        Task<Invoice> GetInvoiceById(int id);
        Task<bool> DeleteInvoiceById(int id);
    }
}
