using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("VMSResourceType", Schema = "VMS")]
    public class VMSResourceType:Base
    {
        public string empType { get; set; }
        public string empTypeBn { get; set; }

        public string shortName { get; set; }
    }
}
