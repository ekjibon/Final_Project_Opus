using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Data.Entity.Notes
{
    [Table("CRMNoteMaster", Schema = "CRM")]
    public class CRMNoteMaster:Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }
        public string title { get; set; }
        public string notesdescription { get; set; }
    }
}
