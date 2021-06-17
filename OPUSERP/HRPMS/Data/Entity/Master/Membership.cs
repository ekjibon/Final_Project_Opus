using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Membership", Schema = "HR")]
    public class Membership:Base
    {
        [Required]
        public string membershipName { get; set; }
        public string membershipNameBn { get; set; }

        public string membershipShortName { get; set; }
    }
}
