using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class QualificationHeadViewModel
    {
        public int? qualificationHeadId { get; set; }

        [Required]
        public string qualificationHeadName { get; set; }

        
        public IEnumerable<QualificationHead> qualificationHeads { get; set; }
    }
}
