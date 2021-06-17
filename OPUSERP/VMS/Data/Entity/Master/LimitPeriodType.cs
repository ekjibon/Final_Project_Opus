using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("LimitPeriodType", Schema = "VMS")]
    public class LimitPeriodType:Base
    {
        public string limitPeriodTypeName { get; set; }
    }
}
