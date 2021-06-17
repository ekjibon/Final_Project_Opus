using OPUSERP.CLUB.Data.Forum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Forum.Models
{
    public class ForumViewModel
    {
        public string topic { get; set; }
        public string details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public int? memberId { get; set; }

        public int? topicId { get; set; }

        public IEnumerable<Topic> topics { get; set; }

        public IEnumerable<MemberComment> comments { get; set; }

        public Topic topicz { get; set; }
    }
}
