using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("OTSetupMaster", Schema = "HR")]
    public class OTSetupMaster:Base
    {
        public int? shiftGroupMasterId { get; set; }
        public ShiftGroupMaster shiftGroupMaster { get; set; }

        public int bufferingmin { get; set; }
    }
}
