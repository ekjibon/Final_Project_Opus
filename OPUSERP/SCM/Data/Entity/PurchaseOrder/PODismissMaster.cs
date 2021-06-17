using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PODismissMaster", Schema = "SCM")]
    public class PODismissMaster:Base
    {
        public string reviseNo { get; set; }

        public int? purchaseId { get; set; }
        public PurchaseOrderMaster purchase { get; set; }

        public int? dismissStatus { get; set; }

        public DateTime? dismissDate { get; set; }

        public string remarks { get; set; }
    }
}
