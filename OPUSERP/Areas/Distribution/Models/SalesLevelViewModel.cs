using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class SalesLevelViewModel
    {
        public int? Id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int? salesLevelId { get; set; }
        public IEnumerable<SalesLevel> salesLevels { get; set; }
    }
}
