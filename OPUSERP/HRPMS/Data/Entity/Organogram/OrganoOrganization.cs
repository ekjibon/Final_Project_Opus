using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("OrganoOrganization", Schema = "HR")]
    public class OrganoOrganization: Base
    {
        public int? organoOrganizationId { get; set; }
        public OrganoOrganization organoOrganization { get; set; }

        public int? organizationTypeId { get; set; }
        public OrganizationType organizationType { get; set; }

        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }

    }
}
