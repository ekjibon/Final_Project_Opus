using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class RlnSBUPNSViewModel
    {
        public int? Id { get; set; }

        public int? pNSId { get; set; }

        public int? specialBranchUnitId { get; set; }

        public IEnumerable<PNS> pNs { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<RlnSBUPNS> rlnSBUPNs { get; set; }
    }
}
