using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("PNS", Schema = "HR")]
    public class PNS:Base
    {
        public string code { get; set; }

        public string name { get; set; }
    }
}
