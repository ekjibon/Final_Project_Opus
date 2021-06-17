using OPUSERP.HRPMS.Data.Entity.Wages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class OTSlotTypeViewModel
    {
        public int? slotTypeId { get; set; }
        public string slotName { get; set; }
        public decimal slotHour { get; set; }
        public IEnumerable<OTSlotType> oTSlotTypes { get; set; }
    }
}
