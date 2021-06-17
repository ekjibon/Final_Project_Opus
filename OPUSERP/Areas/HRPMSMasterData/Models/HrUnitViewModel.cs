using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HrUnitViewModel
    {
        public int hrUnitId { get; set; }

        public string unitName { get; set; }
        public string unitNameBn { get; set; }
        public string shortName { get; set; }

        public IEnumerable<HrUnit> hrUnits { get; set; }
    }
}
