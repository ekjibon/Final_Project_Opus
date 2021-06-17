using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Award", Schema = "HR")]
    public class Award:Base
    {
        [Required]
        public string awardName { get; set; }
        public string awardNameBn { get; set; }

        public string awardShortName { get; set; }
    }
}
