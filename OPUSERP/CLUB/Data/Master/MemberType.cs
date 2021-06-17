using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Master
{
    [Table("MemberType", Schema = "Club")]
    public class MemberType : Base
    {
        [Required]
        public string memberType { get; set; }
        public string memberTypeBn { get; set; }
    }
}
