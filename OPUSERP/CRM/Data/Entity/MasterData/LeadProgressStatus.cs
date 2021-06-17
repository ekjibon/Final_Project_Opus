using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("LeadProgressStatus", Schema = "CRM")]
    public class LeadProgressStatus:Base
    {
        public string name { get; set; }
    }
}
