using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("HRPMSOrganizationType", Schema = "HR")]
    public class HRPMSOrganizationType:Base
    {
        public string name { get; set; }
    }
}
