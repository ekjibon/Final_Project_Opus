using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("HRDoner", Schema = "HR")]
    public class HRDoner:Base
    {
        public string name { get; set; }

        public string contactNumber { get; set; }

        public string Address { get; set; }

        public string code { get; set; }
    }
}
