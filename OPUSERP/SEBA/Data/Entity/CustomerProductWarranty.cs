using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SEBA.Data.Entity
{
    [Table("CustomerProductWarranty", Schema = "SEBA")]
    public class CustomerProductWarranty : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public DateTime? salesDate { get; set; }
        public DateTime? warrantyStartDate { get; set; }
        public DateTime? warrantyEndDate { get; set; }

        public string warrantyDescription { get; set; }
        [MaxLength(50)]
        public string invoiceNo { get; set; }
        [MaxLength(50)]
        public string serialNo { get; set; }
        [MaxLength(80)]
        public string modelNo { get; set; }
    }
}
