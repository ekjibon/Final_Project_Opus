using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ActivityCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string activityCategoryName { get; set; }

        public IEnumerable<ActivityCategory> activityCategories { get; set; }
    }
}
