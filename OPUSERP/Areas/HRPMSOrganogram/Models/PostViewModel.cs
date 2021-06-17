using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Models
{
    public class PostViewModel
    {
        public int postId { get; set; }

        public int organoOrganizationId { get; set; }

        public int designationId { get; set; }

        public int altDesignationId { get; set; }

        public int numberOfPost { get; set; }

        public int IsHead { get; set; }

        public PostLn fLang { get; set; }

        public IEnumerable<Post> posts { get; set; }

        public IEnumerable<OrganoOrganization> organoOrganizations { get; set; }

        public IEnumerable<Designation> designations { get; set; }
    }
}
