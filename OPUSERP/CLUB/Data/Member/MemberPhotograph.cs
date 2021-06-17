using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Member
{
    [Table("MemberPhotograph", Schema = "Club")]
    public class MemberPhotograph : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public MemberInfo employee { get; set; }

        [Required]
        public string url { get; set; }

        public string remarks { get; set; }

        [Required]
        public string type { get; set; }

    }
}
