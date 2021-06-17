using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class SubtaskViewModel
    {
        public int? taskIdForFollower { get; set; }
        public decimal? TaskHourName { get; set; }
        public DateTime? TaskHourDate { get; set; }
        public DateTime? starttimeSubtask { get; set; }
        public DateTime? endtimeSubtask { get; set; }
        public string SubTaskName { get; set; }
        public string remarksSubtask { get; set; }

        public string followupname { get; set; }
        public string followupRemarks { get; set; }
        public int? followupactivityCategoryId { get; set; }
        public int? followupactivitySessionId { get; set; }
        public int? followupactivityTypeId { get; set; }
        public int? followupactivityStatusId { get; set; }
        public DateTime? targetDate { get; set; }
        public DateTime? executionDate { get; set; }
        public DateTime? reminderDate { get; set; }
        public int? remainderStatus { get; set; }

        public string TaskStatusName { get; set; }
        public string TaskStatusNameremarks { get; set; }
        public DateTime? TaskStatusDate { get; set; }
    }
}
