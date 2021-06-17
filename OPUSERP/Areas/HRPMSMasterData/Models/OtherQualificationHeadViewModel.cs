using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class OtherQualificationHeadViewModel
    {
        public int? OtherQualificationHeadId { get; set; }

        [Required]
        public string OtherQualificationHeadName { get; set; }


        public IEnumerable<OtherQualificationHead> otherQualificationHeads { get; set; }
    }
}
