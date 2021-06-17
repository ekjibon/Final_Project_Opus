using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ActivityTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string activityTypeName { get; set; }

        public IEnumerable<ActivityType> activityTypes { get; set; }
    }
}
