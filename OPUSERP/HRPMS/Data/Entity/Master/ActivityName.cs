using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("ActivityName", Schema = "HR")]
    public class ActivityName:Base
    {
        public int activityTypeId { get; set; }
        public HRPMSActivityType activityType { get; set; }

        public string name { get; set; }
    }
}
