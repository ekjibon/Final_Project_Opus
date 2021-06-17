using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Forum
{
    [Table("Topic", Schema = "Club")]
    public class Topic : Base
    {
        public string topic { get; set; }
        public string details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public int? memberId { get; set; }
        public MemberInfo member { get; set; }

        public int? status { get; set; }
    }
}
