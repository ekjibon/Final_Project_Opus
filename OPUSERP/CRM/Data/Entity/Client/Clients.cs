using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Client
{
    [Table("Clients", Schema = "CRM")]
    public class Clients : Base
    {
        public DateTime? agreementReceiveDate { get; set; }
        public int leadsId { get; set; }
        public Leads leads { get; set; }
        public int? isconverted { get; set; }
        public int? isactive { get; set; }

    }
}
