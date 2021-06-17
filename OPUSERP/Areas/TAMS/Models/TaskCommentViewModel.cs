using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class TaskCommentViewModel
    {
        public int? taskIdForComment { get; set; }

        public int? projectIdForComment { get; set; }

        public string Comment { get; set; }

        public int? taskIdForFollower { get; set; }

        public int? projectIdForFollower { get; set; }

        public int?[] ids { get; set; }

        public int? taskIdForTags { get; set; }

        public int? projectIdForTags { get; set; }

        public string[] Tags { get; set; }

        public string title { get; set; }

        public int? taskIdForFile { get; set; }

        public int? projectIdForFile { get; set; }

        public int? TaskModalId { get; set; }
    }
}
