using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("ActivityType", Schema = "CRM")]
    public class ActivityType : Base
    {
       
        [Required]
        public string activityTypeName { get; set; }
    }
}
