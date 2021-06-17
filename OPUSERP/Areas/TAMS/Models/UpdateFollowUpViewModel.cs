using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class UpdateFollowUpViewModel
    {
        public string followupname { get; set; }

        public string followupRemarks { get; set; }

        public string followupNotes { get; set; }

        public string satusfollowup { get; set; }

        public int? taskIdForFollower { get; set; }

        public int Id { get; set; }

        public int? followupactivityStatusId { get; set; }

        public int? followupactivityCategoryId { get; set; }

        public int? followupactivitySessionId { get; set; }

        public DateTime? targetDate { get; set; }

        public DateTime? executionDate { get; set; }

        public DateTime? reminderDate { get; set; }

        public int? remainderStatus { get; set; }
    }
}
