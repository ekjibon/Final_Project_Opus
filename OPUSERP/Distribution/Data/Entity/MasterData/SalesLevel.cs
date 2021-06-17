using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("SalesLevel", Schema = "Distribution")]
    public class SalesLevel:Base
    {
        public string name { get; set; }
        public string code { get; set; }
        public int? salesLevelId { get; set; }
        public SalesLevel salesLevel { get; set; }
    }
}
