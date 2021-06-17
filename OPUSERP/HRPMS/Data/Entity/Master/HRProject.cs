using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("HRProject", Schema = "HR")]
    public class HRProject:Base
    {
        public string name { get; set; }

        public string code { get; set; }
    }
}
