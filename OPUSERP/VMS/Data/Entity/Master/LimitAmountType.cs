using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("LimitAmountType", Schema = "VMS")]
    public class LimitAmountType:Base
    {
        public string limitAmountTypeName { get; set; }
    }
}
