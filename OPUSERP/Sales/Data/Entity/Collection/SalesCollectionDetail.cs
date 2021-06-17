using OPUSERP.Data.Entity;
using OPUSERP.Sales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Data.Entity.Collection
{
    [Table("SalesCollectionDetail", Schema = "Sales")]
    public class SalesCollectionDetail:Base
    {
        public int? salesCollectionId { get; set; }
        public SalesCollection salesCollection { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? collectionDate { get; set; }

        public decimal? Amount { get; set; }

        public string remarks { get; set; }
    }
}
