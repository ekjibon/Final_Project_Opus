using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SEBA.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskInformation", Schema = "TMS")]
    public class TaskInformation:Base
    {
        public int? problemMasterId { get; set; }
        public ProblemMaster problemMaster { get; set; }

        [MaxLength(400)]
        public string name { get; set; }
        public string description { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? employeeAssignId { get; set; }
        public EmployeeInfo employeeAssign { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? satrtDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        public decimal? estimatedHours { get; set; }

        public int? taskSectionId { get; set; }
        public TaskSection taskSection { get; set; }

        public int? activityStatusId { get; set; }
        public ActivityStatus activityStatus { get; set; }

        public int? activityCategoryId { get; set; }
        public ActivityCategory activityCategory { get; set; }

        public int? activitySessionId { get; set; }
        public ActivitySession activitySession { get; set; }

        public int? activityTypeId { get; set; }
        public ActivityType activityType { get; set; }

        public int? leadsId { get; set; }
        public Leads leads { get; set; }

    }
}
