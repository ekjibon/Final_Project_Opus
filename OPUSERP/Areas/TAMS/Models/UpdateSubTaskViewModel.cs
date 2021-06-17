using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class UpdateSubTaskViewModel
    {
        public int Id { get; set; }
        public int? taskIdForFollower { get; set; }
        public DateTime? starttimeSubtask { get; set; }
        public DateTime? endtimeSubtask { get; set; }
        public string SubTaskName { get; set; }
        public string remarksSubtask { get; set; }
        public string commentsSubtask { get; set; }
        public string satusSubtask { get; set; }
    }
}
