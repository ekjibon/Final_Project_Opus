using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesMenuOpeningBalance", Schema = "OSales")]
    public class SalesMenuOpeningBalance:Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }        
        
        public DateTime? openingDate { get; set; }        
        public decimal? openingQty { get; set; }
        
    }
}
