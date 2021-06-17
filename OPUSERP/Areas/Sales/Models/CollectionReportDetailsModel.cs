
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Sales.Models
{
    public class CollectionReportDetailsModel
    {
        public string invoiceNumber { get; set; }
        public string collectionNumber { get; set; }
        public DateTime? collectionDate { get; set; }
        public decimal? collectionAmount { get; set; }

       
       
        
    }
}
