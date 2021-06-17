using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("RelProductAgreement", Schema = "CRM")]
    public class RelProductAgreement : Base
    {
        public int? productId { get; set; }
        public Product product { get; set; }

        public int? agreementId { get; set; }
        public Agreement agreement { get; set; }

        [NotMapped]
        public string productName { get; set; }
        [NotMapped]
        public string productDescription { get; set; }


    }
}
