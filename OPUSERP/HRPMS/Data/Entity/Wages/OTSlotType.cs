using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("OTSlotType", Schema = "HR")]
    public class OTSlotType:Base
    {
        public string slotName { get; set; }
        public decimal slotHour { get; set; }
    }
}
