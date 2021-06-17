using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ShiftGroupDetailViewModel
    {
        public int shiftMasterId { get; set; }
        [Required]
        public string weekDay { get; set; }
        [Required]
        public string startTime { get; set; }
        [Required]
        public string endTime { get; set; }

        public string holiday { get; set; }
        
        public int shiftGroupMasterId { get; set; }

        public ShiftGroupDetailsLn fLang { get; set; }

        public IEnumerable<ShiftGroupDetail> shiftGroupDetailslist { get; set; }

        public IEnumerable<ShiftGroupMaster> shiftGroupMasterslist { get; set; }
    }
}
