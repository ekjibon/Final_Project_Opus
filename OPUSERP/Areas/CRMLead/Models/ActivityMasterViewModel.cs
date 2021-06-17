using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class ActivityMasterViewModel
    {
        public int Id { get; set; }
       public int? taskId { get; set; }
        public int? leadsID { get; set; }
        public int? tasksectionId { get; set; }
        
        public string subject { get; set; }
        
       
        public int? category { get; set; }
        public int? session  { get; set; }
        public int? typeId { get; set; }
        public int? parentID { get; set; }
        public int? CparentID { get; set; }
        public int? activityStatusId { get; set; }
        public int?[] contactlist { get; set; }
        public int?[] teamlist { get; set; }
        public string leadName { get; set; }

        public string[] contactlistname { get; set; }
        public string[] discussionlist { get; set; }
        //public string startDate { get; set; }
        public int? isfollowUp { get; set; }
        //public string endDate { get; set; }

        public string priority { get; set; }

        public string description { get; set; }
        public string assignTo { get; set; }
        public DateTime? activitiesDate { get; set; }
        public DateTime? reminderTime { get; set; }
        public int? isreminder { get; set; }
        public int? taskStatusId { get; set; }
        public string statusdescription { get; set; }
        public string statusremarks { get; set; }

        public IEnumerable<ActivityMaster> activityMasters { get; set; }
        public IEnumerable<ColdActivityMasters> coldActivityMasters { get; set; }
        public ActivityMaster activityMaster { get; set; }
        public ColdActivityMasters coldActivityMaster { get; set; }
        public IEnumerable<ActivityCategory> ActivityCategories { get; set; }
        public IEnumerable<ActivitySession> ActivitySessions { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }

        public IEnumerable<Contacts>  contacts { get; set; }
        public IEnumerable<Team> teams { get; set; }
        public IEnumerable<ActivityStatus> activityStatuses { get; set; }
        public IEnumerable<ActivityMasterCViewModel> activityMasterCViewModels { get; set; }
        public IEnumerable<ActivityMasterCAViewModel> activityMasterCAViewModels { get; set; }
        public IEnumerable<ColdActivityMasterCAViewModel> coldActivityMasterCAViewModels { get; set; }
        public IEnumerable<ColdActivityMasterCViewModel> coldActivityMasterCViewModels { get; set; }
        public IEnumerable<ActivityStatusProgress> activityStatusProgresses { get; set; }
        public IEnumerable<TaskSection> taskSections { get; set; }
    }
}
