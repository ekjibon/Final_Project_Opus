using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Models
{
    public class OrganoOrganizationViewModel
    {
        public int organoOrganizationId { get; set; }

        public int? organizationTypeId { get; set; }

        public int? organoOrganizationParrentId { get; set; }

        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }

        public int designationId { get; set; }

        public int? altDesignationId { get; set; }

        public int numberOfPost { get; set; }

        public int IsHead { get; set; }

        public OrganoOrganizationLn fLang { get; set; }
        public PostLn fLangPost { get; set; }

        public IEnumerable<Designation> designations { get; set; }

        public IEnumerable<OrganoOrganization> organoOrganizations { get; set; }

        public IEnumerable<OrganizationType> organizationTypes { get; set; }

        public IEnumerable<OrganoOrganization> organoRoots { get; set; }

        public string organizationType { get; set; }

    }
}
