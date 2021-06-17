using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.SEBA.Data.Entity;
using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SEBA.Models
{
    public class TaskInformationViewModel
    {
        public int taskInformationId { get; set; }
        public int? problemMasterId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? employeeId { get; set; }
        public int? employeeAssignId { get; set; }  
        public DateTime? satrtDate { get; set; }      
        public DateTime? endDate { get; set; }
        public decimal? estimatedHours { get; set; }
        public int? taskSectionId { get; set; }  
        public int? activityStatusId { get; set; }
        public int? activityCategoryId { get; set; }
        public int? activitySessionId { get; set; }
        public int? activityTypeId { get; set; }

        public IEnumerable<TaskProject> taskProjects { get; set; }
        public IEnumerable<ActivityStatus> activityStatuses { get; set; }
        public IEnumerable<ActivityCategory> activityCategories { get; set; }
        public IEnumerable<ActivitySession> activitySessions { get; set; }
        public IEnumerable<ActivityType> activityTypes { get; set; }
        public IEnumerable<TaskInformation> taskInformation { get; set; }
    }
}
