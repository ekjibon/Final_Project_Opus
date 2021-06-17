using OPUSERP.Areas.Rental.Models;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Rental.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Services.Sales.Interfaces
{
    public interface IRentInvoiceMasterService
    {
        #region SaleInvoiceMaster
        Task<int> SaveSalesInvoiceMaster(RentInvoiceMaster salesInvoiceMaster);
        Task<bool> UpdateSalesInvoiceMaster(int Id, DateTime startDate, DateTime endDate);
        Task<bool> UpdateRSalesInvoiceMaster(int Id, DateTime startDate);
        Task<IEnumerable<RentInvoiceMaster>> GetAllSalesInvoiceMaster();
        Task<RentInvoiceMaster> GetSalesInvoiceMasterById(int id);
        Task<bool> DeleteSalesInvoiceMasterById(int id);
        Task<LeadAutoNumber> GetAutoRentInvoiceNoBySp(string invoiceDate);
        Task<IEnumerable<RentInvoiceMaster>> GetSalesInvoiceMasterByLeadId(int id);
        //Task<IEnumerable<PosSalesDetailModel>> GetSalesInvoiceMasterByDate(DateTime from, DateTime toDate);

        Task<IEnumerable<RantalListwithDateViewModel>> GetAllRantalListwithDateViewModel();
        Task<LeadAutoNumber> GetAutoSalesInvoiceNoBySp(string invoiceDate);
        //Task<IEnumerable<RentalInvoiceDataViewModel>> GetSalesInvoiceMasterById(int id);
        #endregion

        #region Terms
        Task<int> SaveRentTerms(RentTermsConditions salesInvoiceMaster);
        Task<RentTermsConditions> GetTermsConditionsById(int id);
        Task<IEnumerable<RentTermsConditions>> GetTermsConditionsByMasterId(int id);
        Task<bool> DeleteTermsConditionsById(int id);
        Task<bool> DeleteTermsConditionsByMasterId(int id);
        #endregion

        #region Terms and Condition

        Task<int> SaveTermsAndConditions(TermsAndConditions termsAndConditions);
        Task<IEnumerable<TermsAndConditions>> GetAllTermsAndConditions();
        Task<bool> DeleteTermsAndConditionsById(int id);

        #endregion
    }
}
