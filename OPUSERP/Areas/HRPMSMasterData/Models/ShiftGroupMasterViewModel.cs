using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ShiftGroupMasterViewModel
    {
        public int masterId { get; set; }
        [Required]
        public string shiftName { get; set; }
        public string shiftNameBn { get; set; }

        public ShiftGroupLn fLang { get; set; }

        public IEnumerable<ShiftGroupMaster> shiftGroupMasterlist { get; set; }
        public IEnumerable<ShiftGroupDetail> shiftGroupDetailslist { get; set; }
    }
}
