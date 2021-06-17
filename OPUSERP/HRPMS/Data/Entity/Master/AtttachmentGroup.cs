using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("AtttachmentGroup", Schema = "HR")]
    public class AtttachmentGroup : Base
    {
        [MaxLength(250)]
        public string groupName { get; set; }
        [MaxLength(250)]
        public string groupNameBn { get; set; }

    }
}
