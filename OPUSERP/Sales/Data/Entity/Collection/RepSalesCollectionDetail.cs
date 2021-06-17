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
    [Table("RepSalesCollectionDetail", Schema = "Sales")]
    public class RepSalesCollectionDetail : Base
    {
        public int? repSalesCollectionId { get; set; }
        public RepSalesCollection repSalesCollection { get; set; }

        public int? salesCollectionDetailId { get; set; }
        public SalesCollectionDetail salesCollectionDetail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? collectionDate { get; set; }

        public decimal? Amount { get; set; }

        public string remarks { get; set; }
    }
}
