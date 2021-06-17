using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("RenewalType", Schema = "VMS")]
    public class RenewalType : Base
    {
        public string renewalTypeName { get; set; }
    }
}
