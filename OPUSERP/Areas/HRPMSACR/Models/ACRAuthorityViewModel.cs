using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class ACRAuthorityViewModel
    {
        public int? empDesignationId { get; set; }

        public string empDesignationName { get; set; }

        public int? CADesignationId { get; set; }

        public string CADesignationName { get; set; }

        public int? CSADesignationId { get; set; }

        public string CSADesignationName { get; set; }

        public ACRLn fLang { get; set; }

        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<ACRAuthority> aCRAuthorities { get; set; }
    }
}
