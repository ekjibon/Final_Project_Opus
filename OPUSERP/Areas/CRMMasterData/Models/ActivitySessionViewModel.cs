using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ActivitySessionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string activitySessionName { get; set; }

        public IEnumerable<ActivitySession> activitySessions { get; set; }
    }
}
