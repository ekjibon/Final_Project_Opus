using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("DistributorType", Schema = "Distribution")]
    public class DistributorType:Base
    {
        public string name { get; set; }

    }
}
