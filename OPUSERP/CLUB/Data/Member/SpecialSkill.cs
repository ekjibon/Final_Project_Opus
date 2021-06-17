using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Member
{
    [Table("SpecialSkill", Schema = "Club")]
    public class SpecialSkill : Base
    {
        [Required]
        public string skilName { get; set; }
        public string skilNameBn { get; set; }

        public string shortName { get; set; }
    }
}
