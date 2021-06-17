using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.TAMS.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("ActivityMaster", Schema = "CRM")]
    public class ActivityMaster : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }
        public int? activityMasterId { get; set; }
        public ActivityMaster activityMaster { get; set; }
        public int? activityStatusId { get; set; }
        public ActivityStatus activityStatus { get; set; }

        public int? activityCategoryId { get; set; }
        public ActivityCategory activityCategory { get; set; }
        public int? activitySessionId { get; set; }
        public ActivitySession activitySession { get; set; }
        public int? activityTypeId { get; set; }
        public ActivityType activityType { get; set; }

        public DateTime? activitiesDate { get; set; }
        public string ownerType { get; set; }
        public string taskOwner { get; set; }
        public string subject { get; set; }
        public string priority { get; set; }
        public string description { get; set; }
        public DateTime? reminderDate { get; set; }
        public DateTime? reminderTime { get; set; }
        public int? isreminder { get; set; }
        public int? taskSectionId { get; set; }
        public TaskSection taskSection { get; set; }
        [NotMapped]
        public int? totalchild { get; set; }

    }
}
