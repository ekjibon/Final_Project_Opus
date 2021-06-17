using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("Post", Schema = "HR")]
    public class Post : Base
    {
        public int? organoOrganizationId { get; set; }
        public OrganoOrganization organoOrganization { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? altDesignationId { get; set; }
        public Designation altDesignation { get; set; }

        public int IsHead { get; set; }

        public int numberOfPost { get; set; }
    }
}
