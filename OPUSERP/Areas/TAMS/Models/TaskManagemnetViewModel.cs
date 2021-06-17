using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.VIMS.Data;
using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class TaskManagemnetViewModel
    {
        public int projectId { get; set; }

        public int? taskProjectId { get; set; }

        public int? fromModal { get; set; }

        public string name { get; set; }

        public string empPhoto { get; set; }

        public string shortDescription { get; set; }

        public string description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? satrtDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? approxEndDate { get; set; }

        public IEnumerable<TaskProject> taskProjects { get; set; }

        public IEnumerable<TaskSection> taskSections { get; set; }

        public ProjectViewModel projectViewModel { get; set; }

        public SingleTaskViewModel singleTaskViewModel { get; set; }

        public IEnumerable<ActivityCategory> ActivityCategories { get; set; }
        public IEnumerable<ActivitySession> ActivitySessions { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public IEnumerable<ActivityStatus> activityStatuses { get; set; }

        public IEnumerable<Company> companies { get; set; }

        public IEnumerable<TaskInformationHistory> taskInformationHistories { get; set; }

        public int? empId { get; set; }

        public string empname { get; set; }
    }
}
