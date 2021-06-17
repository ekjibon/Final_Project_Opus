using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class UnitViewModel
    {
        public int unitId { get; set; }

        public string unitName { get; set; }

        public string description { get; set; }

        public IEnumerable<Unit> units { get; set; }
    }
}
