using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class ActivityDetailsViewModel
    {
        public int Id { get; set; }
       
        [Required]
        public int activityMasterId { get; set; }

        public int contactsId { get; set; }

        public string activitiesDate { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int activityStatusId { get; set; }

        public string description { get; set; }

        public IEnumerable<ActivityMaster> activityMasters { get; set; }
        public ActivityMaster activityMaster { get; set; }
       
        public IEnumerable<ActivityCategory> ActivityCategories { get; set; }
        public IEnumerable<Contacts>  contacts { get; set; }
        public IEnumerable<ActivityStatus> activityStatus { get; set; }
        public IEnumerable<ActivityDetails> activityDetails { get; set; }
    }
}
