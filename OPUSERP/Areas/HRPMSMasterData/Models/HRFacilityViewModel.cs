using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HRFacilityViewModel
    {
        public int? Id { get; set; }
        public string facilityName { get; set; }
        public string facilityCode { get; set; }

        public IEnumerable<HRFacility> hRFacilities { get; set; }
    }
}
