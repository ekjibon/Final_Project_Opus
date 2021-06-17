using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("OrganizationType", Schema = "HR")]
    public class OrganizationType : Base
    {
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }
    }
}
