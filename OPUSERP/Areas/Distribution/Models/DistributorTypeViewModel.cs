using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class DistributorTypeViewModel
    {
        public int? Id { get; set; }
        public string name { get; set; }
   
        public IEnumerable<DistributorType> distributorTypes { get; set; }
    }
}
