using OPUSERP.POS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
    public interface IPosInvoiceDetailService
    {
        #region Pos Invoice Detail

        Task<int> SavePosInvoiceDetail(PosInvoiceDetail posInvoiceDetail);
        Task<IEnumerable<PosInvoiceDetail>> GetPosInvoiceDetail();
        Task<PosInvoiceDetail> GetPosInvoiceDetailById(int id);
        Task<bool> DeletePosInvoiceDetailById(int id);
        Task<IEnumerable<PosInvoiceDetail>> GetPosInvoiceDetailByMasterId(int masterId);
        Task<bool> DeletePosInvoiceDetailByMasterId(int id);

        #endregion

        #region POS Replica Details

        Task<int> SavePosRepInvoiceDetail(PosRepInvoiceDetail posRepInvoiceDetail);
        Task<IEnumerable<PosRepInvoiceDetail>> GetPosRepInvoiceDetail();
        Task<PosRepInvoiceDetail> GetPosRepInvoiceDetailById(int id);
        Task<bool> DeletePosRepInvoiceDetailById(int id);
        Task<IEnumerable<PosRepInvoiceDetail>> GetPosRepInvoiceDetailByMasterId(int masterId);
        Task<bool> DeletePosRepInvoiceDetailByMasterId(int id);
        Task<bool> DeletePosRepInvoiceDetailByCustomerId(int customerId);

        #endregion

        #region ReturnSaleDetail

        Task<int> SaveSalesReturnInvoiceDetail(PosSalesReturnInvoiceDetail salesReturnInvoiceDetail);
        Task<bool> DeleteSalesReturnInvoiceDetailByMasterId(int masterId);
        Task<IEnumerable<PosSalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailBySaleMaster(int MasterId);
        Task<IEnumerable<PosSalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailByMasterId(int masterId);
        Task<PosSalesReturnInvoiceDetail> GetSalesReturnInvoiceDetailById(int id);

        #endregion
    }
}
