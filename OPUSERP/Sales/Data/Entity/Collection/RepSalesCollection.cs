
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Data.Entity.Collection
{
    [Table("RepSalesCollection", Schema = "Sales")]
    public class RepSalesCollection : Base
    {
        public int? salesCollectionId { get; set; }
        public SalesCollection salesCollection { get; set; }
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? collectionDate { get; set; }

        public decimal? collectionAmount { get; set; }

        public string collectionNumber { get; set; }

        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? mobileAmount { get; set; }
     
        //public int? storeId { get; set; }
        //public Store store { get; set; }

    }
}
