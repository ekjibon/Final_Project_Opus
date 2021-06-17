using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("OTSetupDetail", Schema = "HR")]
    public class OTSetupDetail:Base
    {
        public int? oTSetupMasterId { get; set; }
        public OTSetupMaster oTSetupMaster { get; set; }

        public string description { get; set; }
        public decimal? basicPercent { get; set; }

        public int? oTSlotTypeId { get; set; }
        public OTSlotType oTSlotType { get; set; }


    }
}
