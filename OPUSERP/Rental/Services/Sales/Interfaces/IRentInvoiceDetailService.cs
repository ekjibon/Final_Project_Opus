using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Services.Sales.Interfaces
{
    public interface IRentInvoiceDetailService
    {
        #region ReturnSaleDetail
        Task<bool> SaveSalesInvoiceDetail(RentInvoiceDetail salesInvoiceDetail);
        Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetail();
        Task<RentInvoiceDetail> GetSalesInvoiceDetailById(int id);
        Task<bool> DeleteSalesInvoiceDetailById(int id);

        Task<IEnumerable<SalesInvoiceDetailViewModel>> GetAllSalesInvoiceDetailByAgreementId(int masterId);
        Task<IEnumerable<PatientInvoiceDetailViewModel>> GetPatientInvoiceItemByLeadsId(int leadId);
        Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId);
        Task<IEnumerable<RentInvoiceDetail>> GetAllSalesInvoiceDetailByMasterItemId(int itemId, int masterId);
        Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId);
        Task<IEnumerable<PatientServiceDetail>> GetAllSalesInvoiceDetailPatientByMasterId(int masterId);
        Task<IEnumerable<PatientServiceDetail>> GetAllSalesInvoiceDetailByMasterpatItemId(int itemId, int masterId);
        #endregion

    }
}
