using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HRPMSOrganizationTypeViewModel
    {
        public int? HRPMSOrganizationTypeId { get; set; }

        [Required]
        public string HRPMSOrganizationTypeName { get; set; }


        public IEnumerable<HRPMSOrganizationType> hRPMSOrganizationTypes { get; set; }
    }
}
