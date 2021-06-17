using OPUSERP.Areas.POS.Models;
using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
   public interface IPosInvoiceMasterService
    {
        #region POS Replica Master

        Task<int> SavePosInvoiceMaster(PosInvoiceMaster posInvoiceMaster);
        Task<IEnumerable<PosInvoiceMaster>> GetPosInvoiceMaster();
        Task<PosInvoiceMaster> GetPosInvoiceMasterById(int id);
        Task<bool> DeletePosInvoiceMasterById(int id);

        #endregion

        #region POS Replica Master

        Task<int> SavePosRepInvoiceMaster(PosRepInvoiceMaster posRepInvoiceMaster);
        Task<IEnumerable<PosRepInvoiceMaster>> GetPosRepInvoiceMaster();
        Task<PosRepInvoiceMaster> GetPosRepInvoiceMasterById(int id);
        Task<bool> DeletePosRepInvoiceMasterById(int id);
        Task<IEnumerable<PosRepInvoiceMaster>> GetPosRepInvoiceMasterbyDateRange(DateTime fromDate, DateTime toDate);

        #endregion

        #region SalesReturnInvoice

        Task<int> SaveSalesReturnInvoiceMaster(PosSalesReturnInvoiceMaster salesReturnInvoiceMaster);
        Task<IEnumerable<PosSalesReturnInvoiceMaster>> GetAllSalesReturnInvoiceMaster();
      
        Task<PosSalesReturnInvoiceMaster> GetSalesReturnInvoiceMasterById(int id);
        Task<IEnumerable<PosSalesReturnInvoiceMaster>> GetAllDueSalesReturnInvoiceMaster();
       
        Task<IEnumerable<PosInvoiceMaster>> GetSaleInvoiceListForReturn();
        Task<IEnumerable<StockItemViewModel>> GetReturnDetailsInvoiceList(int Id);
        Task<bool> UpdateSalesReturnInvoiceMasterById(int id);

        #endregion
    }
}
