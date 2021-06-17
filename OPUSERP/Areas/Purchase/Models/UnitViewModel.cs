
using OPUSERP.SCM.Data.Entity.MasterData;

using System.Collections.Generic;

namespace OPUSERP.Areas.Purchase.Models
{
    public class UnitViewModel
    {
        public int UnitId { get; set; }

        public string name { get; set; }

        public IEnumerable<Unit> units { get; set; }
    }
}
