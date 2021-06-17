using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskFollowUp", Schema = "TMS")]
    public class TaskFollowUp:Base
    {
        public string name { get; set; }

        public string description { get; set; }

        public string note { get; set; }

        public string status { get; set; }

        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public int? activityStatusId { get; set; }
        public ActivityStatus activityStatus { get; set; }

        public int? activityCategoryId { get; set; }
        public ActivityCategory activityCategory { get; set; }

        public int? activitySessionId { get; set; }
        public ActivitySession activitySession { get; set; }

        public int? activityTypeId { get; set; }
        public ActivityType activityType { get; set; }

        public DateTime? targetDate { get; set; }

        public DateTime? executionDate { get; set; }

        public DateTime? reminderDate { get; set; }

        public int? remainderStatus { get; set; }
    }
}
