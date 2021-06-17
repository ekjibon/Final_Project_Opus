using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class TaskSaveViewModel
    {
        public string name { get; set; }

        public string description { get; set; }

        public int? employeeId { get; set; }

        public int? employeeAssignId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? satrtDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        public decimal? estimatedHours { get; set; }

        public int? taskSectionId { get; set; }

        public int? activityStatusId { get; set; }

        public int? activityCategoryId { get; set; }

        public int? activitySessionId { get; set; }

        public int? activityTypeId { get; set; }

        public int? isreminder { get; set; }

        public int? taskProjectId { get; set; }

        public int? myTask { get; set; }
    }
}
