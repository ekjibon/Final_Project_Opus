using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ActivityNatureViewModel
    {
        public int Id { get; set; }

        [Required]
        public string activityNatureName { get; set; }

        public IEnumerable<ActivityNature> activityNatures { get; set; }
    }
}
