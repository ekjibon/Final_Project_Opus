using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("ResourceSkill", Schema = "VMS")]
    public class ResourceSkill:Base
    {
        public string skillCode { get; set; }

        public string skillNameBN { get; set; }

        public string skillNameEN { get; set; }

        public string shortName { get; set; }
    }
}
