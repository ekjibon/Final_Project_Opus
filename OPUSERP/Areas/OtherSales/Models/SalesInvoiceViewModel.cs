using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesInvoiceViewModel
    {
        public int? masterId { get; set; }
        public int? customerId { get; set; }
        public int? storeId { get; set; }
        public int? exportType { get; set; }
        public int? TrentdetailId { get; set; }
        public int?[] itemPriceFixingId { get; set; }
        public int?[] itemspecId { get; set; }
        public decimal?[] tblQuantity { get; set; }
        public decimal?[] vattotal { get; set; }
        public decimal?[] taxtotal { get; set; }
        public decimal?[] cdtotal { get; set; }
        public decimal?[] sdtotal { get; set; }
        public decimal?[] attotal { get; set; }
        public decimal?[] rdtotal { get; set; }
        public decimal?[] distotal { get; set; }
        public decimal?[] lineTotal { get; set; }
        public decimal?[] lineprice { get; set; }
        public int?[] rendetailId { get; set; }
        public string shippingAddress { get; set; }
        public string alternateMobile { get; set; }
        public string[] context { get; set; }
        public string tconditext { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }

        public string salesInvoiceNo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? grossTotal { get; set; }
        public decimal? grossVAT { get; set; }
        public decimal? grossTAX { get; set; }
        public decimal? grossSD { get; set; }
        public decimal? grossCD { get; set; }
        public decimal? grossAT { get; set; }
        public decimal? grossRD { get; set; }
        public decimal? grossDiscount { get; set; }
        public decimal? netTotal { get; set; }
        public decimal? vds { get; set; }
        public decimal? shippingCost { get; set; }

        //LC
        public string lcNo { get; set; }
        public DateTime? lcDate { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }

        //POS
        public int? paymentModeId { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cardBankAmount { get; set; }
        public decimal? cardAmount { get; set; }
        public decimal? mobileAmount { get; set; }
        public int? isMember { get; set; }
        public string customerMobile { get; set; }
        public string customerName { get; set; }
        public decimal? change { get; set; }
        public decimal? given { get; set; }
        public int? bankInfoId { get; set; }
        public int? cardTypeId { get; set; }
        public string trxNumber { get; set; }
        public int? mobileBankingTypeId { get; set; }
        public decimal?[] priceList { get; set; }
        public string address { get; set; }
        public string cardNo { get; set; }
        public string baleboy { get; set; }

        public IEnumerable<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> itemPriceFixings { get; set; }
        public IEnumerable<SalesInvoiceMaster> salesInvoiceMasters { get; set; }
        public IEnumerable<SalesReturnInvoiceMaster> salesReturnInvoiceMasters { get; set; }
        public SalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }
        public IEnumerable<RelSupplierCustomerResourse> relSupplierCustomerResourses { get; set; }
        public IEnumerable<RelSupplierCustomerResourseViewModel>relSupplierCustomerResourseViewModels { get; set; }
        public IEnumerable<SalesInvoiceDetail> salesInvoiceDetails { get; set; }
        public IEnumerable<SalesReturnInvoiceDetail> salesReturnInvoiceDetails { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<PatientRepresentative> PatientRepresentatives { get; set; }
        public IEnumerable<PatientService> PatientServices { get; set; }
        public Company company { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<SalesTermsConditions> salesTermsConditions { get; set; }
        public IEnumerable<Address> GetAddresses { get; set; }
        public IEnumerable<Contacts> Contacts { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public IEnumerable<TermsAndConditions> termsAndConditions { get; set; }

        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<CardType> cardTypes { get; set; }
        public IEnumerable<Bank> bankInfos { get; set; }
        public IEnumerable<WalletType> mobileBankingTypes { get; set; }
        public decimal? discountRate { get; set; }

        //POS MEnu
        public IEnumerable<SalesMenuCategory> salesMenuCategories { get; set; }
        public IEnumerable<MenuItemViewModel> menuItemViewModels { get; set; }
        public string billingType { get; set; }
        public DateTime? serviceFromDate { get; set; }
        public DateTime? serviceToDate { get; set; }
        public IEnumerable<PatientService> patientServices { get; set; }

    }
}
