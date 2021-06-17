using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//using RentInvoiceMaster = OPUSERP.Rental.Data.Entity.Sales.RentInvoiceMaster;

namespace OPUSERP.Areas.Rental.Models
{
    public class RentInvoiceViewModel
    {
        public int? rentInvoiceMasterId { get; set; }
        public int? masterId { get; set; }
        public int? customerId { get; set; }
        public int? storeId { get; set; }
        public int? exportType { get; set; }
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
        public DateTime?[] startDate { get; set; }
        public DateTime?[] endDate { get; set; }
        public string[] startTime { get; set; }
        public string[] endTime { get; set; }
        public string[] context { get; set; }
        public string tconditext { get; set; }

        
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

        //LC
        public string lcNo { get; set; }
        public DateTime? lcDate { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }


        public IEnumerable<ItemPriceFixing> itemPriceFixings { get; set; }
        public IEnumerable<RentInvoiceMaster> salesInvoiceMasters { get; set; }   
        public RentInvoiceMaster salesInvoiceMaster { get; set; }
        public IEnumerable<RentInvoiceDetail> salesInvoiceDetails { get; set; }
        public IEnumerable<RantalListwithDateViewModel> rantalListwithDateViewModels { get; set; }   
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<RentTermsConditions> rentTermsConditions { get; set; }
        public IEnumerable<Address> GetAddresses { get; set; }
        public IEnumerable<Contacts> Contacts { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public Company company { get; set; }
        public IEnumerable<TermsAndConditions> termsAndConditions { get; set; }
    }
}
