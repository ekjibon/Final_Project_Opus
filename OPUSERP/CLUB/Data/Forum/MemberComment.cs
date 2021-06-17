using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Forum
{
    [Table("MemberComment", Schema = "Club")]
    public class MemberComment : Base
    {
        public string details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public int? topicId { get; set; }
        public Topic topic { get; set; }

        public int? memberId { get; set; }
        public MemberInfo member { get; set; }

    }
}
