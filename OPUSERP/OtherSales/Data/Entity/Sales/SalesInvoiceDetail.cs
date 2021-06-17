using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesInvoiceDetail", Schema = "OSales")]
    public class SalesInvoiceDetail:Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? itemSpecicationId { get; set; }
        public ItemSpecification itemSpecication { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? sdAmount { get; set; }
        public decimal? cdAmount { get; set; }
        public decimal? atAmount { get; set; }
        public decimal? rdAmount { get; set; }
        public decimal? disAmount { get; set; }
        public decimal? lineTotal { get; set; }
        public int? rentInvoiceDetailId { get; set; }
        public RentInvoiceDetail rentInvoiceDetail { get; set; }

        public int? patientServiceDetailsId { get; set; }
        public PatientServiceDetail patientServiceDetails { get; set; }

    }
}
