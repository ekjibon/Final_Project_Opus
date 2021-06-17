using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ActivityStatusViewModel
    {
        public int statusId { get; set; }

        [Required]
        public string statusName { get; set; }

        public IEnumerable<ActivityStatus> activityStatuses { get; set; }
    }
}
