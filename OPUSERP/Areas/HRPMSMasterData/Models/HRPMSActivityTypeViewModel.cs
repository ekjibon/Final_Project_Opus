using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HRPMSActivityTypeViewModel
    {
        public int? HRPMSActivityTypeId { get; set; }

        [Required]
        public string HRPMSActivityTypeName { get; set; }


        public IEnumerable<HRPMSActivityType> hRPMSActivityTypes { get; set; }
    }
}
