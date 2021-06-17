using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Wages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class OTSetaupViewModel
    {
        public int? oTSetupMasterId { get; set; }
        public int? oTSetupDetailId { get; set; }
        public int? shiftGroupMasterId { get; set; }
        public int bufferingmin { get; set; }
        public string description { get; set; }
        public string[] descriptionall { get; set; }
        public decimal? basicPercent { get; set; }
        public decimal?[] basicPercentall { get; set; }

        public int? oTSlotTypeId { get; set; }
        public int?[] oTSlotTypeIdall { get; set; }
        public IEnumerable<OTSlotType> oTSlotType { get; set; }
        public IEnumerable<OTSetupDetail> oTSetupDetails { get; set; }
        public IEnumerable<OTSetupMaster> oTSetupMasters { get; set; }
        public OTSetupMaster oTSetupMaster { get; set; }
        public IEnumerable<ShiftGroupMaster> shiftGroupMasters { get; set; }
    }
}
